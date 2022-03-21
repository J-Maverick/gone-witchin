using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public GameObject recipeBook;
    public GameObject player;

    public void ToggleRecipeBook()
    {
        if (recipeBook.activeSelf)
        {
            recipeBook.SetActive(false);
            player.GetComponent<DragAndDrop>().draggingAllowed = true;
        }
        else if (!recipeBook.activeSelf)
        {
            recipeBook.SetActive(true);
            player.GetComponent<DragAndDrop>().draggingAllowed = false;
        }
    }
}
