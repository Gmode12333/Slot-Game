using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Auto : MonoBehaviour
{
    private float _spinTime = 10f;
    private float _spinSpeed = 0.1f;
    private RectTransform RT;
    public int _itemAmount = 20;
    public ScrollLine scrollLine;
    private bool isFinish = false;
    private int index = 0;
    private float _speedLose;
    private Transform[] child;
    private void Start()
    {
        //Atact this Script to Content in Scroll View To make it Infinite Loop View
        RT = GetComponent<RectTransform>();
        _spinTime = GameManager.Instance.SpinTime;
        _spinSpeed = GameManager.Instance.SpinSpeed;
        GameManager.Instance.GetRandomItemSlot(RT, _itemAmount);
        child = RT.GetComponentsInChildren<Transform>();
        GetStopTime();
    }
    public void OnSpin()
    {
        StartCoroutine(Spinning(_spinTime));
    }
    public void GetStopTime()
    {
        switch (scrollLine)
        {
            case ScrollLine.Line1:
                _speedLose = 0.1f;
                break;
            case ScrollLine.Line2:
                _speedLose = 0.08f;
                break;
            case ScrollLine.Line3:
                _speedLose = 0.06f;
                break;
            case ScrollLine.Line4:
                _speedLose = 0.04f;
                break;
            case ScrollLine.Line5:
                _speedLose = 0.035f;
                break;
        }
    }
    IEnumerator Spinning(float time)
    {
        float height = RT.rect.height - 200;
        while (!isFinish)
        {
            yield return new WaitForSeconds(0.0001f);
            RT.transform.localPosition = new Vector3(RT.transform.localPosition.x, RT.transform.localPosition.y - time * _spinSpeed, RT.transform.localPosition.z);
            time -= _speedLose;
            GameManager.Instance.isSpin = true;
            GameManager.Instance.HasChecked = false;
            GameManager.Instance.CanSpin = false;
            if (time <= 0)
            {
                isFinish = true;
            }
            if(RT.transform.localPosition.y <= -height)
            {
                RT.transform.localPosition = new Vector3(RT.transform.localPosition.x, -400, RT.transform.localPosition.z);
            }
        }
        if (isFinish)
        {
            StartCoroutine(AutoSnap());
        }
    }
    IEnumerator AutoSnap()
    {
        yield return new WaitForSeconds(0.1f);
        if(RT.transform.localPosition.y % 105 != 0)
        {
            isFinish = false;
            float y = Mathf.Round(RT.transform.localPosition.y / 105);
            int temp = (int)y * 105;
            if(RT.transform.localPosition.y > temp)
            {
                float posY = RT.transform.localPosition.y - temp;
                RT.transform.localPosition = new Vector3(RT.transform.localPosition.x, RT.transform.localPosition.y - posY, RT.transform.localPosition.z);
            }
            else
            {
                float posY = temp - RT.transform.localPosition.y;
                RT.transform.localPosition = new Vector3(RT.transform.localPosition.x, RT.transform.localPosition.y + posY, RT.transform.localPosition.z);
            }
            GameManager.Instance.isSpin = false;
            GameManager.Instance.CanSpin = true;
        }
    }
}
[Serializable]
public enum ScrollLine
{
    Line1,
    Line2,
    Line3,
    Line4,
    Line5,
}
