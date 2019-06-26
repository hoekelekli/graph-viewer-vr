using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    /// <summary>
    /// List consisting of all edges.
    /// </summary>
    public List<Edge> edges = new List<Edge>();

    /// <summary>
    /// List consisting of all nodes.
    /// </summary>
    public List<Node> nodes = new List<Node>();

    public Graph(List<Node> nodes, List<Edge> edges)
    {
        this.edges = edges;
        this.nodes = nodes;
    }
}

