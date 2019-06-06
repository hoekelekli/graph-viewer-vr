using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private string id;
    private int size;
    private int[] rgb;
    private float[] xyz;

    public Node(string id, int size, int[] rgb, float[] xyz) 
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

    public int[] getRgb()
    {
        return rgb;
    }

    public float[] getXyz()
    {
        return xyz;
    }   

    public void setId(string id)
    {
        this.id = id;
    }

    public void setXyz(float[] xyz)
    {
        this.xyz = xyz;
    }

    public void setRgb(int[] rgb)
    {
        this.rgb = rgb;
    }
}
