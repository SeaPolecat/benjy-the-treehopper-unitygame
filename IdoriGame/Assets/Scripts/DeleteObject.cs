using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script is attached to:
 * -Side Border
 */

public class DeleteObject : MonoBehaviour
{
    // delete everything that this object collides with
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
