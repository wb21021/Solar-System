using UnityEngine;
using TMPro;
public class InfoMenuButton : MonoBehaviour
{
    //This class deals with adding buttons that change the page of long text, much like a kindle

    //set the long text
    public TMP_Text descriptionText;
    public void ButtonForwardPress()
    {
        //increment page forward, unless its reached the end of the text
        int totalpages = descriptionText.textInfo.pageCount;
        if (descriptionText.pageToDisplay < totalpages)
        {
            descriptionText.pageToDisplay++;
        }
    }
    public void ButtonBackPress()
    {
        //increment page backwards, unless its reached the start of the text
        int totalpages = descriptionText.textInfo.pageCount;
        if (descriptionText.pageToDisplay >= 1)
        {
  
            descriptionText.pageToDisplay = descriptionText.pageToDisplay - 1;
        }
    }
}
