using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Spawn Point
 */

public class StartGame : MonoBehaviour
{
    void Start()
    {
        // used to access the SpawnTree script within Spawn Point
        SpawnTree treeSpawner = this.GetComponent<SpawnTree>();

        // spawn the initial trees at intervals of 8
        transform.position = new Vector3(-5, -1, 0);

        treeSpawner.Spawn();

        transform.position = new Vector3(3, -1, 0);

        treeSpawner.Spawn();

        transform.position = new Vector3(11, -1, 0);

        treeSpawner.Spawn();

        transform.position = new Vector3(19, -1, 0);
    }
}
