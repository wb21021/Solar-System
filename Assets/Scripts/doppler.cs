using UnityEngine;
using UnityEngine.XR;
using System.Collections;
using System.Collections.Generic;
using System;



//defines the class that can be attacjed to GameObjects in the Unity Scene
public class Doppler : MonoBehaviour
{
   
    public GameObject body;

    private Color baseColor;
    public float maxShift;

    private Light bodyLight;

    private Vector3 lastplayerPos;
    private Vector3 lastbodyPos;

    // inititialzes the bodyLight variable with the light component attached to the GameObject and sets the colour to baseColor
    void Start()
    {

        
        
        bodyLight = body.GetComponent<Light>();
        bodyLight.color = baseColor;

        Debug.Log("COLOR: " + baseColor.ToString());
    }

    // called once per frame - calculates the colour shift based on the relative velocity of the body and user
    void Update()
    {

        //Find positions and velocities of both the player and the body

        Vector3 playerPos = playerPos;
        Vector3 playerVel = playerVel;

        Vector3 bodyPos = bodyPos;
        Vector3 bodyVel = bodyVel;


        //--------------------------------------------------------

        Vector3 observerDir = playerPos - bodyPos;

        Vector3 velocity = playerVel;

        float relativeVelocity = Vector3.Dot(velocity, observerDir.normalized);

        // Calculate color shift
        // clamped between 0 and 1 to ensure it doesn't exceed max shift 
        float shiftAmount = Mathf.Clamp01(relativeVelocity / maxShift); 
        Color shiftedColor = baseColor;

        if (relativeVelocity > 0)
        {
            shiftedColor.r -= shiftAmount;
            shiftedColor.b += shiftAmount;
        }
        else
        {
            shiftedColor.r += shiftAmount;
            shiftedColor.b -= shiftAmount;
        }

        Debug.Log("SHIFTED COLOUR: " + shiftedColor.ToString());
        bodyLight.color = shiftedColor;
    }
}
