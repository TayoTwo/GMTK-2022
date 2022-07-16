using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  Walker{

    public Vector2Int direction;
    public Vector2Int position;

    public Walker(Vector2Int startPos){

        RandomizeDirection();
        position = startPos;

    }

    public void Move(){

        position += direction;

    }

    public void ChangePosition(int x,int y){

        position = new Vector2Int(x,y);

    }

    public void RandomizeDirection(){
		//pick random int between 0 and 3
		int choice = Mathf.FloorToInt(Random.value * 3.99f);
		//use that int to chose a direction
		switch (choice){
			case 0:
				direction = Vector2Int.RoundToInt(Vector2.down);
                break;
			case 1:
				direction = Vector2Int.RoundToInt(Vector2.left);
                break;
			case 2:
				direction = Vector2Int.RoundToInt(Vector2.up);
                break;
			default:
				direction = Vector2Int.RoundToInt(Vector2.right);
                break;
		}

	}

}

public class DungeonGen : MonoBehaviour
{

    [Header("Grid")]
    [SerializeField]
    public int[,] grid;
    public int[] dimensions = new int[2];
    public int unitLength = 1;
    public Vector2Int centerOfGrid;
    public Vector3 offset;
    //public int stage;
    [Header("Objects")]
    public GameObject tile;
    public GameObject[] wallTypes;

    [Header("Walkers Parameters")]
    public int maxIterations = 100;
    [SerializeField]
    public List<Walker> walkers = new List<Walker>();
    public int maxWalkerCount;
    public float spawnChance;
    public float changeDirectionChance;
    public float fillPercentage;

    // Start is called before the first frame update
    void Awake(){

        walkers.Clear();

        grid = new int[dimensions[0],dimensions[1]];
        centerOfGrid = new Vector2Int( Mathf.FloorToInt(dimensions[0]/2),  Mathf.FloorToInt(dimensions[1]/2));
        offset = (new Vector3(dimensions[0],dimensions[1],offset.z) * 0.5f * unitLength) + new Vector3(-0.5f * unitLength,-0.5f * unitLength,0);

        //Debug.Log(center);
        //Debug.Log(x + ";" + y);
        walkers.Add(new Walker(centerOfGrid));

        GenerateTiles();
        SpawnStage();

    }
    void GenerateTiles(){

        for(int i = 0;i < maxIterations;i++){

            foreach(Walker w in walkers.ToArray()){
                
                //Make sure the walker is in the boundaries of the grid
                w.ChangePosition(

                        Mathf.Clamp(w.position.x,0,dimensions[0]-1),
                        Mathf.Clamp(w.position.y,0,dimensions[1]-1)
                    
                    );

                //Debug.Log(w.position.x);
                //Place tile at walker position
                if(w.position.x == 0 || w.position.x == dimensions[0] - 1 || w.position.y == 0 || w.position.y == dimensions[1] - 1){

                    grid[w.position.x,w.position.y] = 0;

                } else {

                    //Debug.Log(w.position.x + ":" +w.position.y);
                    grid[w.position.x,w.position.y] = 1;

                }

                //Change direction based on the change direction chance
                if(Random.value < changeDirectionChance){

                    w.RandomizeDirection();

                }
                //Contain within the walls
                if(Random.value < spawnChance && walkers.ToArray().Length < maxWalkerCount){

                    walkers.Add(new Walker(w.position));

                }

                w.Move();
                
                if(FillPercentage() >= fillPercentage){

                    return;

                }

            }



        }        

    }
    float FillPercentage(){

        float tileCount = 0f;

        foreach(int cell in grid){

            if(cell == 1){

                tileCount++;

            }

        }

        return tileCount/grid.LongLength;

    }
    void SpawnStage(){

        for(int y = 0;y < grid.GetLength(1);y++){

            for(int x = 0;x < grid.GetLength(0);x++){

                switch(grid[x,y]){

                    case 0:

                        // //Debug.Log(x + "-" + y);
                        // int wallTypeIndex = 0;
                        // //East
                        // if(x < grid.GetLength(0) - 1 && grid[x+1,y] == 1) wallTypeIndex +=2;
                        // //West
                        // if(0 < x && grid[x-1,y] == 1) wallTypeIndex +=8;
                        // //North
                        // if(y < grid.GetLength(1) - 1 && grid[x,y+1] == 1) wallTypeIndex +=1;
                        // //South
                        // if(0 < y && grid[x,y-1] == 1) wallTypeIndex +=4;

                        //Debug.Log(wallTypeIndex);

                        // if(wallTypeIndex > 0 && wallTypeIndex < 15){ 
                            
                        //     SpawnElement(x,y,wallTypes[wallTypeIndex],0);

                        // } else if(wallTypeIndex == 15){

                        //     SpawnElement(x,y,tile,1);

                        // }

                        SpawnElement(x,y,wallTypes[0]);

                        break;

                    case 1: 
                        SpawnElement(x,y,tile);
                        break;

                }

            }

        }

    }
    void SpawnElement(float x,float y, GameObject obj){

        Vector3 spawnPos = new Vector3(x,y,0) * unitLength;
        spawnPos -= offset;

        GameObject o = (GameObject)Instantiate(obj,spawnPos,Quaternion.identity);
        o.transform.parent = transform;
        o.name += " [" + o.transform.position.ToString() + "]";

    }

}
