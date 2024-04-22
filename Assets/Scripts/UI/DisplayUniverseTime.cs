using TMPro;
using UnityEngine;

public class DisplayUniverseTime : MonoBehaviour
{
    public SolarSystemManager SolarSystemManager;

    private bool started = false;

    void Update()
    {
        //If the Start menu 'Start' button has been pressed
        if (started)
        {
            //set the watch text to the current universe time
            this.GetComponent<TMP_Text>().text = SolarSystemManager.GetDateTimeString();
        }
        
    }

    public void StartedButtonPress()
    {
        started = true;
    }
}
