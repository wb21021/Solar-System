using System.Collections;
using System.Collections.Generic;
using System;

using TMPro;
using UnityEngine;

public class ShowPlayerSpeed : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    private const double AUtoM = 149597870700;

    //every frame
    public void Update()
    {
        //find the player speed in ms, and convert it to AU/s
        double speed = Math.Round(solarSystemManager.GetPlayerSpeed() / AUtoM, 7);

        //if the speed is set to null (before the simulation has started)
        if (double.IsNaN(speed))
        {
            //set speed 'visually' to 0
            speed = 0;
        }

        //display speed on the watch

        this.GetComponent<TMP_Text>().text = speed.ToString() + "AU/s";
    }
}
