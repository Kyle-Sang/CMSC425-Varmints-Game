using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dest : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 10.0f;
    void Start()
    {
        Destroy(gameObject, lifetime);   
    }
}
