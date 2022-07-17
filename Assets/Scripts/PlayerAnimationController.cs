using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationController : MonoBehaviour
{

    public float changePoseTime;
    public Sprite[] playerPoses;
    public SpriteRenderer spriteRenderer;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //North 0, East 1, South 2, West 3
        spriteRenderer.sprite = playerPoses[direction];

    }
}
