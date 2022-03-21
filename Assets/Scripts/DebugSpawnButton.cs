using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugSpawnButton : MonoBehaviour
{
    public CauldronHandler cauldron;
    public RecipeList recipeList;
    public TMP_Dropdown debugDropdown;

    public void DebugSpawn()
    {
        string potionName = debugDropdown.options[debugDropdown.value].text;
        Debug.Log(string.Format("Spawning {0}", potionName));
        foreach (GameObject potion in recipeList.potions)
        {
            if (potion.GetComponent<PotionHandler>().potionName == potionName)
            {
                cauldron.Reset();
                cauldron.SetPotionCrafted(potion);
                cauldron.TriggerSuccess();
                break;
            }
        }
    }
}
