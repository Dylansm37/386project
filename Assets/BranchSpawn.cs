using UnityEngine;

public class BranchSpawn : MonoBehaviour
{
    
    public GameObject branchPrefab;    // The prefab for branches
    public Transform bird;             // Reference to the bird
    public float spawnInterval = 20f;   // Vertical distance between branches
    public float horizontalRange = 3f; // How wide the branches can spawn

    private float nextSpawnY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start just above the bird’s starting Y
        nextSpawnY = bird.position.y + spawnInterval;

        // Pre-spawn some initial branches
     //   for (int i = 0; i < 10; i++)
       // {
        //    SpawnBranch();
         //   nextSpawnY += spawnInterval;
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate camera's top Y in world space
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;

        // Check if we need to spawn another branch off-screen
        while (nextSpawnY < cameraTopY + spawnInterval * 2f) // 2x interval = well off-screen
        {
            SpawnBranch();
            nextSpawnY += spawnInterval;
        }
    }

     void SpawnBranch()
    {
        float x = Random.Range(-horizontalRange, horizontalRange);
        Instantiate(branchPrefab, new Vector3(x, nextSpawnY, 0f), Quaternion.identity);
    }


}
