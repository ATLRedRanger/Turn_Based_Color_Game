using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Renderer : MonoBehaviour
{
    public Sprite sprite;
    private Unit_Spawner spawner;
    public SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Unit_Spawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite = spawner.enemyOne.spriteRenderer.sprite;
        spriteR.sprite = sprite;
    }
}
