using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using System.Globalization;

public class GraphDrawer : MonoBehaviour
{
    public GameObject ball;
    public GameObject lineGenerator;
    public string inputFile;

    private List<Edge> edges = new List<Edge>();
    private List<Node> nodes = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        // TODO: 
        // renderXML();
        // drawVertices();
        parseXML();
        drawNodes();
        drawVertices();
    }

    private void parseXML()
    {
        // renders inputFile and save nodes and vertices.
        XmlDocument doc = new XmlDocument();
        doc.Load(inputFile);

        foreach (XmlNode xmlNode in doc.DocumentElement)
        {   
            if (xmlNode.Name == "graph")
            {
                foreach(XmlNode child in xmlNode.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "node":
                            string id = child.Attributes[0].Value;
                            float size = float.Parse(child.ChildNodes[1].InnerText, CultureInfo.InvariantCulture);
                            float[] rgb = {float.Parse(child.ChildNodes[2].InnerText, CultureInfo.InvariantCulture), float.Parse(child.ChildNodes[3].InnerText, CultureInfo.InvariantCulture), float.Parse(child.ChildNodes[4].InnerText, CultureInfo.InvariantCulture)};
                            float[] xyz = {float.Parse(child.ChildNodes[5].InnerText, CultureInfo.InvariantCulture), float.Parse(child.ChildNodes[6].InnerText, CultureInfo.InvariantCulture), float.Parse(child.ChildNodes[7].InnerText, CultureInfo.InvariantCulture)};
                            nodes.Add(new Node(id, size, rgb, xyz));
                            break;
                        case "edge":
                            string sourceId =  child.Attributes[0].Value;
                            string destinationId =  child.Attributes[1].Value;
                            float weight = float.Parse(child.ChildNodes[0].InnerText, CultureInfo.InvariantCulture);
                            edges.Add(new Edge(sourceId, destinationId, weight));
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
        // Iterates through all nodes and draw from prefab.
        foreach(Node node in nodes)
        {
            var n = Instantiate(ball, new Vector3(node.getXyz()[0] * 10, node.getXyz()[1] * 10, node.getXyz()[2] * 10), Quaternion.identity);
            n.GetComponent<Renderer>().material.color = new Color(node.getRgb()[0]/255, node.getRgb()[1]/255, node.getRgb()[2]/255, 1.0f);
            n.transform.localScale = new Vector3(node.getSize(), node.getSize(), node.getSize());

            foreach(Edge edge in edges)
            {
                // laesst Schleifen zu
                if(string.Equals(edge.getSourceId(), node.getId()))
                {
                    edge.sourceNode = n.transform;
                }
                if(string.Equals(edge.getDestinationId(), node.getId()))
                {
                    edge.destinationNode = n.transform;
                }
            }
        }
    }

    private void drawVertices()
    {
        foreach(Edge edge in edges)
        {
            GameObject newLineGenerator = Instantiate(lineGenerator);
            LineRenderer lineRenderer = newLineGenerator.GetComponent<LineRenderer>();
            lineRenderer.SetWidth(0.001f, 0.001f);
            lineRenderer.SetPosition(0, edge.sourceNode.position);
            lineRenderer.SetPosition(1, edge.destinationNode.position);
        }
    }
}
