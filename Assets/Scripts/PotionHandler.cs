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
public class PotionHandler : BottleItemHandler
{
    public ReagentRecord[] reagents;

    [Header("For in-scene use only -- do not change")]
    public bool createMode = false;

    public string craftingRecipe = "";

    GameObject potionBottle;
    BottleType lastBottleType;
    bool lastCreateMode;

    private void Start()
    {
        if (createMode)
        {
            // Set bottle prefab and colors based on potion parameters
            potionBottle = Instantiate(GetBottle(bottleType)) as GameObject;
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
            potionBottle = Instantiate(GetBottle(bottleType)) as GameObject;
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
        potionBottle = Instantiate(GetBottle(bottleType)) as GameObject;
        potionBottle.GetComponent<TooltipHandler>().tooltipText = tooltipText;
        potionBottle.GetComponent<TooltipHandler>().itemName = itemName;
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
        string recipeString = string.Format("{0} = ", itemName);
        foreach (ReagentRecord reagentRecord in reagents)
        {
            if (recipeString != string.Format("{0} = ", itemName))
            {
                recipeString += " + ";
            }
            if (reagentRecord.numParts > 1)
            {
                recipeString += string.Format("{0} parts {1}", reagentRecord.numParts, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().itemName);
            }
            else
            {
                recipeString += string.Format("{0} part {1}", reagentRecord.numParts, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().itemName);
            }
        }

        return recipeString;

    }
}
