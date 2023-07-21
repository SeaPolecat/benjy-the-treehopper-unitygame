using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Game Manager
 */

// IDEA: could make the camera move faster as the game progresses?

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;

    void Update()
    {
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}
