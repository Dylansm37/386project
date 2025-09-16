using UnityEngine;

public class EnemyFlyAcross : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Clean up after it flies across
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
