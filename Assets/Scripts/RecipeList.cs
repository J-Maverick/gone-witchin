using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class RecipeList : MonoBehaviour
{
    public PotionHandler[] potions;
    public float partSize = 0.1F;
    public string recipeStrings;

    private void Start()
    {
        recipeStrings = "";
        foreach (PotionHandler potion in potions)
        {
            recipeStrings += string.Format("{0}\n", potion.GetCraftingRecipeString());
        }
    }

    // Checks if cauldron ingredients can make a unique potion type, returns potion game object or null if no potion (or multiple unique potions) can be made
    public PotionHandler CheckRecipes(List<Reagent> cauldronReagents)
    {
        Debug.Log("Checking Recipe...");
        int nPotionsCraftable = 0;
        PotionHandler potionToCraft = null;

        List<string> potionRecords = new List<string>();

        // Loop through recipes
        foreach (PotionHandler potion in potions)
        {
            bool potionCraftable = true;
            List<string> recordNames = new List<string>();
            // Loop through reagents in recipe
            foreach (ReagentRecord reagentRecord in potion.reagents)
            {
                bool enoughReagents = false;
                string recordName = reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName;
                // Loop through reagents in cauldron
                foreach (Reagent reagent in cauldronReagents)
                {

                    if (recordName == reagent.reagentName && reagent.fillLevel > 0.01)
                    {
                        Debug.Log(string.Format("Checking for {0} in {1}", reagent.reagentName, potion.potionName));

                        // Check if reagent has sufficient fill level to produce potion
                        if (reagentRecord.numParts * partSize < reagent.fillLevel)
                        {
                            enoughReagents = true;
                            recordNames.Add(recordName);
                        }
                        else
                        {
                            Debug.Log(string.Format("Couldn't Craft {0} || reagentRecord.numParts: {1} || partSize: {2} || reagentMin: {3} || reagent.fillLevel: {4} || reagentName: {5}",
                            potion.potionName, reagentRecord.numParts, partSize, reagentRecord.numParts * partSize, reagent.fillLevel, reagent.reagentName));
                        }
                    }
                }
                if (!enoughReagents)
                {
                    Debug.Log(string.Format("Couldn't Craft {0}, not enough {1}",
                        potion.potionName, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName));
                    potionCraftable = false;
                    break;
                }
            }
            // If potion is craftable, save it
            if (potionCraftable)
            {
                if (ListContainsAllItems(potionRecords, recordNames) && nPotionsCraftable == 1)
                { 
                    Debug.Log(string.Format("Craftable Potion: {0} || Ignoring", potion.potionName));
                }
                else if (ListContainsAllItems(recordNames, potionRecords) && nPotionsCraftable == 1)
                {
                    Debug.Log(string.Format("Craftable Potion: {0} || Overriding", potion.potionName));
                    potionToCraft = potion;
                    potionRecords = recordNames;
                }
                else
                {
                    Debug.Log(string.Format("Craftable Potion: {0}", potion.potionName));
                    nPotionsCraftable += 1;
                    potionToCraft = potion;
                    potionRecords = recordNames;
                }
            }
            // If more than one type of potion can be made from the cauldron ingredients, fail
            if (nPotionsCraftable > 1)
            {
                Debug.Log(string.Format("Potion Failed: {0} valid potions found", nPotionsCraftable));
                return null;
            }
        }

        // Return a valid potion only if a single unique type potion is craftable
        if (nPotionsCraftable == 1)
        {
            return potionToCraft;
        }
        else
        {
            return null;
        }
    }

    bool ListContainsAllItems(List<string> mainList, List<string> minorList)
    {
        bool allItemsContained = true;
        foreach (string item in minorList)
        {
            if (!mainList.Contains(item))
            {
                allItemsContained = false;
                break;
            }
        }
        return allItemsContained;
    }
}
