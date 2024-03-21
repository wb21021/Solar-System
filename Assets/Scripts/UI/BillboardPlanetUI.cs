using UnityEngine;

public class BillboardPlanetUI : MonoBehaviour
{
    //This code allows the hovering text boxes over planets to always be facing the user.

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        //Set camera
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        //Rotate the text box in the same orientation as the user.
        transform.rotation = mainCamera.transform.rotation;
    }
}
