using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;
using System;
using doubleVector3namespace;

//defines the class that can be attacjed to GameObjects in the Unity Scene
public class Doppler : MonoBehaviour
{
    private GameObject player;
    public GameObject body;

    private Color baseColor;
    public float maxShift;

    private Light bodyLight;

    private doubleVector3 lastplayerPos;
    private doubleVector3 lastbodyPos;

    public static double Clamp01(double value)
    {
        if (value < 0.0)
        {
            return 0.0;
        }
        else if (value > 1.0)
        {
            return 1.0;
        }
        else
        {
            return value;
        }
    }

    // inititialzes the bodyLight variable with the light component attached to the GameObject and sets the colour to baseColor
    void Start()
    {

        player = GameObject.Find("Main Camera");
        
        bodyLight = body.GetComponent<Light>();
        bodyLight.color = baseColor;

        //("COLOR: " + baseColor.ToString());
    }

    // called once per frame - calculates the colour shift based on the relative velocity of the body and user
    void Update()
    {

        //Find positions and velocities of both the player and the body

        doubleVector3 playerPos = player.transform.position;
        doubleVector3 playerVel = (playerPos - lastplayerPos) / Time.deltaTime;

        lastplayerPos = playerPos;
        
        doubleVector3 bodyPos = body.transform.position;
        doubleVector3 bodyVel = (bodyPos - lastbodyPos) / Time.deltaTime;

        lastbodyPos = bodyPos;

        //--------------------------------------------------------

        doubleVector3 observerDir = player.transform.position - body.transform.position;

        doubleVector3 velocity = new doubleVector3(10,10,10); //TEMPORARY, PLEASE REMOVE

        double relativeVelocity = doubleVector3.Dot(velocity, observerDir.normalized);

        // Calculate color shift
        // clamped between 0 and 1 to ensure it doesn't exceed max shift 
        double shiftAmount = Clamp01(relativeVelocity / maxShift); 
        Color shiftedColor = baseColor;

        if (relativeVelocity > 0)
        {
            shiftedColor.r -= (float)shiftAmount;
            shiftedColor.b += (float)shiftAmount;
        }
        else
        {
            shiftedColor.r += (float)shiftAmount;
            shiftedColor.b -= (float)shiftAmount;
        }

        //Debug.Log("SHIFTED COLOUR: " + shiftedColor.ToString());
        bodyLight.color = shiftedColor;
    }
}