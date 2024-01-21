using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinButton : MonoBehaviour
{
    private Button button;
    private Image image;
    public float speed = 10f;
    private void Start()
    {
        button = GetComponentInChildren<Button>();
        image = GetComponentInChildren<Image>();
    }
    void Update()
    {
        if (GameManager.Instance.isSpin)
        {
            button.interactable = false;
            image.transform.Rotate(0, 0, image.transform.rotation.z - (speed * 5));
        }
        else
        {
            button.interactable = true;
            image.transform.Rotate(0, 0, image.transform.rotation.z - speed);
        }
    }
}
