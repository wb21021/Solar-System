using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HideUI : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;
    public void HideUIButton()
    {
        foreach(CelestialBody body in solarSystemManager.celestialBodiesList)
        {
            //only look at the bodies that have text boxes over them to begin with
            //ie just the 'parent' bodies
            if(body.isMoon == 0)
            {
                GameObject UI = body.transform.Find("CanvasCelestialBodyInfo(Clone)").gameObject;
                UI.GetComponentInChildren<RawImage>().enabled = !UI.GetComponentInChildren<RawImage>().enabled;
                UI.GetComponentInChildren<TMP_Text>().enabled = !UI.GetComponentInChildren<TMP_Text>().enabled;
            }
            
        }
    }
}
