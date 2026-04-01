using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public float treeSeparation; // how far apart the trees should be
    public float minBranchHeight; // minimum possible Y location
    public float maxBranchHeight; // maximum possilbe Y location
    public float maxBranchHeightDiff; // the maximum height difference that could occur between 2 branches (to make for a fair jump)
    public float minBranchScale; // minimum scale
    public float maxBranchScale; // maximum scale
    public float spawnChance; // a float between 0-1; the percentage chance that each branch will spawn an item
    public float itemOffset; // how much the items will be offset from the branch's edge positions
    public GameObject player;
    public GameObject trunk; // the trunk
    public GameObject leaves_1; // leaf variation 1
    public GameObject leaves_2; // leaf variation 2
    public GameObject leaves_3; // leaf variation 3
    public GameObject leftBranch; // the left branch object
    public GameObject rightBranch; // the right branch object
    public GameObject seed;
    public GameObject apple;
    public GameObject banana;

    private float rightBranchPrevEdgeXPos; // the edge X position of the previous right branch
    private float rightBranchPrevHeight; // the Y position (height) of the previous right branch

    void Start()
    {
        // start off by randomizing a previous right branch height (since we don't have one yet)
        rightBranchPrevHeight = Random.Range(minBranchHeight, maxBranchHeight);

        // spawning the initial trees
        transform.position = new Vector3(0, 0, 0);
        float playerSpawnPos = FindBranchEdgeXPos(SpawnTrunk(true));

        transform.position = new Vector3(rightBranchPrevEdgeXPos + treeSeparation, 0, 0);
        SpawnTrunk();

        transform.position = new Vector3(rightBranchPrevEdgeXPos + treeSeparation, 0, 0);

        // move the player above the first branch
        player.transform.position = new Vector3(playerSpawnPos + 0.5f, maxBranchHeight + 1f, 0);
    }

    void Update()
    {
        // spawn another tree if the spawn point is far away enough from the previous tree
        if(transform.position.x - rightBranchPrevEdgeXPos > treeSeparation)
        {
            SpawnTrunk();
        }
    }
    
    private GameObject SpawnTrunk(bool returnLeft = true)
    {
        // spawn the tree trunk
        Instantiate(trunk, transform.position + new Vector3(0, 0, 0), transform.rotation);
        int leavesChance = Random.Range(0, 3); // a randomized int used to determine which tree leaves to choose

        GameObject randomLeaves = leavesChance switch
        {
            0 => leaves_1,
            1 => leaves_2,
            _ => leaves_3,
        };

        // spawn the tree leaves on top of the trunk
        Instantiate(randomLeaves, transform.position + new Vector3(0, 4.2f, 0), transform.rotation);

        return SpawnBranches(returnLeft);
    }

    private GameObject SpawnBranches(bool returnLeft = true)
    {
        // randomize the spawn locations of the left and right branches
        float height_Left = Random.Range(minBranchHeight, maxBranchHeight);

        /**
         * this loop ensures that the left branch is a reasonable jump distance from the previous right branch
         */
        while (height_Left - rightBranchPrevHeight > maxBranchHeightDiff
            || height_Left > rightBranchPrevHeight && height_Left - rightBranchPrevHeight < 0.5f)
        {
            height_Left = Random.Range(minBranchHeight, maxBranchHeight);
        }

        GameObject leftBranchClone = InstantiateBranch(leftBranch, height_Left);

        float height_Right = Random.Range(minBranchHeight, maxBranchHeight);

        // ensures that the right branch is a reasonable jump distance from the left branch
        while (height_Right - height_Left > maxBranchHeightDiff
            || height_Right > height_Left && height_Right - height_Left < 0.5f)
        {
            height_Right = Random.Range(minBranchHeight, maxBranchHeight);
        }

        GameObject rightBranchClone = InstantiateBranch(rightBranch, height_Right);

        rightBranchPrevHeight = height_Right;
        rightBranchPrevEdgeXPos = FindBranchEdgeXPos(rightBranchClone);

        switch (returnLeft)
        {
            case true:
                return leftBranchClone;

            case false:
                return rightBranchClone;
        }
    }

    private GameObject InstantiateBranch(GameObject branch, float height)
    {
        GameObject branchClone = Instantiate(branch, transform.position + new Vector3(0, height, 0), branch.transform.rotation);
        float randomBranchXScale = Random.Range(minBranchScale, maxBranchScale);

        branchClone.transform.localScale = new Vector3(randomBranchXScale, branch.transform.localScale.y, 1);

        SpawnItem(branchClone);
        return branchClone;
    }

    public void SpawnItem(GameObject branch)
    {
        float branchXPos = branch.transform.position.x; // the X position of the branch
        float branchEdgeXPos = FindBranchEdgeXPos(branch); // the edge X position of the branch
        float itemPosX; // the X position of the item
        float itemPosY = branch.transform.position.y + 0.8f; // the Y position of the item (set to be slightly higher than the branch)

        // set the X position of the item, depending on what the direction of the branch is
        switch (branch.tag)
        {
            case "LeftBranch":
                itemPosX = Random.Range(branchEdgeXPos + itemOffset, branchXPos - itemOffset);
                break;

            case "RightBranch":
                itemPosX = Random.Range(branchXPos + itemOffset, branchEdgeXPos - itemOffset);
                break;

            default:
                Debug.Log("X pos of the item could not be set!");
                itemPosX = -1; 
                break;
        }

        // spawn an item on the branch, based on the spawnChance
        if(Random.Range(0f, 1f) <= spawnChance)
        {
            int itemChance = Random.Range(0, 3);

            switch(itemChance)
            {
                case 0:
                    Instantiate(seed, new Vector3(itemPosX, itemPosY, 0), transform.rotation);
                    break;

                case 1:
                    Instantiate(apple, new Vector3(itemPosX, itemPosY, 0), transform.rotation);
                    break;

                case 2:
                    Instantiate(banana, new Vector3(itemPosX, itemPosY, 0), transform.rotation);
                    break;

                default:
                    Debug.Log("Item could not be spawned!");
                    break;
            }
        }
    }

    float FindBranchEdgeXPos(GameObject branch)
    {
        SpriteRenderer sRenderer = branch.GetComponentInChildren<SpriteRenderer>(); // the sprite renderer of the branch
        float spriteWidth = sRenderer.sprite.bounds.size.x * branch.transform.lossyScale.x; // the width of the branch sprite
        float branchXPos = branch.transform.position.x; // the X position of the branch

        // find the edge X position, depending on what the direction of the branch is
        switch(branch.tag)
        {
            case "LeftBranch":
                return branchXPos - spriteWidth;

            case "RightBranch":
                return branchXPos + spriteWidth;

            default:
                Debug.Log("Branch Edge X Pos could not be found!");
                break;
        }
        return -1;
    }
}
