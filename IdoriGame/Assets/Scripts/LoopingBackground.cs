using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed; // the speed at which the background moves

    private Renderer R_BackgroundRenderer;

    void Start()
    {
        R_BackgroundRenderer = GetComponent<Renderer>();

        float quadHeight = (float)(Camera.main.orthographicSize * 2.0);
        float quadWidth = quadHeight * Screen.width / Screen.height;

        transform.localScale = new Vector3(quadWidth, quadHeight, 1);
    }

    void Update()
    {
        if(Menu.gameStarted && !Menu.gameEnded)
        {
            // continuously loop the background
            R_BackgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
        }
    }
}
