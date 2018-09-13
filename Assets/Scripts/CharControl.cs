using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharControl : MonoBehaviour
{
    protected int direction;
    public abstract void setDirection(int direction);
    public abstract int getDirection();
}
