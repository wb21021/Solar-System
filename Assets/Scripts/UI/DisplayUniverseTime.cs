using TMPro;
using UnityEngine;

public class DisplayUniverseTime : MonoBehaviour
{
    public SolarSystemManager SolarSystemManager;

    private bool started = false;

    void Update()
    {
        if (started)
        {
            this.GetComponent<TMP_Text>().text = SolarSystemManager.GetDateTimeString();
        }
        
    }

    public void StartedButtonPress()
    {
        started = true;
    }
}
