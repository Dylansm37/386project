using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Transform bird;

    public float verticalSpacing = 10f;
    public float horizontalRange = 8f;
    public float bufferAbove = 5f;

    private float nextSpawnY;

    void Start()
    {
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;
        nextSpawnY = cameraTopY + bufferAbove;
    }

    void Update()
    {
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;

        // Use 'if' instead of 'while' to prevent too many clouds per frame
        if (nextSpawnY < cameraTopY + bufferAbove)
        {
            SpawnCloud(nextSpawnY);
            nextSpawnY += Random.Range(verticalSpacing * 0.8f, verticalSpacing * 1.2f);
        }
    }

    void SpawnCloud(float yPos)
    {
        float xPos = Random.Range(-horizontalRange, horizontalRange);
        Vector3 spawnPos = new Vector3(xPos, yPos, 10f);
        GameObject cloud = Instantiate(cloudPrefab, spawnPos, Quaternion.identity);

        // Optional tag for cleanup/debugging
        cloud.tag = "Cloud";

        ParallaxBackground parallax = cloud.GetComponent<ParallaxBackground>();
        if (parallax != null && parallax.cameraTransform == null)
        {
            parallax.cameraTransform = Camera.main.transform;
        }
    }
}
