using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharControl
{
    [SerializeField] private string vertAxis;
    [SerializeField] private string horAxis;
    
    void Start()
    {
        getDirection();
    }
    
    public override void setDirection(int direction)
    {
        this.direction = direction;
        if(direction >= 0)
        {
            transform.rotation = Quaternion.AngleAxis(direction * 90, Vector3.forward);
        }
    }
    
    public override int getDirection()
    {
        float horiz = Input.GetAxisRaw(horAxis);
        float vert = Input.GetAxisRaw(vertAxis);
        
        if(direction % 2 == 0)
        {
            if(!Mathf.Approximately(vert, 0))
            {
                setDirection(vert > 0 ? 1 : 3);
                return direction;
            }
            else if(direction == 0 && horiz < 0)
            {
                setDirection(2);
                return direction;
            }
            else if(direction == 2 && horiz > 0)
            {
                setDirection(0);
                return direction;
            }
        }
        else
        {
            if(!Mathf.Approximately(horiz, 0))
            {
                setDirection(horiz > 0 ? 0 : 2);
                return direction;
            }
            else if(direction == 1 && vert < 0)
            {
                setDirection(3);
                return direction;
            }
            else if(direction == 3 && vert > 0)
            {
                setDirection(1);
                return direction;
            }
        }
        
        return direction;
    }
}
