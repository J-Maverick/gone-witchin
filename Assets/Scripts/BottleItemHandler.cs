using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BottleType
{
    bottleBanana, bottleBigBottom, bottleBigBottom2, bottleBigTop, bottleBigTop2, bottleBolas,
    bottleFatFlask, bottleHandleBigJug, bottleHandleNormal, bottleHeart, bottleNormal, bottleNormalLabel,
    bottleNormalSquare, bottleNormalSquareLabel, bottleO, bottleShortPhial, bottleShortSphere, bottleShortSquare,
    bottleShortTopTriangle, bottleShortTriangle, bottleSkinnyTriangle, bottleSoda, bottleSphere, bottleSphereLabel,
    bottleWideFlask, bottleWideFlaskLabel
}

public class BottleItemHandler : ItemHandler
{

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

    protected GameObject bottle;

    protected void SetBottleMaterialProperties()
    {
        Material[] materials = bottle.GetComponentInChildren<SkinnedMeshRenderer>().materials;
        materials[0].SetColor("_Color", bottleColor);
        materials[0].SetFloat("_Metallic", bottleMetallic);
        materials[0].SetFloat("_Glossiness", bottleSmoothness);
        materials[0].SetColor("_EmissionColor", bottleEmissionColor);
        materials[0].EnableKeyword("_EMISSION");

        bottle.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
    }

    // Retrieve bottle prefab based on enumeration
    public GameObject GetBottle(BottleType bottleType)
    {
        return Resources.Load<GameObject>(string.Format("{0}", bottleType));
    }
}
