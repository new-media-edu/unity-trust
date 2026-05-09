using UnityEngine;
using UnityEngine.Events;

namespace WorkshopAssets
{
    [RequireComponent(typeof(BoxCollider))]
    public class SimpleTrigger : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent onPlayerEnter;
        public UnityEvent onPlayerExit;

        private void Awake()
        {
            // Ensure the collider is set to "Is Trigger"
            GetComponent<BoxCollider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering has the "Player" tag
            if (other.CompareTag("Player"))
            {
                onPlayerEnter.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerExit.Invoke();
            }
        }
    }
}
