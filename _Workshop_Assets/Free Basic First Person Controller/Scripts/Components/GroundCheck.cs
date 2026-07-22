using UnityEngine;

namespace FreeBasicFirstPersonController
{
    [AddComponentMenu("Free Basic First Person Controller/Ground Check")]
    public class GroundCheck : MonoBehaviour
    {
        [Header("Detection Settings")]
        [Tooltip("SphereCast radius for footprint detection.")]
        public float sphereRadius = 0.35f;

        [Tooltip("Maximum distance from ground threshold.")]
        public float distanceThreshold = 0.15f;

        [Tooltip("Layers considered as ground. Defaults to everything (~0).")]
        public LayerMask groundMask = ~0;

        [Tooltip("Whether this transform is grounded currently.")]
        public bool isGrounded = true;

        /// <summary> Called when touching the ground after being airborne. </summary>
        public event System.Action Grounded;

        private const float OriginOffset = 0.2f;
        private Vector3 RaycastOrigin => transform.position + Vector3.up * (OriginOffset + sphereRadius);
        private float CastDistance => distanceThreshold + OriginOffset;

        private void LateUpdate()
        {
            // SphereCast down to detect ground contact across character footprint
            bool isGroundedNow = Physics.SphereCast(
                RaycastOrigin,
                sphereRadius,
                Vector3.down,
                out RaycastHit hit,
                CastDistance,
                groundMask,
                QueryTriggerInteraction.Ignore
            );

            // Call event when landing back on ground
            if (isGroundedNow && !isGrounded)
            {
                Grounded?.Invoke();
            }

            isGrounded = isGroundedNow;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(RaycastOrigin, sphereRadius);
            Gizmos.DrawWireSphere(RaycastOrigin + Vector3.down * CastDistance, sphereRadius);
        }
    }
}
