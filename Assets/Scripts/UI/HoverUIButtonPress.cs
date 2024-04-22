using UnityEngine;

public class HoverUIButtonPress : MonoBehaviour
{
    //called when an icon is pressed
    public void OnButtonPress()
    {

        //find the gameObject of the celestial body proper and run it's showInfoBox funcion
        CelestialBody celes = this.transform.parent.parent.GetComponentInParent<CelestialBody>();
        celes.ShowInfoBox();
    }
}
