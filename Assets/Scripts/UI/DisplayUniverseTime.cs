using TMPro;
using UnityEngine;

public class DisplayUniverseTime : MonoBehaviour
{
    public SolarSystemManager SolarSystemManager;


    void Update()
    {
        this.GetComponent<TMP_Text>().text = SolarSystemManager.GetDateTimeString();
    }
}
