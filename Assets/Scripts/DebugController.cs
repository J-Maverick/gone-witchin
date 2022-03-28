using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugController : MonoBehaviour
{
    [Space(10)]
    public bool debug = false;
    [Space(10)]
    public RecipeList recipeList;
    public GameObject debugMenu;

    [SerializeField]
    TMP_Dropdown debugPotionList;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
        List<string> potions = new List<string>();
        debugPotionList.ClearOptions();
        foreach (PotionHandler potion in recipeList.potions)
        {

            potions.Add(potion.itemName);
        }
        debugPotionList.AddOptions(potions);
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            debugMenu.SetActive(true);
        }
        else
        {
            debugMenu.SetActive(false);
        }
    }
}
