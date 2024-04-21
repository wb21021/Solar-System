using System.Collections;
using System.Collections.Generic;
using System;

using TMPro;
using UnityEngine;

public class ShowPlayerSpeed : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    private const double AUtoM = 149597870700;
    public void Update()
    {
        double speed = Math.Round(solarSystemManager.GetPlayerSpeed() / AUtoM, 7);

        if (double.IsNaN(speed))
        {
            speed = 0;
        }

        this.GetComponent<TMP_Text>().text = speed.ToString() + "AU/s";
    }
}
