using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSTD : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    //called when the Space-Time Distortions toggle is pressed
    public void toggleSTDs()
    {
        //for every body in the simualtion
        foreach(CelestialBody body in solarSystemManager.celestialBodiesList)
        {
            //flip the std plane of the current body to the opposite status (off->on, on->off)
            GameObject plane = body.transform.Find("SpaceTimePlane(Clone)").gameObject;
            plane.SetActive(!plane.activeSelf);
        }
    }
}
