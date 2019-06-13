using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    private string sourceId;
    private string destinationId;
    private float weight;

    public GameObject sourceNode;
    public GameObject destinationNode;

    public Edge(string sourceId, string destinationId, float weight)
    {
        this.sourceId = sourceId;
        this.destinationId = destinationId;
        this.weight = weight;
    }

    public string getSourceId()
    {
        return sourceId;
    }

    public string getDestinationId()
    {
        return destinationId;
    }   

    public float getWeight()
    {
        return weight;
    }  

    public void setSourceId(string sourceId)
    {
        this.sourceId = sourceId;
    }

    public void setDestinationId(string destinationId)
    {
        this.destinationId = destinationId; 
    }

    public void setWeight(float weight)
    {
        this.weight = weight;
    }
}
