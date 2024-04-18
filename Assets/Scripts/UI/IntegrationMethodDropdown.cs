using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntegrationMethodDropdown : MonoBehaviour
{
    public SolarSystemManager solarSystemManager;

    public void DrowpdownChanged()
    {
        Debug.Log("INTEGRATION: RUnning");
        int integrationMethodIndex = this.GetComponent<TMP_Dropdown>().value;
        Debug.Log("INTEGRATION: "+ integrationMethodIndex);
        solarSystemManager.integrationMethodIndex = integrationMethodIndex;
    }
}
