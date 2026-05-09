using UnityEngine;

namespace WorkshopAssets
{
    public class AutoRotate : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [Tooltip("Degrees per second on each axis")]
        public Vector3 rotationSpeed = new Vector3(0, 50, 0);

        void Update()
        {
            // Rotates the object every frame based on the speed and time
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
