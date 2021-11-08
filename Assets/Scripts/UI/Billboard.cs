using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{   public Transform cam;

    private void Awake() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
