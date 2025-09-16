using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bird; // Assign the Bird in Inspector
    public float verticalOffset = 0f;

   void LateUpdate()
    {
        if (bird != null)
        {
            // Follow bird's Y, but keep camera's X and Z
            Vector3 newPos = new Vector3(transform.position.x, bird.position.y + verticalOffset, transform.position.z);
            transform.position = newPos;
        }
    }
}