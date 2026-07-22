using UnityEngine;
using UnityEngine.InputSystem;

namespace FreeBasicFirstPersonController
{
    [AddComponentMenu("Free Basic First Person Controller/First Person Look")]
    public class FirstPersonLook : MonoBehaviour
    {
        [Header("Target Character")]
        [Tooltip("Transform of the character body that rotates horizontally.")]
        [SerializeField]
        private Transform character;

        [Header("Look Sensitivity & Smoothing")]
        [Tooltip("Mouse look sensitivity multiplier.")]
        public float sensitivity = 2f;
        [Tooltip("Look smoothing factor.")]
        public float smoothing = 1.5f;

        [Header("Cursor Settings")]
        [Tooltip("Unlocks cursor when ESC key is pressed.")]
        public bool unlockCursorOnEscape = true;

        [Header("Input System Actions")]
        [Tooltip("Input action for look delta (Vector2). Optional if using automatic mouse/gamepad fallback.")]
        public InputActionReference lookAction;

        private Vector2 velocity;
        private Vector2 frameVelocity;
        private Rigidbody characterRb;

        private void Reset()
        {
            FirstPersonMovement movement = GetComponentInParent<FirstPersonMovement>();
            if (movement != null)
            {
                character = movement.transform;
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (character == null)
            {
                FirstPersonMovement movement = GetComponentInParent<FirstPersonMovement>();
                if (movement != null)
                {
                    character = movement.transform;
                }
            }

            if (character != null)
            {
                characterRb = character.GetComponent<Rigidbody>();
            }

            // Initialize velocity from current angles to prevent camera snap on spawn
            float currentPitch = transform.localEulerAngles.x;
            if (currentPitch > 180f) currentPitch -= 360f;
            velocity.y = -currentPitch;
            if (character != null)
            {
                velocity.x = character.localEulerAngles.y;
            }
        }

        private void OnEnable()
        {
            if (lookAction != null && lookAction.action != null)
            {
                lookAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (lookAction != null && lookAction.action != null)
            {
                lookAction.action.Disable();
            }
        }

        private void Update()
        {
            // Handle cursor lock/unlock
            if (unlockCursorOnEscape && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            Vector2 rawInputDelta = Vector2.zero;

            if (lookAction != null && lookAction.action != null)
            {
                rawInputDelta = lookAction.action.ReadValue<Vector2>();
            }
            else
            {
                if (Mouse.current != null)
                {
                    rawInputDelta = Mouse.current.delta.ReadValue();
                }
                if (rawInputDelta == Vector2.zero && Gamepad.current != null)
                {
                    rawInputDelta = Gamepad.current.rightStick.ReadValue() * 15f;
                }
            }

            // Scale mouse delta appropriately for look speed
            Vector2 mouseDelta = rawInputDelta * (sensitivity * 0.05f);
            float lerpFactor = smoothing <= 0f ? 1f : Mathf.Clamp01(Time.deltaTime * (25f / Mathf.Max(smoothing, 0.1f)));
            frameVelocity = Vector2.Lerp(frameVelocity, mouseDelta, lerpFactor);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90f, 90f);

            // Rotate camera pitch (X axis) immediately
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

            // If there's no Rigidbody, rotate character horizontally in Update
            if (character != null && characterRb == null)
            {
                character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            }
        }

        private void FixedUpdate()
        {
            // If there is a Rigidbody, rotate it in FixedUpdate to avoid interpolation jitter
            if (characterRb != null)
            {
                characterRb.MoveRotation(Quaternion.AngleAxis(velocity.x, Vector3.up));
            }
        }
    }
}
