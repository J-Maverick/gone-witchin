using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeDemo : MonoBehaviour
{

    string myString = "Nice";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myString = MyMethod(myString);
        print(myString);
    }

    string MyMethod(string inputString)
    {
        return inputString + "!";
    }
}
