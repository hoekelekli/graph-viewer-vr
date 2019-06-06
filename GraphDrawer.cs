using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

public class GraphDrawer : MonoBehaviour
{
    private List<Edge> edges = new List<Edge>();
    private List<Node> nodes = new List<Node>();
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: 
        // renderXML();
        // drawNodes();
        // drawVertices();
        parseXML();
    }

    private void parseXML()
    {
        // TODO: render inputFile and save nodes and vertices.
        XmlDocument doc = new XmlDocument();
        doc.Load("Assets/Resources/gephi_output.xml");

        foreach (XmlNode xmlNode in doc.DocumentElement)
        {   
            if (xmlNode.Name == "graph")
            {
                foreach(XmlNode child in xmlNode.ChildNodes)
                {
                    Debug.Log(child.Attributes[0].Value);

                    switch (child.Name)
                    {
                        case "node":
                            nodes.Add(
                                new Node(
                                child.Attributes[0].Value, 
                                child.ChildNodes[1].InnerText, 
                                {child.ChildNodes[2].InnerText, child.ChildNodes[3].InnerText, child.ChildNodes[4].InnerText}, 
                                {child.ChildNodes[6].InnerText, child.ChildNodes[6].InnerText, child.ChildNodes[7].InnerText}));
                            break;
                        case "edge":
                            edges.Add(new Edge(
                                child.Attributes[0].Value, 
                                child.Attributes[1].Value,
                                child.ChildNodes[2].InnerText));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void drawNodes()
    {
        // TODO: Iterate through all nodes and draw from prefab.
    }

    private void drawVertices()
    {
        // TODO: Iterate through all vertices and draw lines.
        // lineRenderer = GetComponent<LineRenderer>();
        // lineRenderer.SetWidth(0.1f, 0.1f);
        // lineRenderer.SetPosition(0, origin.position);
        // lineRenderer.SetPosition(1, destination.position);
    }
}
