using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerSpeedSlider : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider;
    public void PlayerSpeedSliderChanged()
    {
        float rawSliderValue = this.GetComponent<Scrollbar>().value;
        float speed = (rawSliderValue * 4f) + 1f;
        moveProvider.moveSpeed = speed;
    }
}
