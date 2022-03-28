using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronHandler : MonoBehaviour
{
    [Range(0, 1)]
    public float fillLevel = 0.0F;
    public float maxFill = 1.0F;
    [Range(0, 1)]
    public float temperature = 0.3F;

    public GameObject rotationBone;
    public GameObject radiusBone;
    public RecipeList recipeList;

    public Animator fitchAnimator;
    public DragAndDrop playerDragAndDrop;
    public Animator musicController;

    public GameObject potionTarget;

    Material[] materials;
    Transform[] transforms;
    Animator animator;
    List<Reagent> reagents = new List<Reagent>();
    ParticleSystem smoke;
    Color startingSmokeColor;
    Color smokeColor;
    PotionHandler potionCrafted = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        materials = GetComponentInChildren<SkinnedMeshRenderer>().materials;
        smoke = transform.Find("Smoke").GetComponent<ParticleSystem>();
        startingSmokeColor = smoke.main.startColor.color;

        musicController.SetBool("isCooking", true);

        materials[2].EnableKeyword("_EMISSION");
        materials[3].EnableKeyword("_EMISSION");
        materials[4].EnableKeyword("_EMISSION");
        materials[6].EnableKeyword("_EMISSION");
        materials[5].EnableKeyword("_EMISSION");

        StartCoroutine(debugLogReagents());
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("FillLevel", fillLevel);
        animator.SetFloat("Temperature", fillLevel);



        materials[1].SetColor("_Color", DetermineTemperatureColor(fillLevel));
        GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;

        SetGemColors();

        SetSmokeColor();

        // Debug.Log(string.Format("radius: {0}, angle: {1}", GetLipRadius(), GetLipRotation()));


    }

    IEnumerator debugLogReagents()
    {
        while (true)
        { 
            foreach (Reagent reagent in reagents)
            {
                Debug.Log(string.Format("Amount of {0}: {1}", reagent.name, reagent.fillLevel));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Set color based on temperate value from 0-1
    private Color DetermineTemperatureColor(float temperature)
    {
        Color tempColor;
        if (temperature <= 0.5)
        {
            tempColor = Color.Lerp(Color.blue, Color.green, temperature * 2F);
        }
        else
        {
            tempColor = Color.Lerp(Color.green, Color.red, (temperature - 0.5F) * 2F);
        }

        return tempColor;
    }

    private void SetSmokeColor()
    {
        smokeColor = startingSmokeColor;
        foreach (Reagent reagent in reagents)
        {
            smokeColor = Color.Lerp(smokeColor, reagent.reagentColor, reagent.fillLevel * 0.2F);
        }
        var smokeMain = smoke.main;
        smokeMain.startColor = smokeColor;
    }

    // Makes the gems around the rim glow based on fill level
    private void SetGemColors()
    {
        if (fillLevel >= 0.01F)
        {
            materials[2].SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.green, 0.25F));
        }
        if (fillLevel >= 0.25F)
        {
            materials[3].SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.green, 0.25F));
        }
        if (fillLevel >= 0.5F)
        {
            materials[4].SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.green, 0.25F));
        }
        if (fillLevel >= 0.75F)
        {
            materials[6].SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.green, 0.25F));
        }
        if (fillLevel >= 0.99F)
        {
            materials[5].SetColor("_EmissionColor", Color.Lerp(Color.blue, Color.green, 0.25F));
        }
        if (fillLevel < 0.01F)
        {
            materials[2].SetColor("_EmissionColor", Color.black);
            materials[3].SetColor("_EmissionColor", Color.black);
            materials[4].SetColor("_EmissionColor", Color.black);
            materials[6].SetColor("_EmissionColor", Color.black);
            materials[5].SetColor("_EmissionColor", Color.black);
        }
    }
    
    // Retrieve radius of lip bone
    private float GetLipRadius()
    {
        return radiusBone.transform.localPosition.y;
    }

    private float GetLipRotation()
    {
        return rotationBone.transform.localEulerAngles.y;
    }

    // Convert linear fill value to equivalent rotation and return 0-1 value for rotation animation application
    private float LinearConversion()
    {
        float theta;
        float x;
        float y;
        float radius = GetLipRadius();
        float linearLength = fillLevel * radius * 2.0F;
        x = (radius - linearLength) / radius;
        theta = - (Mathf.Asin(x) - Mathf.PI / 2);
        y = theta / Mathf.PI;
        Debug.Log(string.Format("fillLevel: {0} || radius: {1} || linearLength: {2} || x: {3} || theta: {4} || y: {5}", fillLevel, radius, linearLength, x, theta, y));
        return y;
    }

    // Used by reagent handler to add reagents to the cauldron
    public void AddReagent(ReagentHandler reagentHandler)
    {
        bool matchedReagent = false;
        foreach (Reagent reagent in reagents)
        {
            if (reagent.IsMatch(reagentHandler))
            {
                Debug.Log(string.Format("Adding {0} to the {1} levels", reagentHandler.itemName, reagent.name));
                AddReagentFill(reagent, reagentHandler);
                matchedReagent = true;
                break;
            }
        }
        if (!matchedReagent)
        {
            Reagent reagent = new Reagent(reagentHandler);
            AddReagentFill(reagent, reagentHandler);
            reagents.Add(reagent);
        }

    }

    // Add reagent to the cauldron, preserving hard max
    public void AddReagentFill(Reagent reagent, ReagentHandler reagentHandler)
    {
        float fillAmount = reagentHandler.flowRate * 0.02f;
        fillLevel += fillAmount;
        if (fillLevel > maxFill)
        {
            fillAmount = fillAmount - (fillLevel - maxFill);
            fillLevel = maxFill;
        }
        reagent.AddFill(fillAmount);
    }

    // Add reagent to the cauldron, preserving hard max
    public void AddHeat(FireHandler fireHandler)
    {
        float heatAmount = fireHandler.heatingRate * Time.deltaTime;
        temperature += heatAmount;
        if (temperature > 1.0)
        {
            temperature = 1.0F;
        }
        else if (temperature < 0.0)
        {
            temperature = 0.0F;
        }
    }


    public void CheckSuccess()
    {
        Debug.Log(reagents);
        foreach (Reagent reagent in reagents)
        {
            Debug.LogFormat("Reagent {0} present in cauldron reagent list", reagent.name);
        }
        potionCrafted = recipeList.CheckRecipes(reagents);
        Debug.Log(string.Format("Checking Success: potionCrafted: {0}", potionCrafted));
        if (potionCrafted == null)
        {
            Debug.Log("Potion Failed");
            TriggerFailure();
        }
        else
        {
            Debug.Log("Potion Succeeded!");
            TriggerSuccess();
        }
    }

    public void SetPotionCrafted(PotionHandler potionToSet)
    {
        potionCrafted = potionToSet;
    }

    public void TriggerFailure()
    {
        fitchAnimator.SetBool("onFire", true);
        musicController.SetBool("onFire", true);
        playerDragAndDrop.draggingAllowed = false;
    }

    public void TriggerSuccess()
    {
        fitchAnimator.SetBool("isVictory", true);
        musicController.SetBool("isVictory", true);
        potionTarget.transform.parent.parent.GetComponent<Animator>().SetBool("isVictory", true);
        potionCrafted.SpawnPotion(potionTarget);
        playerDragAndDrop.draggingAllowed = false;
    }

    public void Reset()
    {
        temperature = 0.3F;
        fillLevel = 0.0F;
        fitchAnimator.SetBool("isVictory", false);
        fitchAnimator.SetBool("onFire", false);
        musicController.SetBool("isVictory", false);
        potionTarget.transform.parent.parent.GetComponent<Animator>().SetBool("isVictory", false);
        musicController.SetBool("onFire", false);

        reagents = new List<Reagent>();
        playerDragAndDrop.draggingAllowed = true;
        if (potionCrafted != null)
        {
            potionCrafted.DestroyPotion();
            potionCrafted = null;
        }
    }
}

// Class used for storing reagents currently in the cauldron
public class Reagent
{
    public string name { get; }
    public float fillLevel = 0F;
    public Color reagentColor;

    public Reagent(ReagentHandler reagentHandler)
    {
        name = reagentHandler.itemName;
        reagentColor = reagentHandler.pourColor;
    }

    public void AddFill(float fill)
    {
        fillLevel += fill;
    }

    public bool IsMatch(ReagentHandler reagentHandler)
    {
        if (reagentHandler.itemName == name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}