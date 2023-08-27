using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Game Manager
 */

// (maybe this script should be placed in Spawn Point instead)

public class SpawnItem : MonoBehaviour
{
    public float spawnChance; // a float between 0-1; the percentage chance that each branch will spawn an item
    public float offset; // how much the items will be offset from the branch's edge positions
    public GameObject seed; // the item to be spawned
    public GameObject spawnPoint; // the spawn point entity; used to access the SpawnTree class

    /**
     * REQUIRES:
     * branch: the branch that the item will be spawned on
     * 
     * MODIFIES:
     * n/a
     * 
     * EFFECTS:
     * spawns an item on a branch based on a random chance
     */
    public void Spawn(GameObject branch)
    {
        // create an instance of the class SpawnTree, so we can find the edge X positions of the branches
        SpawnTree treeSpawner = spawnPoint.GetComponent<SpawnTree>();

        float branchXPos = branch.transform.position.x; // the X position of the branch
        float branchEdgeXPos = treeSpawner.findBranchEdgeXPos(branch); // the edge X position of the branch

        float itemPosX; // the X position of the item
        float itemPosY = branch.transform.position.y + 1; // the Y position of the item (set to be slightly higher than the branch)

        // set the X position of the item, depending on what the direction of the branch is
        if (branch.tag == "LeftBranch")
        {
            itemPosX = Random.Range((branchEdgeXPos + offset), (branchXPos - offset));

        } else if(branch.tag == "RightBranch")
        {
            itemPosX = Random.Range((branchXPos + offset), (branchEdgeXPos - offset));

        } else
        {
            itemPosX = -1; 
        }

        // spawn an item on the branch, based on the spawnChance
        if(Random.Range(0f, 1f) <= spawnChance)
        {
            Instantiate(seed, new Vector3(itemPosX, itemPosY, -0.5f), transform.rotation);
        }
    }
}
