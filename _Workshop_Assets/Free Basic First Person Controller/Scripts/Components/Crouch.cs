using UnityEngine;
using UnityEngine.InputSystem;

namespace FreeBasicFirstPersonController
{
    [AddComponentMenu("Free Basic First Person Controller/Crouch")]
    public class Crouch : MonoBehaviour
    {
        [Header("Input System Actions")]
        [Tooltip("Input action for crouch (Button). Optional if using automatic keyboard/gamepad fallback.")]
        public InputActionReference crouchAction;

        [Header("Slow Movement")]
        [Tooltip("Movement component to slow down when crouched.")]
        public FirstPersonMovement movement;
        [Tooltip("Movement speed when crouched.")]
        public float movementSpeed = 2f;

        [Header("Low Head & Camera")]
        [Tooltip("Head (Camera) transform to lower when crouched.")]
        public Transform headToLower;
        [HideInInspector]
        public float? defaultHeadYLocalPosition;
        [Tooltip("Local Y position of head when crouched.")]
        public float crouchYHeadPosition = 1.0f;
        [Tooltip("Speed multiplier for crouch camera & collider transitions.")]
        public float transitionSpeed = 12f;

        [Header("Collider Lowering & Ceiling Clearance")]
        [Tooltip("CapsuleCollider to lower when crouched.")]
        public CapsuleCollider colliderToLower;
        [HideInInspector]
        public float? defaultColliderHeight;
        [Tooltip("Layer mask for overhead ceiling clearance check.")]
        public LayerMask ceilingLayerMask = ~0;

        public bool IsCrouched { get; private set; }

        public event System.Action CrouchStart;
        public event System.Action CrouchEnd;

        private float currentHeadY;
        private float currentColliderHeight;

        private void Reset()
        {
            movement = GetComponentInParent<FirstPersonMovement>();
            if (movement != null)
            {
                Camera cam = movement.GetComponentInChildren<Camera>();
                if (cam != null) headToLower = cam.transform;
                colliderToLower = movement.GetComponentInChildren<CapsuleCollider>();
            }
        }

        private void Awake()
        {
            if (headToLower != null) defaultHeadYLocalPosition = headToLower.localPosition.y;
            if (colliderToLower != null) defaultColliderHeight = colliderToLower.height;
        }

        private void OnEnable()
        {
            if (crouchAction != null && crouchAction.action != null)
            {
                crouchAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (crouchAction != null && crouchAction.action != null)
            {
                crouchAction.action.Disable();
            }
        }

        private void LateUpdate()
        {
            bool isCrouchPressed = false;
            if (crouchAction != null && crouchAction.action != null)
            {
                isCrouchPressed = crouchAction.action.IsPressed();
            }
            else if (Keyboard.current != null)
            {
                isCrouchPressed = Keyboard.current.leftCtrlKey.isPressed || Keyboard.current.cKey.isPressed;
            }
            else if (Gamepad.current != null)
            {
                isCrouchPressed = Gamepad.current.buttonEast.isPressed;
            }

            // Check if there is an obstacle blocking standing up above the character
            bool headBlocked = IsCrouched && IsCeilingAbove();

            bool targetCrouchState = isCrouchPressed || headBlocked;

            if (targetCrouchState)
            {
                if (!IsCrouched)
                {
                    IsCrouched = true;
                    SetSpeedOverrideActive(true);
                    CrouchStart?.Invoke();
                }
            }
            else
            {
                if (IsCrouched)
                {
                    IsCrouched = false;
                    SetSpeedOverrideActive(false);
                    CrouchEnd?.Invoke();
                }
            }

            // Smoothly interpolate head Y position and collider height
            float targetHeadY = IsCrouched ? crouchYHeadPosition : (defaultHeadYLocalPosition ?? 1.7f);
            float targetHeight = IsCrouched ? 1.3f : (defaultColliderHeight ?? 2.0f);

            if (headToLower != null)
            {
                float newY = Mathf.Lerp(headToLower.localPosition.y, targetHeadY, Time.deltaTime * transitionSpeed);
                headToLower.localPosition = new Vector3(headToLower.localPosition.x, newY, headToLower.localPosition.z);
            }

            if (colliderToLower != null)
            {
                float newHeight = Mathf.Lerp(colliderToLower.height, targetHeight, Time.deltaTime * transitionSpeed);
                colliderToLower.height = newHeight;
                colliderToLower.center = Vector3.up * (newHeight * 0.5f);
            }
        }

        private bool IsCeilingAbove()
        {
            if (colliderToLower == null) return false;

            float standingHeight = defaultColliderHeight ?? 2.0f;
            float crouchHeight = colliderToLower.height;
            float clearanceNeeded = standingHeight - crouchHeight;

            if (clearanceNeeded <= 0.05f) return false;

            Vector3 rayOrigin = colliderToLower.transform.position + Vector3.up * crouchHeight;
            return Physics.Raycast(rayOrigin, Vector3.up, clearanceNeeded + 0.1f, ceilingLayerMask, QueryTriggerInteraction.Ignore);
        }

        #region Speed Override
        private void SetSpeedOverrideActive(bool state)
        {
            if (movement == null) return;

            if (state)
            {
                if (!movement.speedOverrides.Contains(SpeedOverride))
                {
                    movement.speedOverrides.Add(SpeedOverride);
                }
            }
            else
            {
                if (movement.speedOverrides.Contains(SpeedOverride))
                {
                    movement.speedOverrides.Remove(SpeedOverride);
                }
            }
        }

        private float SpeedOverride() => movementSpeed;
        #endregion
    }
}
