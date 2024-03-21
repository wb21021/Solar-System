using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]

public class CloseVrMenu : MonoBehaviour
{

    //This class deals with toggling the Left and Right hand information panels

    //right hand 'options menu'
    public GameObject OptionsMenu;

    //left hand 'information tab'
    public GameObject InfoMenu;
    public InputData _inputData;

    //values to check if button is being held down, so that the toggle isnt triggered on and off every frame
    private bool _menubuttonHeld = false;
    private bool _secondarybuttonHeld = false;

    void Start()
    {
      InfoMenu.SetActive(false);
      OptionsMenu.SetActive(false);
    }
    void Update()
    {
        //Get values of the Y button and menu button (both on left hand)
        _inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menubuttonValue);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondarybuttonValue);

        //if the menu button is pressed down and wasnt held down the previous frame
        if (menubuttonValue == true && _menubuttonHeld == false)
        {
            //Turn off other menu so they dont overlap
            if (InfoMenu.activeSelf)
            {
                InfoMenu.SetActive(false);
            }

            //toggle menu
            OptionsMenu.SetActive(!OptionsMenu.activeSelf);

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
            //Turn off other menu so they dont overlap
            if (OptionsMenu.activeSelf)
            {
                OptionsMenu.SetActive(false);
            }

            InfoMenu.SetActive(!InfoMenu.activeSelf);
            _secondarybuttonHeld = true;
        }

        if (secondarybuttonValue == false)
        {
            _secondarybuttonHeld = false;
        }
    }
    public List<GameObject> GetUI()
    {
        List<GameObject> UIElements = new List<GameObject>
        {
            InfoMenu,
            OptionsMenu
        };

        return UIElements;
    }
}
