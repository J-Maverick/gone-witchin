using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetection : MonoBehaviour
{
    Animator animator;

    [Range(0,1)]
    public float pourThreshold = 0.6F;
    public float pourPositionThreshold = 2F;

    public float particleEmissionRate = 500F;

    ReagentHandler reagentHandler;
    private bool isPouring = false;
    ParticleSystem particleSystem;

    float pourAngle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.emissionRate = 0F;
    }

    // Update is called once per frame
    void Update()
    {
        pourAngle = CalculatePourAngle();
        //Debug.Log(string.Format("Pour Angle: {0}", pourAngle));
        bool pourCheck = pourAngle > pourThreshold && transform.position.y > pourPositionThreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }

        if (isPouring)
        {
            particleSystem.startSize = 0.1F * NormalizedPourAngle(pourAngle);
            // particles

            //animator.SetFloat("PourAngle", NormalizedPourAngle(pourAngle, isPouring));
        }
    }

    private void StartPour()
    {
        particleSystem.emissionRate = particleEmissionRate;
    }

    private void EndPour()
    {
        particleSystem.emissionRate = 0F;
    }

    private float CalculatePourAngle()
    {        return transform.up.y;
    }

    // Normalizes the pour angle to 0-1
    private float NormalizedPourAngle(float pourAngle)
    {
        return (pourAngle - pourThreshold) / (1.0F - pourThreshold);
    }

    private void OnParticleTrigger()
    {
        Debug.Log("Particle Triggered");
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        Debug.Log(string.Format("numEnter: {0}", numEnter));
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            reagentHandler = transform.parent.parent.gameObject.GetComponentInParent<ReagentHandler>();
            Debug.Log(string.Format("Setting flowRate of {0} to {1}", reagentHandler.reagentName, p.startSize));
            reagentHandler.flowRate = p.startSize * 0.2F;
            reagentHandler.cauldron.AddReagent(reagentHandler);
            reagentHandler.flowRate = 0F;
        }
        
    }

}
