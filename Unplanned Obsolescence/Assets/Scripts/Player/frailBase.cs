using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frailBase : MonoBehaviour
{
    public Rigidbody2D pcRigidBody;
    public Animator Animator;
    float speed = 10f;
    float jumpAmount = 10f;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidBody from gameObject
        pcRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerInput;

        //Applies upwards force when jump is pressed
        if (Input.GetKeyDown(KeyCode.Space)){
            pcRigidBody.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        //store player input as movement vector
        playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        
        // Moves the character according to existing vectors
        pcRigidBody.AddForce(playerInput * speed);
        //pcRigidBody.MovePosition(transform.position + playerInput * Time.deltaTime * speed);
        Animator.SetFloat("Movement_Horizontal", System.Math.Abs(playerInput.x));
        // Adjusts Direction of Model
        if (direction == 1 && playerInput.x < 0){
            gameObject.transform.Rotate(new Vector3(0,180,0));
            direction = 0;
        }
        else if (direction == 0 && playerInput.x > 0){
            gameObject.transform.Rotate(new Vector3(0,180,0));
            direction = 1;
        }

        
    }
}
