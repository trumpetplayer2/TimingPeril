using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    GameManager manager;
    public GameObject[] boxHitboxes;
    public GameObject[] enableHitboxes;
    public Sprite defaultTexture;
    public Sprite invalidLocationTexture;

    private void Start()
    {
        manager = GameManager.instance;
    }

    private void Update()
    {
        int side = manager.getSideID();
        for(int i = 0; i < boxHitboxes.Length; i++)
        {
            GameObject box = boxHitboxes[i];
            if(i == side)
            {
                if (!enableHitboxes[i].GetComponent<Triggered>().getTriggered())
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = defaultTexture;
                    box.SetActive(true);
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = invalidLocationTexture;
                }
            }
            else
            {
                box.SetActive(false);
            }
        }
    }
}
