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

            
            for(int i = 0; i < body.transform.childCount; i++)
            {
                Debug.Log(body.transform.GetChild(i).name);
            }
            GameObject UI = body.transform.Find("icon").gameObject;

            UI.GetComponent<SpriteRenderer>().enabled = !UI.GetComponent<SpriteRenderer>().enabled;

            TMP_Text[] text_components = UI.transform.GetChild(0).GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in text_components)
            {
                text.enabled = !text.enabled;
            }
            
            
        }
    }
}
