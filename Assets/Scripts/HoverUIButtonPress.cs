using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUIButtonPress : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log("BUTTON: pressed");
        Debug.Log("BUTTON: "+transform.parent.parent.name);

        
        CelestialBody celes = transform.parent.GetComponentInParent<CelestialBody>();
        celes.ShowInfoBox();
    }
}
