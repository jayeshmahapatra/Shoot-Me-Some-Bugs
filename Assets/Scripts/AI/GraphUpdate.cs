using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateGraph", 0.5f, 0.5f);
    }


    void UpdateGraph()
    {
        // Recalculate all graphs
    AstarPath.active.Scan();
    }
}
