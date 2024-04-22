using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerSpeedSlider : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider;

    //called when the Player Speed slider is updated
    public void PlayerSpeedSliderChanged()
    {
        //take the slider value from 0-1 and convert it into a useable speed
        float rawSliderValue = this.GetComponent<Scrollbar>().value;
        float speed = (rawSliderValue * 4f) + 1f;

        //update the move providers speed
        moveProvider.moveSpeed = speed;
    }
}
