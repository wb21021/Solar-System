using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseVrMenu : MonoBehaviour
{
    public GameObject menu;
    
    public void Close()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
