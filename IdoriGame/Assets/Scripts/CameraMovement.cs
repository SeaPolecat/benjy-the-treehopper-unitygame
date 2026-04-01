using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed; // the speed at which the camera moves
    public float speedMultiplier;

    void Update()
    {
        if(Menu.gameStarted && !Menu.gameEnded)
        {
            // continuously move the camera towards the right
            transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);

            // continously increase the speed of the camera
            cameraSpeed += speedMultiplier * Time.deltaTime;
        }
    }
}
