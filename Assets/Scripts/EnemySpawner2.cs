using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 6f;
    public float minSpeed = 15f;
    public float maxSpeed = 30f;
    public float yOffset = -3f;
    public float spawnBuffer = 2f;

    private float timer = 0f;
    private float currentSpawnInterval;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnEnemy()
    {
        // right side of the screen
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));
        float spawnY = cam.transform.position.y + yOffset;

        Vector3 spawnPos = new Vector3(rightEdge.x + spawnBuffer, spawnY, 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // set random speed
        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        EnemyFlyAcross flyScript = enemy.GetComponent<EnemyFlyAcross>();
        if (flyScript != null)
        {
            flyScript.speed = randomSpeed;
        }
    }
}
