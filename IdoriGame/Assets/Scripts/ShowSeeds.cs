using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSeeds : MonoBehaviour
{
    public GameObject endSeed;

    public void showSeeds()
    {
        float seedXPos = transform.position.x;
        float seedYPos = transform.position.y;

        for (int i = 0; i < Menu.endSeedCount; i++)
        {
            float randomSeedYPos = seedYPos + Random.Range(-0.2f, 0.2f);

            GameObject biggerEndSeed = Instantiate(endSeed, new Vector3(seedXPos, randomSeedYPos, 0), transform.rotation);
            biggerEndSeed.transform.localScale = new Vector3(0.4f, 0.4f, 1);

            seedXPos += 1.5f;

            if(seedXPos > 8)
            {
                seedXPos = transform.position.x;
                seedYPos -= 1.5f;
            }
        }
    }
}
