using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirHandler : MonoBehaviour
{
    public float minSpeed = 1.0F;
    public float stirTimeThreshold = 3.0F;
    public float stopTimeThreshold = 1.0F;
    public bool stirTrigger = false;

    bool isStirring;
    Vector3 lastPosition = Vector3.zero;
    float stirSpeed = 0.0F;
    public float stirTimer = 0.0F;
    bool stirTimerEnabled = false;
    public float stopTimer = 0.0F;

    public CauldronHandler cauldron;

    // Update is called once per frame
    void Update()
    {
        if (isStirring)
        {
            if (stirSpeed >= minSpeed)
            {
                UpdateStirTimer();
                ResetStopTimer();
            }
            else
            {
                if (stirTimerEnabled)
                {
                    UpdateStirTimer();
                }
                UpdateStopTimer();
            }

            //Debug.Log(string.Format("Stirring || stirSpeed: {0} || stirTimerEnabled: {1} || stirTimer: {2} || stopTimer: {3} || stirTrigger: {4}", stirSpeed, stirTimerEnabled, stirTimer, stopTimer, stirTrigger));
        }
        else
        {
            ResetStirTimer();
            ResetStopTimer();
        }
    }

    // Collision trigger, begin 'stirring' on enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Spoon")
        {
            isStirring = true;
            Debug.Log("Started Stirring");
        }
    }

    // When spoon is inside collider, get the speed of the spoon
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Spoon")
        {
            stirSpeed = ((other.GetComponentInParent<Transform>().transform.position - lastPosition).magnitude) / Time.deltaTime;

            lastPosition = other.GetComponentInParent<Transform>().transform.position;

        }
    }

    // Stop stirring on collider exit
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Spoon")
        {
            isStirring = false;
            Debug.Log("Stopped Stirring");
        }
    }

    // Update the stirring timer and check if stirring is complete
    private void UpdateStirTimer()
    {
        stirTimer += Time.deltaTime;
        stirTimerEnabled = true;
        if (stirTimer >= stirTimeThreshold)
        {
            cauldron.GetComponent<CauldronHandler>().CheckSuccess();
            ResetStirTimer();
            ResetStopTimer();
        }
    }

    private void ResetStirTimer()
    {
        stirTimer = 0.0F;
    }

    // Update the stopping timer and check if stirring timer should stop
    private void UpdateStopTimer()
    {
        stopTimer += Time.deltaTime;
        if (stopTimer >= stopTimeThreshold)
        {
            ResetStirTimer();
            ResetStopTimer();
            stirTimerEnabled = false;
        }
    }

    private void ResetStopTimer()
    {
        stopTimer = 0.0F;
    }
}
