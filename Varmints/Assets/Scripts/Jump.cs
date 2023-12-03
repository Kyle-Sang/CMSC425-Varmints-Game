using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 2.0f;
    public float height;
    public float jumpFreq;

    Rigidbody rb;
    private Vector3 jump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, height, 0);

        InvokeRepeating("Move", 0, jumpFreq);

    }

    // Update is called once per frame


    private void Move()
    {
     
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
    }
}

