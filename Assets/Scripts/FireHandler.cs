using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FireHandler : MonoBehaviour
{
    public float heatingRate = 0.0F;
    public float maxRate = 0.001F;
    public float minRate = -0.0005F;
    public float decayRate = -0.0001F;

    public CauldronHandler cauldron;
    public ParticleSystem particleSystem;

    public float maxFireSpeed = 2F;
    public float minFireSpeed = 0.8F;

    public PlayerActions controls;
    public Animator fitchAnimator;

    int jump = 0;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    //private void Awake()
    //{
    //    controls = new PlayerActions();
    //}

    //private void OnEnable()
    //{
    //    controls.Enable();
    //}

    //private void OnDisable()
    //{
    //    controls.Disable();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    controls.Cauldron.FanFlame.performed += ctx => FanFlame();

    //    particleSystem.startSpeed = GetParticleSpeed();
    //    Debug.Log(string.Format("heatingRate: {0} || decayRate: {1}", heatingRate, decayRate));
    //    cauldron.AddHeat(this);
    //    DecayHeat();

    //    if (jump > 0)
    //    {
    //        jump -= 1;
    //    }
    //    else
    //    {
    //        jump = 0;
    //        fitchAnimator.SetBool("fanFlame", false);
    //    }
    //}

    // Set heating rate to maximum
    public void FanFlame()
    {
        heatingRate = maxRate;
        fitchAnimator.SetBool("fanFlame", true);
        jump = 5;
    }

    // Calculates the speed to set the fire particles at
    private float GetParticleSpeed()
    {
        float heatRatio = (heatingRate - minRate) / (maxRate - minRate);
        return heatRatio * (maxFireSpeed - minFireSpeed) + minFireSpeed;
    }

    // Decay heating rate until minimum value
    private void DecayHeat()
    {
        if (heatingRate != minRate)
        {
            heatingRate += decayRate * Time.deltaTime;
            if (heatingRate < minRate)
            {
                heatingRate = minRate;
            }
        }
    }
}
