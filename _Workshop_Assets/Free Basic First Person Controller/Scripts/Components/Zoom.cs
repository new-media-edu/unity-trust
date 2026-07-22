using UnityEngine;
using UnityEngine.InputSystem;

namespace FreeBasicFirstPersonController
{
    [ExecuteInEditMode]
    [AddComponentMenu("Free Basic First Person Controller/Zoom")]
    public class Zoom : MonoBehaviour
    {
        [Header("FOV Settings")]
        [Tooltip("Default field of view when not zoomed.")]
        public float defaultFOV = 60f;
        [Tooltip("Field of view at maximum zoom.")]
        public float maxZoomFOV = 15f;
        [Range(0, 1)]
        [Tooltip("Current zoom level (0 = normal, 1 = max zoom).")]
        public float currentZoom;
        [Tooltip("Zoom sensitivity multiplier.")]
        public float sensitivity = 1f;

        [Header("Input System Actions")]
        [Tooltip("Input action for zoom scroll (Vector2 or Axis). Optional if using automatic mouse scroll fallback.")]
        public InputActionReference zoomAction;

        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            if (cam != null)
            {
                defaultFOV = cam.fieldOfView;
            }
        }

        private void OnEnable()
        {
            if (zoomAction != null && zoomAction.action != null)
            {
                zoomAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (zoomAction != null && zoomAction.action != null)
            {
                zoomAction.action.Disable();
            }
        }

        private void Update()
        {
            float scrollDelta = 0f;

            if (zoomAction != null && zoomAction.action != null)
            {
                scrollDelta = zoomAction.action.ReadValue<Vector2>().y;
            }
            else if (Mouse.current != null)
            {
                scrollDelta = Mouse.current.scroll.ReadValue().y;
            }

            if (Mathf.Abs(scrollDelta) > 0.01f)
            {
                // Normalize scroll delta across platforms / devices
                float scrollStep = Mathf.Sign(scrollDelta) * 0.1f * sensitivity;
                currentZoom = Mathf.Clamp01(currentZoom + scrollStep);
            }

            if (cam != null)
            {
                cam.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
            }
        }
    }
}
