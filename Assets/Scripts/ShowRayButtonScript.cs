using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMenuButtonPresses : MonoBehaviour
{

    public XRInteractorLineVisual LeftHandRenderer;
    public XRInteractorLineVisual RightHandRenderer;
    //public bool IsRayOn = true;

    public Gradient invisibleRay;
    public Gradient visibleRay;
    // Start is called before the first frame update

    public void ShowRayButton()
    {
        bool IsRayOff = GetComponent<Toggle>().isOn;
        if (IsRayOff == false)
        {
            LeftHandRenderer.invalidColorGradient = invisibleRay;
            RightHandRenderer.invalidColorGradient = invisibleRay;

        }
        if (IsRayOff == true) 
        {
            LeftHandRenderer.invalidColorGradient = visibleRay;
            RightHandRenderer.invalidColorGradient = visibleRay;

        }

    }
}
