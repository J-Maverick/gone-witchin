using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureHandler : MonoBehaviour
{
    public float lureRadius;
    public List<FishSwimHandler> fishies;

    // Start is called before the first frame update
    void Start()
    {
        
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
