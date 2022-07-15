using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float accel;
    public float maxSpeed;
    public float decel;
    public Vector2 movDir;
    InputMaster inputMaster;
    Rigidbody2D rb;

    void Awake(){

        inputMaster = new InputMaster();
        rb = GetComponent<Rigidbody2D>();

    }

    void OnEnable(){

        inputMaster.Enable();

    }

    void OnDisable(){

        inputMaster.Disable();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movDir = inputMaster.Player.Movement.ReadValue<Vector2>().normalized * accel;

        rb.AddForce(movDir);

        if(movDir == Vector2.zero){

            rb.AddForce(-rb.velocity.normalized * decel);

        }

    }

    void Move(Vector2 dir){

        Debug.Log(dir);
       
    }
}
