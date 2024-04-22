using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    //called when the reset button is pressed
    public void ResetSceneButton()
    {
        //find the name of the current (only) scene and load it, while unloading the current one
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
