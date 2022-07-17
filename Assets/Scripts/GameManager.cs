using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerObj;
    DungeonGen dunGen;
    
    // Start is called before the first frame update
    void Awake()
    {

        GameObject p;
        dunGen = GetComponent<DungeonGen>();
        Vector3 center = (new Vector3(dunGen.centerOfGrid.x,dunGen.centerOfGrid.y,dunGen.offset.z / dunGen.unitLength) * dunGen.unitLength);

        if(FindObjectsOfType<GridPlayerMovement>().Length == 0){

            p = (GameObject)Instantiate(playerObj,center - dunGen.offset,Quaternion.identity);

        } else {

            p = FindObjectOfType<GridPlayerMovement>().gameObject;
            p.transform.position = center - dunGen.offset;

        }

        p.GetComponent<GridPlayerMovement>().gridPosition = dunGen.centerOfGrid;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
