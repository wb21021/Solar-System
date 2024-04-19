using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update

    public ContinuousMoveProviderBase moveProvider;
    public SolarSystemManager solarSystemManager;
    public CloseVrMenu CloseOpenVrMenu;

    void Start()
    {
        moveProvider.moveSpeed = 0f;


    }

    public void OnStartClick()
    {
        moveProvider.moveSpeed = 1f;

        Destroy(this.transform.parent.gameObject);

        solarSystemManager.Init();
        solarSystemManager.CreateButtons();
        CloseOpenVrMenu.hasSimulationStarted = true;
    }
}
