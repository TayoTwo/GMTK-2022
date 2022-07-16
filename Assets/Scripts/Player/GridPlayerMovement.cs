using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerMovement : MonoBehaviour
{
    public bool actionable;
    public float movSpeed;
    public Vector2Int gridPosition;
    DungeonGen dungeonGen;
    InputMaster inputMaster;
    Vector2 inputDir;
    float moveProgress = 1;

    void Awake(){

       inputMaster = new InputMaster();

    }

    // Start is called before the first frame update
    void Start()
    {
        dungeonGen = FindObjectOfType<DungeonGen>();
        inputMaster.Player.Movement.performed += ctx => OnMovePress(ctx.ReadValue<Vector2>());
        
    }

    void OnEnable(){

        inputMaster.Enable();

    }

    void OnDisable(){

        inputMaster.Disable();

    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(inputDir);

    }

    void OnMovePress(Vector2 moveDir){

        if(!actionable) return;

        //Debug.Log("Activated");

        //Debug.Log(Vector2Int.RoundToInt(moveDir));

        Vector2Int intendedMove = (gridPosition + Vector2Int.RoundToInt(moveDir));

        if(moveProgress >= 1){

            if(dungeonGen.grid[intendedMove.x,intendedMove.y] != 0){
            
                Vector2 targetPos = new Vector2(intendedMove.x - dungeonGen.centerOfGrid.x,intendedMove.y- dungeonGen.centerOfGrid.y) * dungeonGen.unitLength;
                //Debug.Log(targetPos);

                gridPosition = intendedMove;
                //Debug.Log(gridPosition);
                StartCoroutine(Move(targetPos));

            }

            //transform.LookAt(transform.position + new Vector3(moveDir.x,0,moveDir.y));

        }

    }

    IEnumerator Move(Vector2 target){

        moveProgress = 0;
        //AnimationController animationController =  mainCharacter.GetComponent<AnimationController>();
        //animationController.moving = true;
        Vector3 start = transform.position;

        while(moveProgress < 1)
        {
            moveProgress += Time.deltaTime * movSpeed;
            transform.position = Vector3.Lerp(start, target, moveProgress);
            yield return null;
        }

        //animationController.GetComponent<AnimationController>().moving = false;

    }


}
