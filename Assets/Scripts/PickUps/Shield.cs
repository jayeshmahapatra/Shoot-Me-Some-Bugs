using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{   public float speed = 5f;
    public float radius = 1f;
    public Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        transform.RotateAround(target.position,Vector3.forward, speed * Time.fixedDeltaTime);
        
    }
}
