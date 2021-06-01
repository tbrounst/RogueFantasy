using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackToolTip : MonoBehaviour
{
    private Text toolTipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        toolTipText = transform.Find("AttackDescription").GetComponent<Text>();

        ShowToolTip("Random tooltip text");
    }

    //public void Mouse()
    //{
    //    ShowToolTip("Mousing over");
    //}

    //private void OnMouseExit()
    //{
    //    HideToolTip();
    //}

    public void ShowToolTip(string toolTipString)
    {
        gameObject.SetActive(true);

        toolTipText.text = toolTipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(toolTipText.preferredWidth + textPaddingSize * 2f, toolTipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
