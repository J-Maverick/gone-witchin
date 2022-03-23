using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public GameObject recipeBook;
    public DragAndDrop playerDrag;

    public void ToggleRecipeBook()
    {
        if (recipeBook.activeSelf)
        {
            recipeBook.SetActive(false);
            playerDrag.draggingAllowed = true;
        }
        else if (!recipeBook.activeSelf)
        {
            recipeBook.SetActive(true);
            playerDrag.draggingAllowed = false;
        }
    }
}
