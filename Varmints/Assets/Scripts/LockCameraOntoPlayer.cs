using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraOntoPlayer : MonoBehaviour
{
    public Transform cameraLock;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraLock.position;
    }
}
