using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if(FindObjectsOfType<MusicManager>().Length > 1){

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
        
    }

}
