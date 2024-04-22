using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUXPanel : MonoBehaviour
{
    public void CloseUXPanelButtonPress()
    {
        //This turns off the Information tab using the CLose button in the top right
        this.transform.parent.parent.gameObject.SetActive(false);
    }
}
