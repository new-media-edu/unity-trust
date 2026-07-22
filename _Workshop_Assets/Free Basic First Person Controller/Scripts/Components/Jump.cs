using UnityEngine;
using UnityEngine.InputSystem;

namespace FreeBasicFirstPersonController
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("Free Basic First Person Controller/Jump")]
    public class Jump : MonoBehaviour
    {
        [Header("Jump Settings")]
        [Tooltip("Vertical jump force multiplier.")]
        public float jumpStrength = 5f;

        [Header("Grounding")]
        [SerializeField, Tooltip("GroundCheck reference to prevent jumping in mid-air.")]
        private GroundCheck groundCheck;

        [Header("Input System Actions")]
        [Tooltip("Input action for jump (Button). Optional if using automatic keyboard/gamepad fallback.")]
        public InputActionReference jumpAction;

        public event System.Action Jumped;

        private Rigidbody rb;

        private void Reset()
        {
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            if (jumpAction != null && jumpAction.action != null)
            {
                jumpAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (jumpAction != null && jumpAction.action != null)
            {
                jumpAction.action.Disable();
            }
        }

        private void Update()
        {
            bool jumpPressed = false;
            if (jumpAction != null && jumpAction.action != null)
            {
                jumpPressed = jumpAction.action.WasPressedThisFrame();
            }
            else if (Keyboard.current != null)
            {
                jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
            }
            else if (Gamepad.current != null)
            {
                jumpPressed = Gamepad.current.buttonSouth.wasPressedThisFrame;
            }

            if (jumpPressed && (groundCheck == null || groundCheck.isGrounded))
            {
                // Apply instant upward impulse force
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                Jumped?.Invoke();
            }
        }
    }
}
