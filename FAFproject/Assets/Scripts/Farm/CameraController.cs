using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GameObject.Find("Player").transform.position;
        pos.z -= 10f;
        transform.position = pos;
    }
}
