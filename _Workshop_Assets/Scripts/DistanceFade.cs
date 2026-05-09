using UnityEngine;
using TMPro;

namespace WorkshopAssets
{
    [RequireComponent(typeof(TextMeshPro))]
    public class DistanceFade : MonoBehaviour
    {
        [Header("Fade Settings")]
        public float fadeStartDistance = 5f;
        public float fadeEndDistance = 2f;

        private TextMeshPro textMesh;
        private Transform playerTransform;

        void Start()
        {
            textMesh = GetComponent<TextMeshPro>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        void Update()
        {
            if (playerTransform == null || textMesh == null) return;

            // Calculate distance between player and text
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            
            // Calculate transparency (0 to 1)
            float alpha = 1f - Mathf.Clamp01((dist - fadeEndDistance) / (fadeStartDistance - fadeEndDistance));
            
            // Apply transparency to the color
            Color c = textMesh.color;
            c.a = alpha;
            textMesh.color = c;
        }
    }
}
