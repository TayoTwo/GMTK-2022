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
    public GameObject wallTile;
    public GameObject[] tiles;
    public float[] tileChances;

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

                    Debug.Log("PLACING WALL");
                    grid[w.position.x,w.position.y] = 0;

                } else {

                    //Debug.Log("PLACING OTHER");
                    int tileIndex = grid[w.position.x,w.position.y];
                    float value = Random.value;
                    float biasTotal = 0;

                    if(grid[w.position.x,w.position.y] != -1){

                        foreach(float f in tileChances){

                            biasTotal += f;

                        }

                        for(int j = 0; j < tiles.Length;j++){

                            //Debug.Log(tileChances[j]/biasTotal);

                            if(value < tileChances[j]/biasTotal){

                                tileIndex = j;
                                //Debug.Log(tileIndex);

                            }

                        }

                        Debug.Log("PLACING " + tileIndex);
                        grid[w.position.x,w.position.y] = tileIndex;

                    }


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

                Debug.Log(grid[x,y]);

                SpawnElement(x,y,tiles[grid[x,y]]);

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
