using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartButton : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider;
    public SolarSystemManager solarSystemManager;
    public CloseVrMenu CloseOpenVrMenu;

    void Start()
    {
        moveProvider.moveSpeed = 0f;


    }

    //Called when the Start button is pressed on the StartMenu
    public void OnStartClick()
    {
        //Set player speed to 1
        moveProvider.moveSpeed = 1f;

        //delete the startmenu
        Destroy(this.transform.parent.gameObject);

        //start simulation
        solarSystemManager.Init();
        solarSystemManager.CreateButtons();

        //let the CloseVrMenu.cs know script that the simulation has started
        CloseOpenVrMenu.hasSimulationStarted = true;
    }
}
