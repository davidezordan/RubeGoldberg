using UnityEngine;

namespace Davide.Dev.UnityHelpers
{
    public class RotatingObject : MonoBehaviour
    {

        [Tooltip("Rotation speed")]
        public float rotationSpeed = 5;

        [Tooltip("Rotation direction")]
        public Vector3 rotationDirection = Vector3.up;

        void Update()
        {

            transform.Rotate(rotationDirection, rotationSpeed * 10 * Time.deltaTime);
        }
    }
}