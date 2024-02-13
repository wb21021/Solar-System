using UnityEngine;


//defines the class that can be attacjed to GameObjects in the Unity Scene
public class Doppler : MonoBehaviour
{
    public Vector3 velocity;
    public GameObject observer;
    public Color baseColor;
    public float maxShift;

    private Light bodyLight;

    
    // inititialzes the bodyLight variable with the light component attached to the GameObject and sets the colour to baseColor
    void Start()
    {
        bodyLight = GetComponent<Light>();
        bodyLight.color = baseColor;
    }

    // called once per frame - calculates the colour shift based on the relative velocity of the body and user
    void Update()
    {
        Vector3 observerDir = observer.transform.position - transform.position;
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

        bodyLight.color = shiftedColor;
    }
}
