using UnityEngine;

namespace WorkshopAssets
{
    public class AudioCrossfader : MonoBehaviour
    {
        [Header("Audio Sources")]
        public AudioSource sourceA;
        public AudioSource sourceB;

        [Header("Settings")]
        [Tooltip("Distance at which the blend is 100% Source B")]
        public float transitionDistance = 10f;

        private Transform playerTransform;

        void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        void Update()
        {
            if (playerTransform == null || sourceA == null || sourceB == null) return;

            // Calculate distance
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            
            // 0 = at the object (Source A), 1 = far away (Source B)
            float ratio = Mathf.Clamp01(dist / transitionDistance);

            sourceA.volume = 1f - ratio;
            sourceB.volume = ratio;
        }
    }
}
