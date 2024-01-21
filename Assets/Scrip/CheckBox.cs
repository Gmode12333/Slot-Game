using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public List<GameObject> CheckBoxList;
    public string A1, A2, A3, B1, B2, B3, C1, C2, C3, D1, D2, D3, E1, E2, E3;
    public GameObject A1Obj, A2Obj, A3Obj, B1Obj, B2Obj, B3Obj, C1Obj, C2Obj, C3Obj, D1Obj, D2Obj, D3Obj, E1Obj, E2Obj, E3Obj;
    public bool isFinish = false;
    public int WinLine = 0;
    private void Start()
    {
        StartCoroutine(CheckWinSlot());
    }
    public IEnumerator CheckWinSlot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(GameManager.Instance.CanSpin && !GameManager.Instance.isSpin)
            {
                foreach (var item in CheckBoxList)
                {
                    item.SetActive(true);
                    if (GameManager.Instance.HasChecked)
                    {
                       CheckWin();
                    }
                }
            }
            else
            {
                SetDefaultFrame();
                foreach (var item in CheckBoxList)
                {
                    item.SetActive(false);
                }
            }
        }
    }
    public void CheckWin()
    {
        A1 = GameManager.Instance.A1;
        A2 = GameManager.Instance.A2;
        A3 = GameManager.Instance.A3;
        B1 = GameManager.Instance.B1;
        B2 = GameManager.Instance.B2;
        B3 = GameManager.Instance.B3;
        C1 = GameManager.Instance.C1;
        C2 = GameManager.Instance.C2;
        C3 = GameManager.Instance.C3;
        D1 = GameManager.Instance.D1;
        D2 = GameManager.Instance.D2;
        D3 = GameManager.Instance.D3;
        E1 = GameManager.Instance.E1;
        E2 = GameManager.Instance.E2;
        E3 = GameManager.Instance.E3;
        WinLine = GameManager.Instance.PayLine;
        CheckFiveLine();
        CheckFourLine();
        CheckThreeLine();
    }
    public void CheckThreeLine()
    {
        if (A1 == B1 && B1 == C1)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C1Obj.SetActive(true);
        }
        if (A2 == B2 && B2 == C2)
        {
            A2Obj.SetActive(true);
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C3)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
        }
        if (B1 == C1 && C1 == D1)
        {
            B1Obj.SetActive(true);
            C1Obj.SetActive(true);
            D1Obj.SetActive(true);
        }
        if (B2 == C2 && C2 == D2)
        {
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
        }
        if (B3 == C3 && C3 == D3)
        {
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
        }
        if (C1 == D1 && D1 == E1)
        {
            C1Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (C2 == D2 && D2 == E2)
        {
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (C3 == D3 && D3 == E3)
        {
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A1 == B2 && B2 == C3)
        {
            A1Obj.SetActive(true);
            B2Obj.SetActive(true);
            C3Obj.SetActive(true);
        }
        if (A3 == B2 && B2 == C1)
        {
            A3Obj.SetActive(true);
            B2Obj.SetActive(true);
            C1Obj.SetActive(true);
        }
        if (C1 == D2 && D2 == E3)
        {
            C1Obj.SetActive(true);
            D2Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (C3 == D2 && D2 == E1)
        {
            C3Obj.SetActive(true);
            D2Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (B1 == C2 && C2 == D3)
        {
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
        }
        if (B3 == C2 && C2 == D1)
        {
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
        }
        if (D1 == C2 && C2 == B3)
        {
            D1Obj.SetActive(true);
            C2Obj.SetActive(true);
            B3Obj.SetActive(true);
        }
        if (D3 == C2 && C2 == B1)
        {
            D3Obj.SetActive(true);
            C2Obj.SetActive(true);
            B1Obj.SetActive(true);
        }
        if (A1 == B2 && B2 == C1)
        {
            A1Obj.SetActive(true);
            B2Obj.SetActive(true);
            C1Obj.SetActive(true);
        }
        if (A2 == B3 && B3 == C2)
        {
            A2Obj.SetActive(true);
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
        }
        if (B1 == C2 && C2 == D1)
        {
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
        }
        if (B2 == C3 && C3 == D2)
        {
            B2Obj.SetActive(true);
            C3Obj.SetActive(true);
            D2Obj.SetActive(true);
        }
        if (C1 == D2 && D2 == E1)
        {
            C1Obj.SetActive(true);
            D2Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (C2 == D3 && D3 == E2)
        {
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
    }
    public void CheckFourLine()
    {
        if (A1 == B1 && B1 == C1 && C1 == D1)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C1Obj.SetActive(true);
            D1Obj.SetActive(true);
        }
        if (A2 == B2 && B2 == C2 && C2 == D2)
        {
            A2Obj.SetActive(true);
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C3 && C3 == D3)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
        }
        if (B1 == C1 && C1 == D1 && D1 == E1)
        {
            B1Obj.SetActive(true);
            C1Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (B2 == C2 && C2 == D2 && D2 == E2)
        {
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (B3 == C3 && C3 == D3 && D3 == E3)
        {
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
    }
    public void CheckFiveLine()
    {
        if (A1 == B1 && B1 == C1 && C1 == D1 && D1 == E1)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C1Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A2 == B2 && B2 == C2 && C2 == D2 && D2 == E2)
        {
            A2Obj.SetActive(true);
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C3 && C3 == D3 && D3 == E3)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A1 == B2 && B2 == C3 && C3 == D2 && D2 == E1)
        {
            A1Obj.SetActive(true);
            B2Obj.SetActive(true);
            C3Obj.SetActive(true);
            D2Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A3 == B2 && B2 == C1 && C1 == D2 && D2 == E3)
        {
            A3Obj.SetActive(true);
            B2Obj.SetActive(true);
            C1Obj.SetActive(true);
            D2Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A2 == B1 && B1 == C2 && C2 == D1 && D1 == E2)
        {
            A2Obj.SetActive(true);
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A2 == B3 && B3 == C2 && C2 == D3 && D3 == E2)
        {
            A2Obj.SetActive(true);
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A1 == B1 && B1 == C2 && C2 == D3 && D3 == E3)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C2 && C2 == D1 && D1 == E1)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A2 == B3 && B3 == C2 && C2 == D1 && D1 == E2)
        {
            A2Obj.SetActive(true);
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A2 == B1 && B1 == C2 && C2 == D3 && D3 == E2)
        {
            A2Obj.SetActive(true);
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A1 == B2 && B2 == C2 && C2 == D2 && D2 == E1)
        {
            A1Obj.SetActive(true);
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A3 == B2 && B2 == C2 && C2 == D2 && D2 == E3)
        {
            A3Obj.SetActive(true);
            B2Obj.SetActive(true);
            C2Obj.SetActive(true);
            D2Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A1 == B2 && B2 == C1 && C1 == D2 && D2 == E1)
        {
            A1Obj.SetActive(true);
            B2Obj.SetActive(true);
            C1Obj.SetActive(true);
            D2Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A3 == B2 && B2 == C3 && C3 == D2 && D2 == E3)
        {
            A3Obj.SetActive(true);
            B2Obj.SetActive(true);
            C3Obj.SetActive(true);
            D2Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A2 == B2 && B2 == C1 && C1 == D2 && D2 == E2)
        {
            A2Obj.SetActive(true);
            B2Obj.SetActive(true);
            C1Obj.SetActive(true);
            D2Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A2 == B2 && B2 == C3 && C3 == D2 && D2 == E2)
        {
            A2Obj.SetActive(true);
            B2Obj.SetActive(true);
            C3Obj.SetActive(true);
            D2Obj.SetActive(true);
            E2Obj.SetActive(true);
        }
        if (A1 == B1 && B1 == C3 && C3 == D1 && D1 == E1)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C3Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C1 && C1 == D3 && D3 == E3)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C1Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A1 == B1 && B1 == C2 && C2 == D1 && D1 == E1)
        {
            A1Obj.SetActive(true);
            B1Obj.SetActive(true);
            C2Obj.SetActive(true);
            D1Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
        if (A3 == B3 && B3 == C2 && C2 == D3 && D3 == E3)
        {
            A3Obj.SetActive(true);
            B3Obj.SetActive(true);
            C2Obj.SetActive(true);
            D3Obj.SetActive(true);
            E3Obj.SetActive(true);
        }
        if (A1 == B3 && B3 == C3 && C3 == D3 && D3 == E1)
        {
            A1Obj.SetActive(true);
            B3Obj.SetActive(true);
            C3Obj.SetActive(true);
            D3Obj.SetActive(true);
            E1Obj.SetActive(true);
        }
    }
    public void SetDefaultFrame()
    {
        A1Obj.SetActive(false);
        A2Obj.SetActive(false);
        A3Obj.SetActive(false);
        B1Obj.SetActive(false);
        B2Obj.SetActive(false);
        B3Obj.SetActive(false);
        C1Obj.SetActive(false);
        C2Obj.SetActive(false);
        C3Obj.SetActive(false);
        D1Obj.SetActive(false);
        D2Obj.SetActive(false);
        D3Obj.SetActive(false);
        E1Obj.SetActive(false);
        E2Obj.SetActive(false);
        E3Obj.SetActive(false);
    }
}
