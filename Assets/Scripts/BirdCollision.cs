using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro; 


public class BirdCollision : MonoBehaviour
{
	 private bool isDead = false;
	

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
    
		//Die();

        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<Rigidbody2D>().simulated = false;
        
        FindFirstObjectByType<GameOverManager>().TriggerGameOver();

    }
}