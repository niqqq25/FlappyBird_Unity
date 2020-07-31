using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    #region Fields

    bool isRunning = false;
    float elapsedTime = 0;
    float duration = 0;

    UnityEvent timerFinishedEvent = new TimerFinished();

    #endregion

    #region Properties

    public float Duration {
        set { duration = value; }
    }

    public bool IsRunning
    {
        get { return isRunning; }
    }

    #endregion

    #region Methods
	
	// Update is called once per frame
	void Update () {
		if(isRunning)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > duration)
            {
                isRunning = false;
                timerFinishedEvent.Invoke();
            }
        }
	}

    public void Run()
    {
        isRunning = true;
        elapsedTime = 0;
    }

    public void AddListener(UnityAction listener)
    {
        timerFinishedEvent.AddListener(listener);
    }

    #endregion
}
