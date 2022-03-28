using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class ReagentHandler : BottleItemHandler
{
    [Tooltip("Color of the liquid poured from the bottle.")]
    public Color pourColor;

    [Tooltip("Bottle emission.")]
    public Color pourEmissionColor;

    [Tooltip("Bottle metallics.")]
    [Range(0.0F, 1.0F)]
    public float pourMetallic;

    [Tooltip("Bottle smoothness.")]
    [Range(0.0F, 1.0F)]
    public float pourSmoothness;

    [Tooltip("Maximum amount that a bottle can hold (0.5 denotes that one bottle can fill the cauldron half way).")]
    [Range(0.1F, 1.0F)]
    public float maxFillLevel = 0.5f;

    [Header("For in-scene use only -- do not change")]
    public bool createMode = false;
    public CauldronHandler cauldron;
    public GameObject pourStream;
    public enum HandlerType { Slider, Bottle }
    public HandlerType handlerType = HandlerType.Bottle;
    public float flowRate = 0.0F;
    public bool colorLock = false;

    BottleType lastBottleType;
    bool lastCreateMode;
    GameObject reagentPour;

    void SpawnReagent(GameObject target)
    {
        // Set bottle prefab and colors based on reagent parameters
        if (handlerType == HandlerType.Bottle)
        {
            bottle = Instantiate(GetBottle(bottleType)) as GameObject;
            Destroy(bottle.GetComponentInChildren<BoxCollider>());
            reagentPour = Instantiate(pourStream) as GameObject;
            reagentPour.transform.SetParent(bottle.transform.Find("Origin"), false);
            var reagentPourParticle = reagentPour.GetComponent<ParticleSystem>().main;
            reagentPourParticle.startColor = pourColor;

            bottle.transform.SetParent(target.GetComponent<Transform>(), false);
            SetBottleMaterialProperties();
        }
    }

    void DestroyReagent()
    {
        DestroyImmediate(bottle);
    }

    private void Start()
    {
        if (!createMode)
        {
            SpawnReagent(this.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Backwards compatibility for the slider controller
        if (handlerType == HandlerType.Slider)
        {
            flowRate = GetComponent<Slider>().value;
            cauldron.AddReagent(this);
        }
        else if (flowRate > 0.0F)
        {
            cauldron.AddReagent(this);
        }

        if (createMode && bottle != null)
        {
            SetBottleMaterialProperties();
        }
        if ((createMode && bottleType != lastBottleType) || createMode && !lastCreateMode)
        {
            DestroyReagent();
            lastBottleType = bottleType;
            bottle = Instantiate(GetBottle(bottleType)) as GameObject;
            bottle.transform.SetParent(transform, false);
            SetBottleMaterialProperties();
        }
        if (!createMode && lastCreateMode)
        {
            DestroyReagent();
        }
        lastCreateMode = createMode;
    }
}