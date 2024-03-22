using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUXPanel : MonoBehaviour
{
    public void CloseUXPanelButtonPress()
    {
        this.transform.parent.parent.gameObject.SetActive(false);
    }
}
