using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class RecipeList : MonoBehaviour
{
    public GameObject[] potions;
    public float partSize = 0.1F;
    public string recipeStrings;

    private void Start()
    {
        recipeStrings = "";
        foreach (GameObject potion in potions)
        {
            recipeStrings += string.Format("{0}\n", potion.GetComponent<PotionHandler>().GetCraftingRecipeString());
        }
    }

    // Checks if cauldron ingredients can make a unique potion type, returns potion game object or null if no potion (or multiple unique potions) can be made
    public GameObject CheckRecipes(List<Reagent> cauldronReagents)
    {
        Debug.Log("Checking Recipe...");
        int nPotionsCraftable = 0;
        GameObject potionToCraft = null;

        List<string> potionRecords = new List<string>();

        // Loop through recipes
        foreach (GameObject potion in potions)
        {
            bool potionCraftable = true;
            List<string> recordNames = new List<string>();
            // Loop through reagents in recipe
            foreach (ReagentRecord reagentRecord in potion.GetComponent<PotionHandler>().reagents)
            {
                bool enoughReagents = false;
                string recordName = reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName;
                // Loop through reagents in cauldron
                foreach (Reagent reagent in cauldronReagents)
                {

                    if (recordName == reagent.reagentName && reagent.fillLevel > 0.01)
                    {
                        Debug.Log(string.Format("Checking for {0} in {1}", reagent.reagentName, potion.GetComponent<PotionHandler>().potionName));

                        // Check if reagent has sufficient fill level to produce potion
                        if (reagentRecord.numParts * partSize < reagent.fillLevel)
                        {
                            enoughReagents = true;
                            recordNames.Add(recordName);
                        }
                        else
                        {
                            Debug.Log(string.Format("Couldn't Craft {0} || reagentRecord.numParts: {1} || partSize: {2} || reagentMin: {3} || reagent.fillLevel: {4} || reagentName: {5}",
                            potion.GetComponent<PotionHandler>().potionName, reagentRecord.numParts, partSize, reagentRecord.numParts * partSize, reagent.fillLevel, reagent.reagentName));
                        }
                    }
                }
                if (!enoughReagents)
                {
                    Debug.Log(string.Format("Couldn't Craft {0}, not enough {1}",
                        potion.GetComponent<PotionHandler>().potionName, reagentRecord.reagentPrefab.GetComponent<ReagentHandler>().reagentName));
                    potionCraftable = false;
                    break;
                }
            }
            // If potion is craftable, save it
            if (potionCraftable)
            {
                if (ListContainsAllItems(potionRecords, recordNames) && nPotionsCraftable == 1)
                { 
                    Debug.Log(string.Format("Craftable Potion: {0} || Ignoring", potion.GetComponent<PotionHandler>().potionName));
                    potionToCraft = potion;
                }
                else if (ListContainsAllItems(recordNames, potionRecords) && nPotionsCraftable == 1)
                {
                    Debug.Log(string.Format("Craftable Potion: {0} || Overriding", potion.GetComponent<PotionHandler>().potionName));
                    potionToCraft = potion;
                }
                else
                {
                    Debug.Log(string.Format("Craftable Potion: {0}", potion.GetComponent<PotionHandler>().potionName));
                    nPotionsCraftable += 1;
                    potionToCraft = potion;
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
