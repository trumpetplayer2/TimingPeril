using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If object is not a player
        if (!collision.gameObject.tag.Contains("Player")) { return; }
        //Check if Player has the PlayerController script, if not return
        if(collision.gameObject.GetComponent<PlayerController>() == null) { return; }
        //Use player controller to restart, as player died
        collision.gameObject.GetComponent<PlayerController>().Reset();
    }
}
