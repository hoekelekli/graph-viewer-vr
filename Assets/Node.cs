using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private string id;
    private float size;
    private float[] rgb;
    private float[] xyz;

    public Node(string id, float size, float[] rgb, float[] xyz) 
    {
        this.id = id;
        this.size = size;
        this.rgb = rgb;
        this.xyz = xyz;
    }

    public string getId()
    {
        return id;
    }

    public float[] getRgb()
    {
        return rgb;
    }

    public float[] getXyz()
    {
        return xyz;
    }   

    public float getSize()
    {
        return size;
    }

    public void setId(string id)
    {
        this.id = id;
    }

    public void setXyz(float[] xyz)
    {
        this.xyz = xyz;
    }

    public void setRgb(float[] rgb)
    {
        this.rgb = rgb;
    }
}
