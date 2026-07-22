using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FreeBasicFirstPersonController
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("Free Basic First Person Controller/First Person Movement")]
    public class FirstPersonMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Base movement speed in units per second.")]
        public float speed = 5f;

        [Header("Running Settings")]
        [Tooltip("Enables or disables running capability.")]
        public bool canRun = true;
        [Tooltip("Running movement speed in units per second.")]
        public float runSpeed = 9f;

        [Header("Stair & Step Climbing")]
        [Tooltip("Enables automatic climbing over stair steps and low ledges.")]
        public bool enableStepClimbing = true;
        [Tooltip("Maximum step height character can automatically step over.")]
        public float maxStepHeight = 0.35f;
        [Tooltip("Distance ahead to detect steps.")]
        public float stepCheckDistance = 0.3f;
        [Tooltip("Smoothness factor for stepping up stairs.")]
        public float stepSmoothness = 14f;
        [Tooltip("Layer mask for step detection.")]
        public LayerMask stepLayerMask = ~0;

        [Header("Input System Actions")]
        [Tooltip("Input action for movement (Vector2). Optional if using automatic keyboard/gamepad fallback.")]
        public InputActionReference moveAction;
        [Tooltip("Input action for sprinting (Button). Optional if using automatic keyboard/gamepad fallback.")]
        public InputActionReference sprintAction;

        /// <summary> True when the character is currently running. </summary>
        public bool IsRunning { get; private set; }

        /// <summary> Functions to override movement speed. Will use the last added override. </summary>
        public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

        private Rigidbody rb;
        private CapsuleCollider col;
        private GroundCheck groundCheck;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                rb.freezeRotation = true;
            }
            col = GetComponent<CapsuleCollider>();
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        private void OnEnable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.Enable();
            }
            if (sprintAction != null && sprintAction.action != null)
            {
                sprintAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (moveAction != null && moveAction.action != null)
            {
                moveAction.action.Disable();
            }
            if (sprintAction != null && sprintAction.action != null)
            {
                sprintAction.action.Disable();
            }
        }

        private void FixedUpdate()
        {
            // Read sprint input
            bool sprintInput = false;
            if (sprintAction != null && sprintAction.action != null)
            {
                sprintInput = sprintAction.action.IsPressed();
            }
            else if (Keyboard.current != null)
            {
                sprintInput = Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed;
            }
            else if (Gamepad.current != null)
            {
                sprintInput = Gamepad.current.leftStickButton.isPressed;
            }

            IsRunning = canRun && sprintInput;

            // Calculate target speed
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
            {
                targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            }

            // Read move input vector
            Vector2 inputVector = Vector2.zero;
            if (moveAction != null && moveAction.action != null)
            {
                inputVector = moveAction.action.ReadValue<Vector2>();
            }
            else
            {
                if (Keyboard.current != null)
                {
                    float x = 0f;
                    float y = 0f;
                    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x += 1f;
                    if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x -= 1f;
                    if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) y += 1f;
                    if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) y -= 1f;
                    inputVector = new Vector2(x, y);
                }
                if (inputVector == Vector2.zero && Gamepad.current != null)
                {
                    inputVector = Gamepad.current.leftStick.ReadValue();
                }
            }

            if (inputVector.sqrMagnitude > 1f)
            {
                inputVector.Normalize();
            }

            Vector2 targetVelocity = inputVector * targetMovingSpeed;

            // Handle smooth stair & step climbing
            if (groundCheck == null || groundCheck.isGrounded)
            {
                HandleStepClimbing(inputVector);
            }

            // Use rb.rotation (the authoritative physics rotation) for velocity direction,
            // not transform.rotation which may be the interpolated render-phase rotation.
            Quaternion physicsRotation = rb.rotation;
            Vector3 currentVel = GetRigidbodyVelocity();
            Vector3 newVel = physicsRotation * new Vector3(targetVelocity.x, currentVel.y, targetVelocity.y);
            SetRigidbodyVelocity(newVel);
        }

        private void HandleStepClimbing(Vector2 inputVector)
        {
            if (!enableStepClimbing || inputVector.sqrMagnitude < 0.01f) return;

            Quaternion physicsRotation = rb.rotation;
            Vector3 moveDir = physicsRotation * new Vector3(inputVector.x, 0, inputVector.y).normalized;
            float radius = col != null ? col.radius : 0.5f;
            float totalRayDistance = radius + stepCheckDistance;

            Vector3 footPos = rb.position;

            // Raycast at lower foot level
            Vector3 lowerRayOrigin = footPos + Vector3.up * 0.05f;
            if (Physics.Raycast(lowerRayOrigin, moveDir, out RaycastHit lowerHit, totalRayDistance, stepLayerMask, QueryTriggerInteraction.Ignore))
            {
                // Raycast at upper step level -- clear above the step means it's climbable
                Vector3 upperRayOrigin = footPos + Vector3.up * (maxStepHeight + 0.05f);
                if (!Physics.Raycast(upperRayOrigin, moveDir, totalRayDistance, stepLayerMask, QueryTriggerInteraction.Ignore))
                {
                    // Raycast down to find the top surface of the step
                    Vector3 stepTopOrigin = lowerRayOrigin + moveDir * (lowerHit.distance + 0.05f) + Vector3.up * maxStepHeight;
                    if (Physics.Raycast(stepTopOrigin, Vector3.down, out RaycastHit topHit, maxStepHeight + 0.1f, stepLayerMask, QueryTriggerInteraction.Ignore))
                    {
                        float stepHeight = topHit.point.y - rb.position.y;
                        if (stepHeight > 0.02f && stepHeight <= maxStepHeight)
                        {
                            // Use MovePosition instead of direct rb.position assignment
                            // so that Rigidbody interpolation can smooth the transition.
                            float lift = stepHeight * Time.fixedDeltaTime * stepSmoothness;
                            rb.MovePosition(rb.position + Vector3.up * lift);
                        }
                    }
                }
            }
        }

        private Vector3 GetRigidbodyVelocity()
        {
            if (rb == null) return Vector3.zero;
#if UNITY_6000_0_OR_NEWER
            return rb.linearVelocity;
#else
            return rb.velocity;
#endif
        }

        private void SetRigidbodyVelocity(Vector3 newVelocity)
        {
            if (rb == null) return;
#if UNITY_6000_0_OR_NEWER
            rb.linearVelocity = newVelocity;
#else
            rb.velocity = newVelocity;
#endif
        }
    }
}
