using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 


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
        // Restart the scene after 2 seconds
        Invoke(nameof(RestartScene), 2f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}