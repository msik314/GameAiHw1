using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharControl))]
public class StepMovement : MonoBehaviour
{
    private CharControl cc;
    private Vector2Int nextPos;
    private Vector2Int lastPos;
    private float lastStep;
    private float nextStep;
    private GridManager grid;
       
    void Awake()
    {
        lastStep = 0;
        nextStep = 1;
        lastPos = Vector2Int.RoundToInt(transform.position);
        nextPos = lastPos;
        cc = GetComponent<CharControl>();
        cc.setDirection(1);
        grid = (GridManager)GameObject.FindObjectOfType(typeof(GridManager));
    }
    
    void Start()
    {
        StepUpdate su = (StepUpdate)GameObject.FindObjectOfType(typeof(StepUpdate));
        su.add(this);
    }
    
    void Update()
    {
        float time = Time.time;
        float mix = (time - lastStep)/(nextStep - lastStep);
        transform.position = Vector2.Lerp(lastPos, nextPos, mix);
    }
    
    public void step(float nextStep)
    {
        lastStep = Time.time;
        this.nextStep = nextStep;
        
        lastPos = Vector2Int.RoundToInt(transform.position);
        
        bool ic = grid.isIntersection(lastPos);
        
        int d = cc.getDirection(ic);
        
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
        default:
            nextPos = lastPos;
            break;
        }
        
        if(!grid.walk(lastPos, nextPos))
        {
            nextPos = lastPos;
        }
    }
}
