using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Spawn Point
 */

public class SpawnInitialTrees : MonoBehaviour
{
    void Start()
    {
        // used to access the SpawnTree script within Spawn Point
        SpawnTree treeSpawner = this.GetComponent<SpawnTree>();

        // spawn the initial trees on screen
        transform.position = new Vector3(0, -1, 0);
        treeSpawner.Spawn();

        transform.position = new Vector3(9, -1, 0);
        treeSpawner.Spawn();

        transform.position = new Vector3(18, -1, 0);
    }
}
