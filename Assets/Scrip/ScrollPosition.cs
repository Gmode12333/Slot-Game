using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPosition : MonoBehaviour
{
    public ScrollRect scrollRect;
    void Update()
    {
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
