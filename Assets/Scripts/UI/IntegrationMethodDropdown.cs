using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntegrationMethodDropdown : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    //This function is called when the dropdown selector for the integration methods is changed
    public void DrowpdownChanged()
    {
        //find the index of the currently selected integration method and update the solarSystemManagers respective index
        int integrationMethodIndex = this.GetComponent<TMP_Dropdown>().value;
        solarSystemManager.integrationMethodIndex = integrationMethodIndex;
    }
}
