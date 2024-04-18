using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPlayerSpeed : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    public void Update()
    {
        this.GetComponent<TMP_Text>().text = solarSystemManager.GetPlayerSpeed().ToString();
    }
}
