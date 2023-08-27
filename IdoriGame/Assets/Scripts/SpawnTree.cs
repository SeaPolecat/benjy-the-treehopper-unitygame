using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Spawn Point
 */

public class SpawnTree : MonoBehaviour
{
    // 4 parts of a tree that spawn together (trunk, leaves, leftBranch, rightBranch)
    public GameObject trunk; // the trunk
    public GameObject leaves_1; // leaf variation 1
    public GameObject leaves_2; // leaf variation 2
    public GameObject leaves_3; // leaf variation 3
    public GameObject leftBranch; // the left branch object
    public GameObject rightBranch; // the right branch object

    public float treeSeparation; // how far apart the trees should be

    // the range of possible Y locations where the tree branches could spawn
    public float minBranchHeight; // minimum possible Y location
    public float maxBranchHeight; // maximum possilbe Y location
    public float maxBranchHeightDiff; // the maximum height difference that could occur between 2 branches (to make for a fair jump)

    // the range of possible scales (lengths) of the tree branches
    public float minBranchScale; // minimum scale
    public float maxBranchScale; // maximum scale

    public GameObject gameManager; // the Game Manager entity; used to access the SpawnItem script within Game Manager

    private GameObject player; // the Player entity; used to modify the player's position
    private float rightBranchEdgeXPos; // the edge X position of the previous right branch
    private float previousHeight_Right; // the Y position (height) of the previous right branch

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // start off by randomizing a previous right branch height (since we don't have one yet)
        previousHeight_Right = Random.Range(minBranchHeight, maxBranchHeight);

        // move the spawn point to the location of the first tree
        transform.position = new Vector3(0, -1, 0);

        // spawn the first tree on screen (while getting a left branch clone via the Spawn method)
        GameObject leftBranchClone = Spawn("left");

        // find the edge X position of the left branch clone
        float leftBranchEdgeXPos = findBranchEdgeXPos(leftBranchClone);

        // move the player slightly above the edge X position
        player.gameObject.transform.position = new Vector3(leftBranchEdgeXPos, leftBranchClone.transform.position.y + 2, -1);

        // spawn 4 more trees
        transform.position = new Vector3(8, -1, 0);
        Spawn();

        transform.position = new Vector3(16, -1, 0);
        Spawn();

        transform.position = new Vector3(24, -1, 0);
        Spawn();

        transform.position = new Vector3(32, -1, 0);
        Spawn();

        // move the spawn point somewhere far away, and let it automatically generate more trees
        transform.position = new Vector3(40, -1, 0);
    }

    void Update()
    {
        // spawn another tree if the spawn point is far away enough from the previous tree
        if (transform.position.x - rightBranchEdgeXPos > treeSeparation)
        {
            Spawn();
        }
    }

    /**
     * REQUIRES:
     * optional direction: either "left" or "right"
     * 
     * MODIFIES:
     * rightBranchEdgeXPos: to be the edge X position of the right branch
     * previousHeight_Right: to be the height of the right branch
     * 
     * EFFECTS:
     * spawns a tree with randomized branch heights,
     * and returns either the tree's left or right branch object
     */
    private GameObject Spawn(string direction = "")
    {
        // spawn the tree trunk
        Instantiate(trunk, transform.position + new Vector3(0, 0, 0.5f), transform.rotation);

        GameObject randomLeaves; // this will be set to 1 of the 3 tree leaves later on
        int leavesChance = Random.Range(0, 3); // a randomized int used to determine which tree leaves to choose

        // randomly choose 1 of the 3 tree leaves
        if (leavesChance == 0)
        {
            randomLeaves = leaves_1;
        }
        else if (leavesChance == 1)
        {
            randomLeaves = leaves_2;
        }
        else
        {
            randomLeaves = leaves_3;
        }

        // spawn the tree leaves on top of the trunk
        Instantiate(randomLeaves, transform.position + new Vector3(0, 4.2f, 0), transform.rotation);

        // randomize the spawn locations of the left and right branches
        float height_Left = Random.Range(minBranchHeight, maxBranchHeight);
        float height_Right = Random.Range(minBranchHeight, maxBranchHeight);

        /**
         * this loop ensures that the left branch is a reasonable jump distance from the previous right branch
         * (it's probably not wise to use a while loop, but i'll just keep it like this for now due to time constraints)
         */
        while (height_Left - previousHeight_Right > maxBranchHeightDiff
            || height_Left > previousHeight_Right && height_Left - previousHeight_Right < 0.5f)
        {
            height_Left = Random.Range(minBranchHeight, maxBranchHeight);
        }

        // ensures that the right branch is a reasonable jump distance from the left branch
        while (height_Right - height_Left > maxBranchHeightDiff 
            || height_Right > height_Left && height_Right - height_Left < 0.5f)
        {
            height_Right = Random.Range(minBranchHeight, maxBranchHeight);
        }

        // modify the previous right branch height
        previousHeight_Right = height_Right;

        // local clones of the tree branches (always create clones of objects before editing them)
        GameObject leftBranchClone;
        GameObject rightBranchClone;

        // spawn the branches (while assigning the return value of Instantiate() to the clones at the same time)
        leftBranchClone = Instantiate(leftBranch, transform.position + new Vector3(0, height_Left, 1), leftBranch.transform.rotation);
        rightBranchClone = Instantiate(rightBranch, transform.position + new Vector3(0, height_Right, 1), rightBranch.transform.rotation);

        // randomize the scales of the branches
        float branchScale_Left = Random.Range(minBranchScale, maxBranchScale);
        float branchScale_Right = Random.Range(minBranchScale, maxBranchScale);

        // edit the branch clones' scales to change the lengths of the branches
        leftBranchClone.transform.localScale = new Vector3(branchScale_Left, leftBranch.transform.localScale.y, 1);
        rightBranchClone.transform.localScale = new Vector3(branchScale_Right, rightBranch.transform.localScale.y, 1);

        // modify rightBranchEdgeXPos to be the edge X position of the right branch
        rightBranchEdgeXPos = findBranchEdgeXPos(rightBranchClone);

        // create an instance of the class SpawnItem, so that we can spawn items on the branches
        SpawnItem itemSpawner = gameManager.GetComponent<SpawnItem>();

        // spawn items on the branches
        itemSpawner.Spawn(leftBranchClone);
        itemSpawner.Spawn(rightBranchClone);

        // return one of the branch objects, depending on what the direction is
        if (direction == "left")
        {
            return leftBranchClone;
        }
        return rightBranchClone;
    }

    /**
     * REQUIRES:
     * branch: the branch object that needs its edge X position to be found
     * 
     * MODIFIES:
     * n/a
     * 
     * EFFECTS:
     * finds the edge X position of a branch object
     */
    public float findBranchEdgeXPos(GameObject branch)
    {
        SpriteRenderer sRenderer = branch.GetComponentInChildren<SpriteRenderer>(); // the sprite renderer of the branch
        float spriteWidth = sRenderer.sprite.bounds.size.x * branch.transform.lossyScale.x; // the width of the branch sprite
        float branchXPos = branch.transform.position.x; // the X position of the branch

        // find the edge X position, depending on what the direction of the branch is
        if (branch.tag == "LeftBranch")
        {
            return branchXPos - spriteWidth;
        }
        else if(branch.tag == "RightBranch")
        {
            return branchXPos + spriteWidth;
        }

        // prints an error message and returns -1 if the direction isn't "left" or "right"
        Debug.Log("Error! Direction needs to be either right or left");
        return -1;
    }
}
