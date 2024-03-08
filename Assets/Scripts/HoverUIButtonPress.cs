using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUIButtonPress : MonoBehaviour
{
    public void OnButtonPress()
    {

        
        CelestialBody celes = transform.parent.GetComponentInParent<CelestialBody>();
        celes.ShowInfoBox();
    }
}
