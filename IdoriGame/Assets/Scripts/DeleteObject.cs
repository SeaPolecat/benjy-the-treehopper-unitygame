using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ATTACHED TO:
 * -Side Border
 */

public class DeleteObject : MonoBehaviour
{
    // delete everything that this object collides with
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
