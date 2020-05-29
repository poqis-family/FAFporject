using UnityEngine;
using System.Collections;
using Pathfinding;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
public class AstarSupport : MonoBehaviour
{
    public float radius = 1000;
    IAstarAI ai;
    GraphNode randomNode;
    public void Start()
    {
        ai = GetComponent<IAstarAI>();
        var grid = AstarPath.active.data.gridGraph;
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
    }
    Vector3 PickRandomPoint()
    {
        var grid = AstarPath.active.data.gridGraph;
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        var destination1 = (Vector3)randomNode.position;
        return destination1;
    }
    public void Update()
    {
                // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
        {
            ai.destination = PickRandomPoint();
            ai.SearchPath();
        }
        AstarPath.active.Scan();
    }
}
