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
    private Graph graph;
    public GameObject ball;
    public GameObject lineGenerator;
    public GameObject nodeText;
    public GameObject arrowHead;
    public GameObject player;

    /// <summary>
    /// This variable can be changed within the Unity GUI.
    /// Keep in mind, that the extension has to be.xml and the extension.graphml is not supported.
    /// </summary>
    public string inputFile;

    /// <summary>
    /// This constant is multiplied with the coordinates of the nodes.
    /// </summary>
    public static readonly float CoordinateScale = 10f;
    

    /**
     * <summary>Parses the graph before the game starts.</summary>
     */
    void Awake()
    {
        graph = XmlParser.GetParsedGraph(inputFile);
    }

    /**
     * <summary>Draws nodes and edges when game is starting.</summary>
     */
    void Start()
    {
        DrawNodes();
        DrawEdges();
    }

    /**
     * <summary>Updates label rotation based on the player position in each frame.</summary>
     */
    void Update()
    {
        foreach(Node node in graph.nodes)
        {
            Vector3 pos = 2 * node.label.transform.position - player.transform.position;
            node.label.transform.LookAt(pos);    
        }
    }

    /**
     * <summary>Iterates through all nodes and plots them into 3D world space.</summary>
     */
    public void DrawNodes()
    {
        // Iterate through all nodes and draw from prefab.
        foreach(Node node in graph.nodes)
        {
            float x = node.xyz[0] * CoordinateScale;
            float y = node.xyz[1] * CoordinateScale;
            float z = node.xyz[2] * CoordinateScale;

            float red = node.rgb[0];
            float green = node.rgb[1];
            float blue = node.rgb[2];

            float size = node.size;

            var n = Instantiate(ball, new Vector3(x, y, z), Quaternion.identity);
            n.GetComponent<Renderer>().material.color = new Color(red, green, blue, 1.0f);
            n.transform.localScale = new Vector3(size, size, size);

            // set collider size
            n.GetComponent<SphereCollider>().radius = node.size * 0.5f;

            var m = Instantiate(nodeText, n.transform.position, Quaternion.identity);
            TextMesh mesh = m.GetComponent<TextMesh>();
            mesh.text = node.id;
            mesh.transform.localScale = n.transform.localScale / 5;
            mesh.fontSize = 20;
            node.label = mesh;
            
            // set source and destination ids
            foreach(Edge edge in graph.edges)
            {
                // this allows loops
                if(string.Equals(edge.sourceId, node.id))
                {
                    edge.sourceNode = n;
                }
                if(string.Equals(edge.destinationId, node.id))
                {
                    edge.destinationNode = n;
                }
            }
        }
    }

    /**
     * <summary>Iterates through all edges and connects source and target nodes.</summary>
     */
    public void DrawEdges()
    {
        foreach (Edge edge in graph.edges)
        {
            float lineScale = (float)2 / graph.nodes.Count;

            GameObject newLineGenerator = Instantiate(lineGenerator);
            LineRenderer lineRenderer = newLineGenerator.GetComponent<LineRenderer>();
            lineRenderer.startWidth = lineScale; // multiply with edge.weight
            lineRenderer.endWidth = lineScale; // multiply with edge.weight
            lineRenderer.SetPosition(0, edge.sourceNode.transform.position);
            lineRenderer.SetPosition(1, edge.destinationNode.transform.position);
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }
    }
}
