using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyControl : CharControl
{
    [SerializeField] private Sprite[] directionSprites;
    SpriteRenderer sr;
    GridManager grid;
    bool initialized;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        grid = (GridManager)GameObject.FindObjectOfType(typeof(GridManager));
        initialized = false;
    }
    
    void Start()
    {
        getDirection();
    }
    
    public override void setDirection(int direction)
    {
        this.direction = direction;
        if(direction >= 0)
        {
            sr.sprite = directionSprites[direction];
        }
    }
    
    public override int getDirection()
    {
        int d = Random.Range(0, 4);
        Vector2Int lastPos = Vector2Int.RoundToInt(transform.position);
        
        for(int i = 0; i < 4; ++i)
        {
            d = (d + 1) % 4;
            
            if(d == (direction + 2) % 4)
            {
                continue;
            }
            
            Vector2Int nextPos = lastPos;
            
            switch(d)
            {
            case 0:
                nextPos = lastPos + Vector2Int.right;
                break;
            case 1:
                nextPos = lastPos + Vector2Int.up;
                break;
            case 2:
                nextPos = lastPos + Vector2Int.left;
                break;
            case 3:
                nextPos = lastPos + Vector2Int.down;
                break;
            }
            
            if(grid.walk(nextPos))
            {
                setDirection(d);
                return direction;
            }
        }
        return direction;
    }
}
