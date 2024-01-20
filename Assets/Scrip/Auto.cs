using System.Collections;
using UnityEngine;

public class Auto : MonoBehaviour
{
    private float _spinSpeed = 10f;
    private RectTransform RT;
    public int _itemAmount = 20;
    private bool isFinish = false;
    private int index = 0;
    private Transform[] child;
    private void Start()
    {
        //Atact this Script to Content in Scroll View To make it Infinite Loop View
        RT = GetComponent<RectTransform>();
        _spinSpeed = GameManager.Instance.SpinTime;
        GameManager.Instance.GetRandomItemSlot(RT, _itemAmount);
        child = RT.GetComponentsInChildren<Transform>();
    }
    public void OnSpin()
    {
        StartCoroutine(Spinning(_spinSpeed));
    }
    IEnumerator Spinning(float speed)
    {
        while (!isFinish)
        {
            yield return new WaitForSeconds(0.0001f);
            RT.transform.localPosition = new Vector3(RT.transform.localPosition.x, RT.transform.localPosition.y + speed, RT.transform.localPosition.z);
            speed -= 0.02f;
            GameManager.Instance.isSpin = true;
            GameManager.Instance.HasChecked = false;
            child[index].SetAsLastSibling();
            index++;
            if (index >= child.Length)
            {
                index = 0;
            }
            if (speed <= 0)
            {
                isFinish = true;
            }
        }
        if (isFinish)
        {
            StartCoroutine(AutoSnap());
        }
    }
    IEnumerator AutoSnap()
    {
        yield return new WaitForSeconds(0.5f);
        if(RT.transform.localPosition.y % 105 != 0)
        {
            isFinish = false;
            float y = Mathf.Round(RT.transform.localPosition.y / 105);
            int temp = (int)y * 105;
            GameManager.Instance.isSpin = false;
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
        }
    }
}
