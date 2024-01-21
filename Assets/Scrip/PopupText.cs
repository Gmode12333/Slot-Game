using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    public TextMeshProUGUI TextPopup;
    public int amount;
    void Start()
    {
        SetText(amount);
    }
    public void SetText(int amount)
    {
        if(amount > 0)
        {
            TextPopup.text = "+" + amount.ToString();
        }
        if(amount < 0)
        {
            TextPopup.text = amount.ToString();
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
