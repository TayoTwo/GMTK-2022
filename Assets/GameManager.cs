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

        dunGen = GetComponent<DungeonGen>();
        Vector3 center = (new Vector3(dunGen.centerOfGrid.x,dunGen.centerOfGrid.y,dunGen.offset.z / dunGen.unitLength) * dunGen.unitLength);

        GameObject p = (GameObject)Instantiate(playerObj,center - dunGen.offset,Quaternion.identity);
        p.GetComponent<GridPlayerMovement>().gridPosition = dunGen.centerOfGrid;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
