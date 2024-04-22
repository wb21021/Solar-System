using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HideUI : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    //Called when the 'hide UI' button is pressed
    public void HideUIButton()
    {
        //for every body in the simulation
        foreach(CelestialBody body in solarSystemManager.celestialBodiesList)
        {

            
            //get the icon gameObject
            GameObject UI = body.transform.Find("icon").gameObject;

            //turn the icon image off
            UI.GetComponent<SpriteRenderer>().enabled = !UI.GetComponent<SpriteRenderer>().enabled;

            //turn all the text off, but keep the button selectable
            TMP_Text[] text_components = UI.transform.GetChild(0).GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in text_components)
            {
                text.enabled = !text.enabled;
            }
            
            
        }
    }
}
