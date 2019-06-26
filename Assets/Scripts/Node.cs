using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    /// <summary>
    ///  This id gets displayed as node label.
    /// </summary>
    /// See <see cref="Node.label"/>
    public string id;

    public float size;

    /// <summary>
    /// Red, green and blue values in range [0, 1].
    /// </summary>
    public float[] rgb;

    /// <summary>
    /// Node coordinates in 3D world space. 
    /// Are scaled to avoid 
    /// </summary>
    public float[] xyz;

    /// <summary>
    /// Unity supported text renderer to display node id.
    /// </summary>
    public TextMesh label;

    public Node(string id, float size, float[] rgb, float[] xyz)
    {
        this.id = id;
        this.size = size;
        this.rgb = rgb;
        this.xyz = xyz;
    }
}
