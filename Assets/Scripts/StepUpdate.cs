using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StepUpdate : MonoBehaviour
{
    [SerializeField] private float stepTime;
	[Space]
	[SerializeField] ScoreManager score;
	[SerializeField] GameObject startText, restartText;
    private float lastStep;
    private HashSet<StepMovement> steppers;

	static bool active = false;
    
    void Awake()
    {
        steppers = new HashSet<StepMovement>();
        lastStep = Time.time - stepTime;
		restartText.SetActive(false);
		startText.SetActive(!active);
    }
    
    void Update()
    {
		if (!active)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				active = true;
				startText.SetActive(false);
				foreach (StepMovement step in steppers)
					step.enabled = true;
			}
			else
			{
				foreach (StepMovement step in steppers)
					step.enabled = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                if(player && sm.getLastPos() == player.getLastPos())
                {
					score.SaveScore();
					steppers.Remove(player);
					Destroy(player.gameObject);
					restartText.SetActive(true);
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
