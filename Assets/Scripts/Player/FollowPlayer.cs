using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float smoothTime;

    Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update(){

        if(target == null){

            target = GameObject.FindGameObjectWithTag("Player").transform;

        }

        MoveCamera();
        
    }

    void MoveCamera(){

        Vector3 posToMove = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position,posToMove,ref vel,smoothTime);

    }
}
