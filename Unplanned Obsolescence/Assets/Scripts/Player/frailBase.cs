using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frailBase : MonoBehaviour
{
    public Rigidbody2D pcRigidBody;
    float speed = 10f;

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

        //store player input as movement vector
        playerInput = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        
        // Moves the character according to existing vectors
        pcRigidBody.MovePosition(transform.position + playerInput * Time.deltaTime * speed);
    }
}
