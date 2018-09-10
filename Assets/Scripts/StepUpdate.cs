using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepUpdate : MonoBehaviour
{
    [SerializeField] private float stepTime;
    private float lastStep;
    private HashSet<StepMovement> steppers;
    
    void Awake()
    {
        steppers = new HashSet<StepMovement>();
        lastStep = Time.time - stepTime;
    }
    
    void Update()
    {
        if(Time.time - lastStep >= stepTime)
        {
            lastStep= Time.time;
            float nextTime = lastStep + stepTime;
            foreach(StepMovement sm in steppers)
            {
                sm.step(nextTime);
            }
        }
    }
    
    public void add(StepMovement sm)
    {
        steppers.Add(sm);
    }
    
    public void remove(StepMovement sm)
    {
        steppers.Remove(sm);
    }
}
