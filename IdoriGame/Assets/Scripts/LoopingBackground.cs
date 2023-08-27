using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Background
 */

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed; // the speed at which the background moves
    public Renderer backgroundRenderer; // the renderer object that renders the background

    void Update()
    {
        // continuously loop the background
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
    }
}
