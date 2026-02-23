using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyEvent : MonoBehaviour
{
    private DialougeBox dialougeBox;

    private void Start()
    {
        dialougeBox = gameObject.GetComponent<DialougeBox>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialougeBox.EnableTextBox();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialougeBox.DisableTextBox();
        }
    }
}
