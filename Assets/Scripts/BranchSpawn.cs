using UnityEngine;

public class BranchSpawn : MonoBehaviour
{
    
    public GameObject branchPrefab;    // the prefab for branches
    public Transform bird;             // reference to the bird
    public float spawnInterval = 20f;   // vertical distance between branches
    public float horizontalRange = 3f; // how wide the branches can spawn

    private float nextSpawnY;

    void Start()
    {
        // start just above the bird’s starting Y
        nextSpawnY = bird.position.y + spawnInterval;

    }

    void Update()
    {
        // calculate camera's top Y in world space
        float cameraTopY = Camera.main.transform.position.y + Camera.main.orthographicSize;

        // check if we need to spawn another branch off-screen
        while (nextSpawnY < cameraTopY + spawnInterval * 2f) 
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
