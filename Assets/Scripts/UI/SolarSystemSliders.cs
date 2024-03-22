using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WholeSolarSystemScale : MonoBehaviour
{
    // This deals with the button/scrollbar presses on the VRMenu

    // Import needed classes
    public GameObject WholeSolarSystem;
    public SolarSystemManager SolarSystemManager;

    public void SizeSliderChanged()
    {
        //Get the slider value {0 -> 1] and convert it into a meaningful scale.
        float rawSliderValue = this.GetComponent<Scrollbar>().value;

        float scale = (rawSliderValue * 5) + 0.1f;

        WholeSolarSystem.transform.localScale = new Vector3(scale,scale,scale);

        //Reset the trails so that the planets dont look like theyve moved in / out
        ClearTrails();
        
    }

    public void PauseButtonPress()
    {

        //Get new value of button press
        bool isPaused = this.GetComponent<Toggle>().isOn;
        if (!isPaused)
        {
            //When unpausing, get the value of the TimeSpeedScrollBar {0 -> 1}, and convert that into a meaningful time scale
            //Then set that to the current timescale.
            SolarSystemManager.customTimeScale = ScrollBarValueToTimeScale(this.transform.parent.Find("TimeSpeedScrollBar").transform.GetComponent<Scrollbar>().value);
        }
        else if (isPaused)
        {   
            //When pausing, set the current timescale to 0
            SolarSystemManager.customTimeScale = 0f;
        }
    }
    public void TimeScaleSliderChanged()
    {
        //Get the current value of the slider, and convert it into a meaningful time scale.
        //Then set the simulation timescale
        float rawSliderValue = this.GetComponent<Scrollbar>().value;
        
        SolarSystemManager.customTimeScale = ScrollBarValueToTimeScale(rawSliderValue);
    }

    public void ClearTrailsButtonPress()
    {
        ClearTrails();
    }

    private float ScrollBarValueToTimeScale(float scrollbarvalue)
    {

        //function to convert a ScrollBar value {0 -> 1} into a usable timescale
        scrollbarvalue = scrollbarvalue - 0.5f;
        float scale = ((scrollbarvalue * scrollbarvalue * 19.95f) + (scrollbarvalue * 19.95f) + 5.0375f) * 400000f;
        Debug.Log(scale);
        return scale;

    }
    private void ClearTrails()
    {
        //Reset the trail for every body currently in the simulation
        foreach (CelestialBody body in SolarSystemManager.celestialBodiesList)
        {
            body.GetComponent<TrailRenderer>().Clear();
        }
    }
}
