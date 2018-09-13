using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            List<StepMovement> sms = new List<StepMovement>();
            StepMovement player = null;
            
            foreach(StepMovement sm in steppers)
            {
                if(sm.getIsPlayer())
                {
                    player = sm;
                }
                else
                {
                    sms.Add(sm);
                }
                sm.step(nextTime);
            }
            foreach(StepMovement sm in sms)
            {
                if(sm.getLastPos() == player.getLastPos())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
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
