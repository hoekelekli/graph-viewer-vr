using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Node source;
    private Node destination;
    private float weight;

    public Edge(Node source, Node destination, float weight)
    {
        this.source = source;
        this.destination = destination;
        this.weight = weight;
    }

    public Node getSource()
    {
        return source;
    }

    public Node getDestination()
    {
        return destination;
    }   

    public float getWeight()
    {
        return weight;
    }  

    public void setSource(Node source)
    {
        this.source = source;
    }

    public void setDestination(Node destination)
    {
        this.destination = destination; 
    }

    public void setWeight(float weight)
    {
        this.weight = weight;
    }
}
