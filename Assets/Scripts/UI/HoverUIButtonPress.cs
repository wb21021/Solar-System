using UnityEngine;

public class HoverUIButtonPress : MonoBehaviour
{
    public void OnButtonPress()
    {

        Debug.Log(this.transform.parent.parent.name);
        CelestialBody celes = this.transform.parent.parent.GetComponentInParent<CelestialBody>();
        celes.ShowInfoBox();
    }
}
