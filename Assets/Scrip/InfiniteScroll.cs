using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewPortTransform;
    public RectTransform contentTransform;
    public VerticalLayoutGroup VLG;

    public RectTransform[] ItemList;
    private void Start()
    {
        int itemToAdd = Mathf.CeilToInt(viewPortTransform.rect.height / (ItemList[0].rect.height + VLG.spacing));

        for (int i = 0; i < itemToAdd; i++)
        {
            RectTransform RT = Instantiate(ItemList[i % ItemList.Length], contentTransform);
            RT.SetAsLastSibling();
        }
        for (int i = 0; i < itemToAdd; i++)
        {
            int num = ItemList.Length - i - 1;
            while (num < 0)
            {
                num += ItemList.Length;
            }
            RectTransform RT = Instantiate(ItemList[num], contentTransform);
            RT.SetAsFirstSibling();
        }
    }
    private void Update()
    {
        if(contentTransform.localPosition.y > 0)
        {
            Canvas.ForceUpdateCanvases();
            contentTransform.localPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.height + VLG.spacing),0,0);
        }
        if (contentTransform.localPosition.y < 0 - (ItemList.Length * (ItemList[0].rect.height + VLG.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            contentTransform.localPosition += new Vector3(ItemList.Length * (ItemList[0].rect.height + VLG.spacing), 0, 0);
        }
    }
}
