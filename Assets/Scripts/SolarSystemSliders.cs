using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WholeSolarSystemScale : MonoBehaviour
{
    public GameObject WholeSolarSystem;
    public SolarSystemManager SolarSystemManager;
    public void SizeSliderChanged()
    {
        float rawSliderValue = this.GetComponent<Scrollbar>().value;

        float scale = (rawSliderValue * 3) + 0.1f;

        WholeSolarSystem.transform.localScale = new Vector3(scale,scale,scale);

        foreach (CelestialBody body in SolarSystemManager.celestialBodiesList)
        {
            body.GetComponent<TrailRenderer>().Clear();
        }
    }

    public void TimeScaleSliderChanged()
    {
        float rawSliderValue = this.GetComponent <Scrollbar>().value;
        float scale = (rawSliderValue * 10000000) + 100000f;

        Debug.Log("TIMESCALE: " + scale);
        SolarSystemManager.customTimeScale = scale;
    }
}
