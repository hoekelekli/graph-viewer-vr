using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;

/// <summary>
/// Parses the graphml input and constructs a graph object.
/// </summary>
public class XmlParser
{
    /// <summary>
    /// This method parses the given graphml file to construct a Graph.
    /// <param name="inputFile">A string directing to the graphml-file.Keep in mind, that the 
    /// extension has to be.xml and the extension.graphml is not supported.</param>
    /// <returns>Graph consisting of List nodes and edges.</returns>
    public static Graph GetParsedGraph(string inputFile)
    {
        List<Edge> edges = new List<Edge>();
        List<Node> nodes = new List<Node>();

        // parse inputFile and save nodes and vertices.
        TextAsset t = Resources.Load(inputFile) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(t.text);

        foreach (XmlNode xmlNode in doc.DocumentElement)
        {
            // we only need the graph section, not the header
            if (xmlNode.Name == "graph")
            {
                foreach (XmlNode child in xmlNode.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "node":
                            string id = child.Attributes[0].Value;

                            float size = float.Parse(child.ChildNodes[1].InnerText, CultureInfo.InvariantCulture);
                            size = string.Equals(size.ToString("R"), "NaN") ? 1.0f : size;

                            // divide to 255 because gephi uses 8 bit coloring, 
                            // while unity supports values in range [0, 1]
                            float red = float.Parse(child.ChildNodes[2].InnerText, CultureInfo.InvariantCulture) / 255;
                            float green = float.Parse(child.ChildNodes[3].InnerText, CultureInfo.InvariantCulture) / 255;
                            float blue = float.Parse(child.ChildNodes[4].InnerText, CultureInfo.InvariantCulture) / 255;
                            float[] rgb = { red, green, blue };

                            float x = float.Parse(child.ChildNodes[5].InnerText, CultureInfo.InvariantCulture);
                            float y = float.Parse(child.ChildNodes[6].InnerText, CultureInfo.InvariantCulture);
                            float z = float.Parse(child.ChildNodes[7].InnerText, CultureInfo.InvariantCulture);
                            float[] xyz = { x, y, z };

                            nodes.Add(new Node(id, size, rgb, xyz));
                            break;
                        case "edge":
                            string sourceId = child.Attributes[0].Value;
                            string destinationId = child.Attributes[1].Value;
                            float weight = float.Parse(child.ChildNodes[0].InnerText, CultureInfo.InvariantCulture);
                            edges.Add(new Edge(sourceId, destinationId, weight));
                            break;
                    }
                }
            }
        }

        return new Graph(nodes, edges);
    }

}
