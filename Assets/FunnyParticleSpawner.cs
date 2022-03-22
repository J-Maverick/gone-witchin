using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyParticleSpawner : MonoBehaviour
{
    public GameObject funnyPicture;
    public ParticleSystem particleSystem;
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        int numEnter = particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        Debug.Log(string.Format("numEnter: {0}", numEnter));
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            StartCoroutine(MakeFunny());
        }

    }

    IEnumerator MakeFunny()
    {
        funnyPicture.SetActive(true);

        yield return new WaitForSeconds(.1f);

        funnyPicture.SetActive(false);
    }


}
