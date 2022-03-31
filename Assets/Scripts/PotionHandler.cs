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

    BottleType lastBottleType;
    bool lastCreateMode;

    private void Start()
    {
        if (createMode)
        {
            // Set bottle prefab and colors based on potion parameters
            bottle = Instantiate(GetBottle(bottleType)) as GameObject;
            bottle.transform.SetParent(GetComponent<Transform>(), false);
            SetBottleMaterialProperties();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (createMode && bottle != null)
        {
            SetBottleMaterialProperties();
        }
        if ((createMode && bottleType != lastBottleType) || createMode && !lastCreateMode)
        {
            DestroyPotion();
            lastBottleType = bottleType;
            bottle = Instantiate(GetBottle(bottleType)) as GameObject;
            bottle.transform.SetParent(GetComponent<Transform>(), false);
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
        bottle = Instantiate(GetBottle(bottleType)) as GameObject;
        bottle.GetComponent<TooltipHandler>().tooltipText = tooltipText;
        bottle.GetComponent<TooltipHandler>().itemName = itemName;
        bottle.gameObject.tag = "Draggable";
        bottle.transform.SetParent(target.GetComponent<Transform>(), false);
        SetBottleMaterialProperties();
    }

    public void DestroyPotion()
    {
        DestroyImmediate(bottle);
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
