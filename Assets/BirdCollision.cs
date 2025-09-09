using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI text
using TMPro; // For TextMeshPro (optional)


public class BirdCollision : MonoBehaviour
{
	 private bool isDead = false;
	 public GameObject gameOverText;

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            Debug.Log("Bird hit something: " + collision.gameObject.name);
		Die();

         
        }
    }

    void Die()
    {
        isDead = true;


        // Stop the bird's physics
        GetComponent<Rigidbody2D>().simulated = false;

	if (gameOverText != null)
   	{
        gameOverText.SetActive(true);
    	}
        
        Debug.Log("Game Over!");



        // Restart the scene after 2 seconds
        Invoke(nameof(RestartScene), 2f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}