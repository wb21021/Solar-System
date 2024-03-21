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
            Debug.Log("BODY: "+body.transform.childCount);
            GameObject UI = body.transform.Find("CanvasCelestialBodyInfo(Clone)").gameObject;
            UI.GetComponentInChildren<RawImage>().enabled = !UI.GetComponentInChildren<RawImage>().enabled;
            UI.GetComponentInChildren<TMP_Text>().enabled = !UI.GetComponentInChildren<TMP_Text>().enabled;
        }
    }
}
