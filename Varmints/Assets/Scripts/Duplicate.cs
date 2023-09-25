using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 5.0f, 30.0f);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(this);
        Instantiate(this);
    }
}
