using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/**
 * This script is attached to:
 * -Spawn Point
 */

/**
 * IDEA: could add multiple branches on one side
 */

public class SpawnTree : MonoBehaviour
{
    // 4 parts of a tree that spawn together (trunk, leaves, leftBranch, rightBranch)
    public GameObject trunk;
    public GameObject leaves_1; // there are 3 variations of the leaves; this script will randomly pick 1
    public GameObject leaves_2;
    public GameObject leaves_3;
    public GameObject leftBranch;
    public GameObject rightBranch;

    public float treeSeparation; // how far apart the trees should be

    // the range of possible Y locations where the tree branches could spawn
    public float minBranchHeight;
    public float maxBranchHeight;
    public float maxBranchHeightDiff;

    // the range of possible lengths of the tree branches
    public float minBranchScale;
    public float maxBranchScale;

    public GameObject gameManager; // used to access the SpawnItem script within Game Manager

    private float rightBranchEdgePos;
    private float previousHeight_Right;

    private void Start()
    {
        previousHeight_Right = Random.Range(minBranchHeight, maxBranchHeight);
    }

    void Update()
    {
        // transform.position is referring to the spawnPoint; branch lengths are still randomized; this is why the separations are still random
        if (transform.position.x - rightBranchEdgePos > treeSeparation)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        // randomize the spawn locations of the branches
        float height_Left = Random.Range(minBranchHeight, maxBranchHeight);
        float height_Right = Random.Range(minBranchHeight, maxBranchHeight);

        while (height_Left - previousHeight_Right > maxBranchHeightDiff)
        {
            height_Left = Random.Range(minBranchHeight, maxBranchHeight);
        }

        while (height_Right - height_Left > maxBranchHeightDiff 
            || height_Right > height_Left && height_Right - height_Left < 0.5f)
        {
            height_Right = Random.Range(minBranchHeight, maxBranchHeight);
        }

        previousHeight_Right = height_Right;

        // randomize the lengths of the branches
        float randomBranchLength_Left = Random.Range(minBranchScale, maxBranchScale);
        float randomBranchLength_Right = Random.Range(minBranchScale, maxBranchScale);

        GameObject randomLeaves; // this will be set to 1 of the 3 tree leaves later on
        int leavesChance = Random.Range(0, 3); // a randomized int used to determine which tree leaves to choose

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

        // local clones of the tree branches (always create clones of objects before editing them)
        GameObject leftBranchClone;
        GameObject rightBranchClone;

        // spawn the branches (while assigning the return value of Instantiate() to the clones at the same time)
        leftBranchClone = Instantiate(leftBranch, transform.position + new Vector3(0, height_Left, 1), leftBranch.transform.rotation);
        rightBranchClone = Instantiate(rightBranch, transform.position + new Vector3(0, height_Right, 1), rightBranch.transform.rotation);

        // edit the clones' scales to change the lengths of the branches
        leftBranchClone.transform.localScale = new Vector3(randomBranchLength_Left, leftBranch.transform.localScale.y, 1);
        rightBranchClone.transform.localScale = new Vector3(randomBranchLength_Right, rightBranch.transform.localScale.y, 1);

        SpriteRenderer sRenderer = rightBranchClone.GetComponentInChildren<SpriteRenderer>();
        float spriteWidth = sRenderer.sprite.bounds.size.x * rightBranchClone.transform.lossyScale.x;
        float rightBranchPos = rightBranchClone.transform.position.x;

        rightBranchEdgePos = spriteWidth + rightBranchPos;

        SpawnItem itemSpawner = gameManager.GetComponent<SpawnItem>(); // used to spawn items on the branches

        // spawn items
        itemSpawner.Spawn(leftBranchClone);
        itemSpawner.Spawn(rightBranchClone);
    }
}
