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
    public GameObject player;
    public string inputFile;
    
    private List<Edge> edges = new List<Edge>();
    private List<Node> nodes = new List<Node>();

    void Awake()
    {
        parseXML();
    }

    // Start is called before the first frame update
    void Start()
    {
        drawNodes();
        drawEdges();
    }

    void Update()
    {
        foreach(Node node in nodes)
        {
            Vector3 pos = 2 * node.getLabel().transform.position - player.transform.position;
            node.getLabel().transform.LookAt(pos);    
        }
    }

    private void parseXML()
    {
        // parse inputFile and save nodes and vertices.
        TextAsset t = Resources.Load(inputFile) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(t.text);

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
                            size = string.Equals(size.ToString("R"), "NaN") ? 1.0f : size;

                            float red = float.Parse(child.ChildNodes[2].InnerText, CultureInfo.InvariantCulture);
                            float green = float.Parse(child.ChildNodes[3].InnerText, CultureInfo.InvariantCulture);
                            float blue = float.Parse(child.ChildNodes[4].InnerText, CultureInfo.InvariantCulture);
                            float[] rgb = {red, green, blue};

                            float x = float.Parse(child.ChildNodes[5].InnerText, CultureInfo.InvariantCulture);
                            float y = float.Parse(child.ChildNodes[6].InnerText, CultureInfo.InvariantCulture);
                            float z = float.Parse(child.ChildNodes[7].InnerText, CultureInfo.InvariantCulture);
                            float[] xyz = {x, y, z};
                            
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
            float x = node.getXyz()[0] * 10;
            float y = node.getXyz()[1] * 10;
            float z = node.getXyz()[2] * 10;

            float red = node.getRgb()[0] / 255;
            float green = node.getRgb()[1] / 255;
            float blue = node.getRgb()[2] / 255;

            float size = node.getSize();

            var n = Instantiate(ball, new Vector3(x, y, z), Quaternion.identity);
            n.GetComponent<Renderer>().material.color = new Color(red, green, blue, 1.0f);
            n.transform.localScale = new Vector3(size, size, size);

            // set collider size
            n.GetComponent<SphereCollider>().radius = node.getSize() * 0.5f;

            var m = Instantiate(nodeText, n.transform.position, Quaternion.identity);
            TextMesh mesh = m.GetComponent<TextMesh>();
            mesh.text = node.getId();
            mesh.transform.localScale = n.transform.localScale / 5;
            mesh.fontSize = 20;
            node.setLabel(mesh);
            
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
        foreach (Edge edge in edges)
        {
            float lineScale = (float)2 / nodes.Count;
            float arrowScale = Math.Min((float)50 / nodes.Count, 2.0f);

            GameObject newLineGenerator = Instantiate(lineGenerator);
            LineRenderer lineRenderer = newLineGenerator.GetComponent<LineRenderer>();
            lineRenderer.startWidth = lineScale;
            lineRenderer.endWidth = lineScale;
            lineRenderer.SetPosition(0, edge.sourceNode.transform.position);
            lineRenderer.SetPosition(1, edge.destinationNode.transform.position);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            /*
            Todo: add arrowheads later
            vector3 nodeposition = edge.sourcenode.transform.position;
            vector3 headposition = edge.destinationnode.getcomponent<spherecollider>().closestpointonbounds(nodeposition);
            vector3 directionvector = edge.destinationnode.transform.position - edge.sourcenode.transform.position;
            var arrow = instantiate(arrowhead, headposition, quaternion.lookrotation(directionvector));
            arrow.transform.localscale = new vector3(arrowscale, arrowscale, arrowscale);
            */
        }
    }
}
