using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Trunk prefab
 * -Left Branch prefab
 * -Right Branch prefab
 */

public class DeleteTree : MonoBehaviour
{
    // delete the object this script is attached to if it collides with the side border
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
