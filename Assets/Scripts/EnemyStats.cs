using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public string name;
    public int maxHealth;
    public float currentHealth;
    public int numberOfDice;
    public Sprite[] bunnySprites;
    SpriteRenderer spriteRenderer;

    void Start(){

        currentHealth = maxHealth;
        spriteRenderer.sprite = bunnySprites[Random.Range(0,bunnySprites.Length)];

    }

    public bool TakeDamage(int dmg){

        currentHealth -= dmg;

        if(currentHealth <= 0) 
            return true;
        else 
            return false;
        
    }

}
