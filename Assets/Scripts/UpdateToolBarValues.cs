using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpdateToolBarValues : MonoBehaviour
{
    public TMP_Text speedValue;
    public TMP_Text universeTimeValue;
    public TMP_Text playerTimeValue;

    public Button pauseButton;
    public bool pauseButtonPressed;

    public Button stdButton;
    public bool stdButtonPressed;

    public Scrollbar speedSlider;
    public float speedSliderValue;

    // Start is called before the first frame update
    void Start()
    {
        Button pausebtn = pauseButton.GetComponent<Button>();
        pausebtn.onClick.AddListener(pauseClick);

        Button stdbtn = stdButton.GetComponent<Button>();
        pausebtn.onClick.AddListener(stdClick);
    }

    // Update is called once per frame
    void Update()
    {
        // SET SPEED VALUE
        speedValue.text = "Temp";

        // SET UNIVERSE TIME
        universeTimeValue.text = "Temp";

        // SET PLAYER TIME
        playerTimeValue.text = "Temp";

        //UPDATE SLIDER VALUE
        speedSliderValue = speedSlider.value;
    }

    void pauseClick()
    {
        if (pauseButtonPressed)
        {
            pauseButtonPressed = false;
        }
        else if (!pauseButtonPressed) 
        {
            pauseButtonPressed = true;
        }
    }
    void stdClick()
    {
        if (stdButtonPressed)
        {
            stdButtonPressed = false;
        }
        else if (!stdButtonPressed)
        {
            stdButtonPressed = true;
        }
    }
}

