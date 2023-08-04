using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Spawn Point
 */

// IDEA: could add multiple branches on one side

public class SpawnTree : MonoBehaviour
{
    // 4 parts of a tree that spawn together (trunk, leaves, leftBranch, rightBranch)
    public GameObject trunk;
    public GameObject leaves_1; // there are 3 variations of the leaves; this script will randomly pick 1
    public GameObject leaves_2;
    public GameObject leaves_3;
    public GameObject leftBranch;
    public GameObject rightBranch;

    // the range of possible Y locations where the tree branches could spawn
    public float minSpawnY;
    public float maxSpawnY;

    // the range of possible lengths of the tree branches
    public float minBranchLength;
    public float maxBranchLength;

    // the range of possible time lags that could pass between each tree spawn
    public float minSpawnLag;
    public float maxSpawnLag;

    // used to access the SpawnItem script within Game Manager
    public GameObject gameManager;

    // float that's used to help with the timing of tree spawns
    private float spawnTime;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            // randomize the time lag between each tree spawn
            float randomSpawnLag = Random.Range(minSpawnLag, maxSpawnLag);

            // spawn trees
            Spawn();
            spawnTime = Time.time + randomSpawnLag;
        }
    }

    public void Spawn()
    {
        // local clones of the tree branches
        /**
         * i wanted to randomize the lengths of the branches, and to do that, i needed to create clones of them first, then edit the clones.
         * please don't edit the original public variables here in the script, it will mess up the prefabs lol
         * i learned that the hard way :(
         */
        GameObject leftBranchClone;
        GameObject rightBranchClone;

        // randomize the spawn locations of the branches
        float randomSpawnY_Left = Random.Range(minSpawnY, maxSpawnY);
        float randomSpawnY_Right = Random.Range(minSpawnY, maxSpawnY);

        // randomize the lengths of the branches
        float randomBranchLength_Left = Random.Range(minBranchLength, maxBranchLength);
        float randomBranchLength_Right = Random.Range(minBranchLength, maxBranchLength);

        GameObject randomLeaves; // this will be set to 1 of the 3 tree leaves later on
        int leavesChance = Random.Range(0, 3); // a randomized int used to determine which tree leaves to choose

        SpawnItem itemSpawner = gameManager.GetComponent<SpawnItem>(); // used to spawn items on the branches

        // spawn the tree trunk
        Instantiate(trunk, transform.position + new Vector3(0, 0, 0.5f), transform.rotation);

        // randomly choose 1 of the 3 tree leaves
        if(leavesChance == 0)
        {
            randomLeaves = leaves_1;
        }
        else if(leavesChance == 1)
        {
            randomLeaves = leaves_2;
        }
        else
        {
            randomLeaves = leaves_3;
        }

        // spawn the tree leaves on top of the trunk
        Instantiate(randomLeaves, transform.position + new Vector3(0, 4.2f, 0), transform.rotation);

        // spawn the branches (while assigning the return value of Instantiate() to the clones at the same time)
        leftBranchClone = Instantiate(leftBranch, transform.position + new Vector3(0, randomSpawnY_Left, 1), leftBranch.transform.rotation);
        rightBranchClone = Instantiate(rightBranch, transform.position + new Vector3(0, randomSpawnY_Right, 1), rightBranch.transform.rotation);

        // edit the clones' scales to change the lengths of the branches
        leftBranchClone.transform.localScale = new Vector3(randomBranchLength_Left, leftBranch.transform.localScale.y, 1);
        rightBranchClone.transform.localScale = new Vector3(randomBranchLength_Right, rightBranch.transform.localScale.y, 1);

        // spawn items
        itemSpawner.Spawn(leftBranchClone);
        itemSpawner.Spawn(rightBranchClone);
    }
}
