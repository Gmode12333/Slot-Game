using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetSlotItem : MonoBehaviour
{
    public MatrixPosition SlotPostion;
    public string InSlotName;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SlotItem" && !GameManager.Instance.isSpin && !GameManager.Instance.HasChecked && !GameManager.Instance.isBeginSpawn)
        {
            InSlotName = collision.GetComponent<SlotItem>().GetName();
            GetSlot();
            StartCoroutine(GameManager.Instance.WaitForCheckWin());
        }
    }
    public void GetSlot()
    {
        switch (SlotPostion)
        {
            case MatrixPosition.A1:
                GameManager.Instance.A1 = InSlotName;
                break;
            case MatrixPosition.A2:
                GameManager.Instance.A2 = InSlotName;
                break;
            case MatrixPosition.A3:
                GameManager.Instance.A3 = InSlotName;
                break;
            case MatrixPosition.B1:
                GameManager.Instance.B1 = InSlotName;
                break;
            case MatrixPosition.B2:
                GameManager.Instance.B2 = InSlotName;
                break;
            case MatrixPosition.B3:
                GameManager.Instance.B3 = InSlotName;
                break;
            case MatrixPosition.C1:
                GameManager.Instance.C1 = InSlotName;
                break;
            case MatrixPosition.C2:
                GameManager.Instance.C2 = InSlotName;
                break;
            case MatrixPosition.C3:
                GameManager.Instance.C3 = InSlotName;
                break;
            case MatrixPosition.D1:
                GameManager.Instance.D1 = InSlotName;
                break;
            case MatrixPosition.D2:
                GameManager.Instance.D2 = InSlotName;
                break;
            case MatrixPosition.D3:
                GameManager.Instance.D3 = InSlotName;
                break;
            case MatrixPosition.E1:
                GameManager.Instance.E1 = InSlotName;
                break;
            case MatrixPosition.E2:
                GameManager.Instance.E2 = InSlotName;
                break;
            case MatrixPosition.E3:
                GameManager.Instance.E3 = InSlotName;
                break;
        }
    }
}
public enum MatrixPosition
{
    A1,
    A2,
    A3,
    B1,
    B2,
    B3,
    C1,
    C2,
    C3,
    D1,
    D2,
    D3,
    E1,
    E2,
    E3,
}
