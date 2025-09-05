using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItems : MonoBehaviour
{
    public Text TX_SeedCountText; // a text component that displays how many seeds the player has collected
    public ScoreManager S_ScoreText_ScoreManager;
    public AudioSource AS_SparkleSound;
    public AudioSource AS_MunchSound;

    private int seedCount; // counts the number of seeds the player has
    //
    private benmovjmp S_Player_Benmovjmp;

    public int SeedCount
    {
        get { return seedCount; }
    }

    void Start()
    {
        S_Player_Benmovjmp = GetComponent<benmovjmp>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            GameObject itemSprite = other.gameObject.transform.GetChild(0).gameObject;
            ParticleSystem PS_Item = other.gameObject.GetComponentInChildren<ParticleSystem>();

            if (itemSprite.activeSelf)
            {
                itemSprite.SetActive(false);

                PS_Item.Play();

                switch (other.name)
                {
                    case "Seed(Clone)":
                        seedCount++;

                        TX_SeedCountText.text = seedCount.ToString();
                        AS_SparkleSound.Play();
                        break;

                    case "Apple(Clone)":
                        S_ScoreText_ScoreManager.EatApple();
                        AS_MunchSound.Play();
                        break;

                    case "Banana(Clone)":
                        S_Player_Benmovjmp.EatBanana();
                        AS_MunchSound.Play();
                        break;
                }
            }
        }
    }
}
