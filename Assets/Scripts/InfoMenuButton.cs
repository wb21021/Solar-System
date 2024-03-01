using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InfoMenuButton : MonoBehaviour
{
    public TMP_Text descriptionText;
    public void ButtonForwardPress()
    {
        int totalpages = descriptionText.textInfo.pageCount;
        Debug.Log("PAGES: " + totalpages);
        Debug.Log("FORWARD CURRENT PAGES: " + descriptionText.pageToDisplay);
        if (descriptionText.pageToDisplay < totalpages)
        {
            descriptionText.pageToDisplay++;
        }
    }
    public void ButtonBackPress()
    {
        int totalpages = descriptionText.textInfo.pageCount;

        Debug.Log("BACK CURRENT PAGES: " + descriptionText.pageToDisplay);

        if (descriptionText.pageToDisplay >= 1)
        {
  
            descriptionText.pageToDisplay = descriptionText.pageToDisplay - 1;
        }
    }
}
