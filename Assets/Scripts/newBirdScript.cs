using UnityEngine;

public class newBirdScript : MonoBehaviour
{
	public Rigidbody2D myRigidBody;
 	public float flapStrength;
 
  
    void Start()
    {
        
    }

      void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
        }

         if (Input.GetKey(KeyCode.A) == true)
        {
            myRigidBody.linearVelocity = new Vector2(-flapStrength, myRigidBody.linearVelocity.y);
        }

         if (Input.GetKey(KeyCode.D) == true)
        {
            myRigidBody.linearVelocity = new Vector2(flapStrength, myRigidBody.linearVelocity.y);
        }
        
    }
}