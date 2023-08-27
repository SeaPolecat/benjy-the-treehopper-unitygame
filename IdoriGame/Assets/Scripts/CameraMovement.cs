using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Game Manager
 */

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed; // the speed at which the camera moves

    void Update()
    {
        // continuously move the camera towards the right
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);

        // continously increase the speed of the camera
        cameraSpeed += 0.05f * Time.deltaTime;
    }
}
