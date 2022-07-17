using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    [Range(1,6)]
    public int value;
    public RawImage image;
    public Sprite[] diceImages;

    // Update is called once per frame
    void Update()
    {
        
        image.texture = diceImages[value - 1].texture;

    }

}
