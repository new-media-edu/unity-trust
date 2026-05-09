using UnityEngine;

namespace WorkshopAssets
{
    public class LookAtPlayer : MonoBehaviour
    {
        private Transform playerTransform;

        void Start()
        {
            // Find the player automatically using the tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        void LateUpdate()
        {
            if (playerTransform != null)
            {
                // Make the object look at the player
                transform.LookAt(playerTransform);
                
                // Flip 180 degrees so text isn't backwards
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
