using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReagentDetection : MonoBehaviour
{
    ParticleSystem particleSystem;

    void OnEnable()
    {
        particleSystem = GetComponent<ParticleSystem>();
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
            Debug.Log(string.Format("Setting flowRate of {0} to {1}", particleSystem.transform.parent.parent.gameObject.GetComponentInParent<ReagentHandler>().itemName, p.startSize));
            particleSystem.transform.parent.parent.gameObject.GetComponentInParent<ReagentHandler>().flowRate = p.startSize;
        }

        // set
        particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
}
