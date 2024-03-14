using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]

public class CloseVrMenu : MonoBehaviour
{

    //This class deals with toggling the Left and Right hand information panels

    //right hand 'options menu'
    public GameObject menu;

    //left hand 'information tab'
    public GameObject infoBar;
    private InputData _inputData;

    //values to check if button is being held down, so that the toggle isnt triggered on and off every frame
    private bool _menubuttonHeld = false;
    private bool _secondarybuttonHeld = false;


    private void Start()
    {
        //set classes
        _inputData = GetComponent<InputData>();
        infoBar = GameObject.Find("UXPanel");
        
    }

    void Update()
    {
        //Get values of the Y button and menu button (both on left hand)
        _inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menubuttonValue);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondarybuttonValue);

        //if the menu button is pressed down and wasnt held down the previous frame
        if (menubuttonValue == true && _menubuttonHeld == false)
        {
            //toggle menu
            menu.SetActive(!menu.activeSelf);

            //the button was pressed this current frame
            _menubuttonHeld = true;
        }

        //if the menu button was not pressed this frame
        if (menubuttonValue == false)
        {
            //the button is not being held
            _menubuttonHeld = false;
        }

        if (secondarybuttonValue == true && _secondarybuttonHeld == false)
        {
            infoBar.SetActive(!infoBar.activeSelf);
            _secondarybuttonHeld = true;
        }
        if (secondarybuttonValue == false)
        {
            _secondarybuttonHeld = false;
        }
    }
}
