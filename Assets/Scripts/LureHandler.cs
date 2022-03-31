using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LureHandler : MonoBehaviour, IPointerClickHandler
{
    public float lureRadius;
    public List<FishSwimHandler> fishies;
    public Vector3 ghostPosition = Vector3.zero;

    [HideInInspector]
    public bool hovering = false;

    private bool lurePlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Lure clicked.");
        lurePlaced = true;
        transform.Find("Lure").gameObject.SetActive(true);
        transform.Find("GhostLure").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // target fish in a radius around the lure (layer 7 is for fish -- it's a bitmask I guess)
        Collider[] fishiesInZone = Physics.OverlapSphere(transform.position, lureRadius, layerMask: 1 << 7);
        foreach (var fishCollider in fishiesInZone)
        {
            fishCollider.SendMessage("TargetLure", Random.Range(0f, 1f));
        }

        moveLure();
    }

    public void moveLure()
    {
        if (!lurePlaced)
        {
            transform.position = ghostPosition;
        }

    }

    public void AddFish(FishSwimHandler fish)
    {
        fishies.Add(fish);
    }

    public void RemoveFish(FishSwimHandler fish)
    {
        fishies.Remove(fish);
    }
}
