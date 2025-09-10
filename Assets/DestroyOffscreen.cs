using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (mainCam == null) return;

        Vector3 viewportPos = mainCam.WorldToViewportPoint(transform.position);

        if (viewportPos.y < -0.9f)
        {
            Destroy(gameObject);
        }
    }
}
