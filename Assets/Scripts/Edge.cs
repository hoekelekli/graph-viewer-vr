using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public string sourceId;
    public string destinationId;
    public float weight;

    public GameObject sourceNode;
    public GameObject destinationNode;

    public Edge(string sourceId, string destinationId, float weight)
    {
        this.sourceId = sourceId;
        this.destinationId = destinationId;
        this.weight = weight;
    }
}
