using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEndSeeds : MonoBehaviour
{
    void Update()
    {
        if(!Menu.gameEnded)
        {
            Destroy(gameObject);
        }
    }
}
