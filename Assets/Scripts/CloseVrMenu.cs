using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]

public class CloseVrMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject infoBar;
    private InputData _inputData;

    private bool _menubuttonHeld = false;
    private bool _secondarybuttonHeld = false;


    private void Start()
    {
        _inputData = GetComponent<InputData>();
        infoBar = GameObject.Find("UXPanel");
        
        //infoBar.SetActive(false);
    }

    void Update()
    {
        _inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menubuttonValue);
        _inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondarybuttonValue);

        if (menubuttonValue == true && _menubuttonHeld == false)
        {
            menu.SetActive(!menu.activeSelf);
            _menubuttonHeld = true;
        }
        if (menubuttonValue == false)
        {
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
