using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VRMenuButtonPresses : MonoBehaviour
{
    //This class turns the ray projection coming out of each hand on and off.
    //This is visual only, the interactor still works.

    //Set the projections and the gradients of what the rays should be.
    public XRInteractorLineVisual LeftHandRenderer;
    public XRInteractorLineVisual RightHandRenderer;

    public Gradient invisibleRay;
    public Gradient visibleRay;

    public void ShowRayButton()
    {
        //Find updated value of button
        bool IsRayOff = GetComponent<Toggle>().isOn;
        if (IsRayOff == false)
        {   //Set ray to the invisible gradient
            LeftHandRenderer.invalidColorGradient = invisibleRay;
            RightHandRenderer.invalidColorGradient = invisibleRay;

        }
        if (IsRayOff == true) 
        {
            //Set ray to the visible gradient
            LeftHandRenderer.invalidColorGradient = visibleRay;
            RightHandRenderer.invalidColorGradient = visibleRay;

        }

    }
}
