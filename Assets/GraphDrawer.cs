using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;

public class GraphDrawer : MonoBehaviour
{
    public GameObject ball;
    public GameObject lineGenerator;
    public GameObject nodeText;
    public GameObject arrowHead;
    public string inputFile;

    private List<Edge> edges = new List<Edge>();
    private List<Node> nodes = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        parseXML();
        drawNodes();
        drawEdges();
    }

    private void parseXML()
    {
        // parse inputFile and save nodes and vertices.
        XmlDocument doc = new XmlDocument();
        doc.Load(inputFile);

        foreach (XmlNode xmlNode in doc.DocumentElement)
        {   
            // we only need the graph section, not the header
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
                    }
                }
            }
        }
    }

    private void drawNodes()
    {
        // Iterate through all nodes and draw from prefab.
        foreach(Node node in nodes)
        {
            var n = Instantiate(ball, new Vector3(node.getXyz()[0] * 10, node.getXyz()[1] * 10, node.getXyz()[2] * 10), Quaternion.identity);
            n.GetComponent<Renderer>().material.color = new Color(node.getRgb()[0]/255, node.getRgb()[1]/255, node.getRgb()[2]/255, 1.0f);
            // n.transform.localScale = new Vector3(node.getSize(), node.getSize(), node.getSize());

            var m = Instantiate(nodeText, n.transform.position, Quaternion.identity);
            TextMesh mesh = m.GetComponent<TextMesh>();
            mesh.text = node.getId();
            mesh.transform.localScale = n.transform.localScale / 5;
            mesh.fontSize = 20;
            
            // set source and destination ids
            foreach(Edge edge in edges)
            {
                // this allows loops
                if(string.Equals(edge.getSourceId(), node.getId()))
                {
                    edge.sourceNode = n;
                }
                if(string.Equals(edge.getDestinationId(), node.getId()))
                {
                    edge.destinationNode = n;
                }
            }
        }
    }

    private void drawEdges()
    {
        foreach(Edge edge in edges)
        {
            GameObject newLineGenerator = Instantiate(lineGenerator);
            LineRenderer lineRenderer = newLineGenerator.GetComponent<LineRenderer>();
            lineRenderer.SetWidth(0.1f, 0.1f); // todo: multiply with edge.getWeight()
            lineRenderer.SetPosition(0, edge.sourceNode.transform.position);
            lineRenderer.SetPosition(1, edge.destinationNode.transform.position);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            // add arrowhead
            var arrow = Instantiate(arrowHead, edge.destinationNode.GetComponent<SphereCollider>().ClosestPoint(edge.sourceNode.transform.position), 
                             Quaternion.LookRotation(edge.destinationNode.transform.position - edge.sourceNode.transform.position));
        }
    }
}
