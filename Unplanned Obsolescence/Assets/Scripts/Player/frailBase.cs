using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frailBase : MonoBehaviour
{
    public Rigidbody2D pcRigidBody;
    public Animator Animator;
    bool grounded = true;
    bool lockout = false;
    float speed = 10f;
    float jumpAmount = 10f;
    int direction = 1;

    private float lastY;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidBody from gameObject
        pcRigidBody = GetComponent<Rigidbody2D>();
        lastY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if performing an action that locks out of others
        if (lockout){
            //locks out until current animation is finished
            AnimatorStateInfo AnimInfo = Animator.GetCurrentAnimatorStateInfo(0);
            if(AnimInfo.normalizedTime >= 1){
                lockout = false;
            }
        }
        else{

            Vector3 playerInput;

            //Applies upwards force when jump is pressed
            if (Input.GetKeyDown(KeyCode.Space) && grounded == true){
                pcRigidBody.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
                grounded = false;
                Animator.SetTrigger("Jump");
            }

            if (Input.GetKeyDown(KeyCode.Z)){
                Animator.SetTrigger("Attack_Start");
                lockout = true;
            }

            //checks for falling
            float currentY = transform.position.y;
            if (currentY + 0.01 < lastY){
                Animator.SetTrigger("Fall");
            }
            if (currentY + 0.05 < lastY){
                Animator.SetTrigger("Fall_Big");
            }
            lastY = currentY;
        
            //store player input as movement vector
            playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        
            // Moves the character according to existing vectors
            pcRigidBody.AddForce(playerInput * speed);
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

    //for collision with ground
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Floor"){
            grounded = true;
            Animator.SetTrigger("Hit_Ground");
        }
        
    }
}
