using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    public bool isTriggered = false;
    int itemsInside = 0;
    public bool getTriggered()
    {
        return isTriggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        isTriggered = true;
        itemsInside += 1;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        isTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        itemsInside -= 1;
        if (itemsInside <= 0)
        {
            isTriggered = false;
        }
    }
}
