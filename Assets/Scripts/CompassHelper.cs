using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassHelper : MonoBehaviour
{
    public GameObject player; 

    /// <summary>
    /// Initializes compass at the same position as the player.
    /// </summary>
    void Start()
    {
        transform.position = player.transform.position;
    }

    /// <summary>
    /// Updates compass position due to player position each frame.
    /// </summary>
    void Update()
    {
        transform.position = player.transform.position;
    }
}
