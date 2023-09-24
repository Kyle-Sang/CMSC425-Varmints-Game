using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 10.0f, 5.0f);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(this);
        Instantiate(this);
    }
}
