using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    public string SlotName;
    public bool isOnSlot = false;
    private RectTransform RT;
    private void Start()
    {
        RT = GetComponent<RectTransform>();
    }
    public string GetName()
    {
        return SlotName;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Line" && !GameManager.Instance.CanSpin)
        {
            isOnSlot = true;
        }
    }
}
