using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    public bool isTriggered = false;
    public bool getTriggered()
    {
        return isTriggered;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        isTriggered = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        isTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Box")) { return; }
        isTriggered = false;
    }
}
