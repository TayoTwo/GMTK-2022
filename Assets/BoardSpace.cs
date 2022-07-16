using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpaceType {

    Basic,
    Enemy,
    Trap,
    Buff

}

public class BoardSpace : MonoBehaviour
{

    public SpaceType spaceType;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c){

        if(c.tag != "Player") return;

        switch(spaceType){

            case SpaceType.Basic:
                Debug.Log("Player Landed on Basic");

                break;
            case SpaceType.Enemy:
                Debug.Log("Player Landed on Enemy");

                break;
            case SpaceType.Trap:
                Debug.Log("Player Landed on Trap");

                break;
            case SpaceType.Buff:
                Debug.Log("Player Landed on Buff");

                break;

        }

    }
}
