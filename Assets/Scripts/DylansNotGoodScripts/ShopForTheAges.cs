using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopForTheAges : MonoBehaviour
{

    private float chance = 0;
    public float gungaga;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (chance == 0)
        {
            chance += 1; //even number
            Debug.Log($"even {chance}");
        }
        else
        {
            chance -= 1; //odd number
            Debug.Log($"odd {chance}");
        }
    }
}
