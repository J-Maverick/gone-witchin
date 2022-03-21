using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ReagentRecord
{
    public GameObject reagentPrefab;
    [Range(0,4)]
    public int numParts;
}

//[ExecuteInEditMode]
public class PotionHandler : MonoBehaviour
{
    [Tooltip("Name of potion.")]
    public string potionName = "generic";

    public enum BottleType
    {
        bottleBanana, bottleBigBottom, bottleBigBottom2, bottleBigTop, bottleBigTop2, bottleBolas,
        bottleFatFlask, bottleHandleBigJug, bottleHandleNormal, bottleHeart, bottleNormal, bottleNormalLabel,
        bottleNormalSquare, bottleNormalSquareLabel, bottleO, bottleShortPhial, bottleShortSphere, bottleShortSquare,
        bottleShortTopTriangle, bottleShortTriangle, bottleSkinnyTriangle, bottleSoda, bottleSphere, bottleSphereLabel,
        bottleWideFlask, bottleWideFlaskLabel
    }
    [Tooltip("Type of bottle to use.")]
    public BottleType bottleType;

    [Tooltip("Color of the bottle.")]
    public Color bottleColor = Color.black;

    [Tooltip("Bottle emission.")]
    public Color bottleEmissionColor;

    [Tooltip("Bottle metallics.")]
    [Range(0.0F, 1.0F)]
    public float bottleMetallic;

    [Tooltip("Bottle smoothness.")]
    [Range(0.0F, 1.0F)]
    public float bottleSmoothness;

    [Tooltip("Price of the potion at the store.")]
    public int shopValue = 50;

    [TextArea]
    public string tooltipText = "";

    public ReagentRecord[] reagents;

    [Header("For in-scene use only -- do not change")]
    public bool createMode = false;
    

    public string craftingRecipe = "";
    public GameObject bottle1Prefab;
    public GameObject bottle2Prefab;
    public GameObject bottle3Prefab;
    public GameObject bottle4Prefab;
    public GameObject bottle5Prefab;
    public GameObject bottle6Prefab;
    public GameObject bottle7Prefab;
    public GameObject bottle8Prefab;
    public GameObject bottle9Prefab;
    public GameObject bottle10Prefab;
    public GameObject bottle11Prefab;
    public GameObject bottle12Prefab;
    public GameObject bottle13Prefab;
    public GameObject bottle14Prefab;
    public GameObject bottle15Prefab;
    public GameObject bottle16Prefab;
    public GameObject bottle17Prefab;
    public GameObject bottle18Prefab;
    public GameObject bottle19Prefab;
    public GameObject bottle20Prefab;
    public GameObject bottle21Prefab;
    public GameObject bottle22Prefab;
    public GameObject bottle23Prefab;
    public GameObject bottle24Prefab;
    public GameObject bottle25Prefab;
    public GameObject bottle26Prefab;

    GameObject potionBottle;
    BottleType lastBottleType;
    bool lastCreateMode;

    private void SetBottleMaterialProperties()
    {

        Material[] materials = potionBottle.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        materials[0].SetColor("_Color", bottleColor);
        materials[0].SetFloat("_Metallic", bottleMetallic);
        materials[0].SetFloat("_Glossiness", bottleSmoothness);
        materials[0].SetColor("_EmissionColor", bottleEmissionColor);
        materials[0].EnableKeyword("_EMISSION");

        potionBottle.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
    }

    // Retrieve bottle prefab based on enumeration -- hardcoded RIP
    private GameObject GetBottle()
    {
        switch (bottleType)
        {
            case BottleType.bottleBanana:
                return bottle1Prefab;

            case BottleType.bottleBigBottom:
                return bottle2Prefab;

            case BottleType.bottleBigBottom2:
                return bottle3Prefab;

            case BottleType.bottleBigTop:
                return bottle4Prefab;

            case BottleType.bottleBigTop2:
                return bottle5Prefab;

            case BottleType.bottleBolas:
                return bottle6Prefab;

            case BottleType.bottleFatFlask:
                return bottle7Prefab;

            case BottleType.bottleHandleBigJug:
                return bottle8Prefab;

            case BottleType.bottleHandleNormal:
                return bottle9Prefab;

            case BottleType.bottleHeart:
                return bottle10Prefab;

            case BottleType.bottleNormal:
                return bottle11Prefab;

            case BottleType.bottleNormalLabel:
                return bottle12Prefab;

            case BottleType.bottleNormalSquare:
                return bottle13Prefab;

            case BottleType.bottleNormalSquareLabel:
                return bottle14Prefab;

            case BottleType.bottleO:
                return bottle15Prefab;

            case BottleType.bottleShortPhial:
                return bottle16Prefab;

            case BottleType.bottleShortSphere:
                return bottle17Prefab;

            case BottleType.bottleShortSquare:
                return bottle18Prefab;

            case BottleType.bottleShortTopTriangle:
                return bottle19Prefab;

            case BottleType.bottleShortTriangle:
                return bottle20Prefab;

            case BottleType.bottleSkinnyTriangle:
                return bottle21Prefab;

            case BottleType.bottleSoda:
                return bottle22Prefab;

            case BottleType.bottleSphere:
                return bottle23Prefab;

            case BottleType.bottleSphereLabel:
                return bottle24Prefab;

            case BottleType.bottleWideFlask:
                return bottle25Prefab;

            case BottleType.bottleWideFlaskLabel:
                return bottle26Prefab;
        }
        return bottle1Prefab;
    }


    private void Start()
    {
        if (createMode)
        {
            // Set bottle prefab and colors based on potion parameters
            potionBottle = Instantiate(GetBottle()) as GameObject;
            potionBottle.transform.SetParent(GetComponent<Transform>(), false);
            SetBottleMaterialProperties();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (createMode && potionBottle != null)
        {
            SetBottleMaterialProperties();
        }
        if ((createMode && bottleType != lastBottleType) || createMode && !lastCreateMode)
        {
            DestroyPotion();
            lastBottleType = bottleType;
            potionBottle = Instantiate(GetBottle()) as GameObject;
            potionBottle.transform.SetParent(GetComponent<Transform>(), false);
            SetBottleMaterialProperties();
        }
        if (!createMode && lastCreateMode)
        {
            DestroyPotion();
        }
        lastCreateMode = createMode;
    }

    public void SpawnPotion(GameObject target)
    {
        // Set bottle prefab and colors based on potion parameters
        potionBottle = Instantiate(GetBottle()) as GameObject;
        potionBottle.GetComponent<TooltipHandler>().tooltipText = tooltipText;
        potionBottle.GetComponent<TooltipHandler>().itemName = potionName;
        potionBottle.gameObject.tag = "Draggable";
        potionBottle.transform.SetParent(target.GetComponent<Transform>(), false);
        SetBottleMaterialProperties();
    }

    public void DestroyPotion()
    {
        DestroyImmediate(potionBottle);
    }

    public string GetCraftingRecipeString()
    {
        string recipeString = string.Format("{0} = ", potionName);
        foreach (ReagentRecord reagentRecord in reagents)
        {
            if (recipeString != string.Format("{0} = ", potionName))
            {
                recipeString += " + ";
            }
            if (reagentRecord.numParts > 1)
            {
                recipeString += string.Format("{0} parts {1}", reagentRecord.numParts, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName);
            }
            else
            {
                recipeString += string.Format("{0} part {1}", reagentRecord.numParts, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName);
            }
        }

        return recipeString;

    }
}
