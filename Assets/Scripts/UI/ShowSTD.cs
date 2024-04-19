using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSTD : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    public void toggleSTDs()
    {
        Debug.Log("SPACETIME: Toggled");
        foreach(CelestialBody body in solarSystemManager.celestialBodiesList)
        {
            GameObject plane = body.transform.Find("SpaceTimePlane(Clone)").gameObject;
            plane.SetActive(!plane.activeSelf);
        }
    }
}
