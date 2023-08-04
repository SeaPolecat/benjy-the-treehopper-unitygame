using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Game Manager
 */

public class SpawnItem : MonoBehaviour
{
    public float spawnChance; // a float between 0-1; the percentage chance that each branch will spawn an item
    public GameObject apple; // the item to be spawned

    public void Spawn(GameObject branch)
    {
        float branchXPos = branch.transform.position.x;

        SpriteRenderer sRenderer = branch.GetComponentInChildren<SpriteRenderer>();
        float spriteWidth = sRenderer.sprite.bounds.size.x * branch.transform.lossyScale.x;

        float itemPosX;
        float itemPosY = branch.transform.position.y + 0.8f;
        float offset = 1f;

        if(branch.tag == "Right")
        {
            itemPosX = Random.Range((branchXPos + offset), (branchXPos + spriteWidth - offset));
        } else
        {
            itemPosX = Random.Range((branchXPos - spriteWidth + offset), (branchXPos - offset));
        }

        if(Random.Range(0f, 1f) <= spawnChance)
        {
            Instantiate(apple, new Vector3(itemPosX, itemPosY, -0.5f), transform.rotation);
        }
    }
}
