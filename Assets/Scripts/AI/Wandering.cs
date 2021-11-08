using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

[HelpURL("http://arongranberg.com/astar/docs/class_wandering_destination_setter.php")]
public class Wandering : MonoBehaviour {
    public float radius = 5;

    IAstarAI ai;

    void Start () {
        ai = GetComponent<IAstarAI>();
    }

    GraphNode PickRandomNode () {
        var grid = AstarPath.active.data.gridGraph;

        var randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];

        return randomNode;
    }

    void Update () {
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) {
            GraphNode Currentnode = AstarPath.active.GetNearest(transform.position).node;
            GraphNode randomNode = PickRandomNode();
            //If the path is Walkable
            if (PathUtilities.IsPathPossible(Currentnode, randomNode))
            {  
                ai.destination = (Vector3)randomNode.position;
                ai.SearchPath();
            }

            
        }
    }
}