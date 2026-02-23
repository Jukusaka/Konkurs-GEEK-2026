using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougeBox : MonoBehaviour
{
    [SerializeField] public GameObject textBox;
    [SerializeField] public string text;

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        if (text == "")
        {
            text = "jeśli widzisz ten tekst to spierdoliłem ten gówniany kod";
        }
        textBox.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
    }
}
