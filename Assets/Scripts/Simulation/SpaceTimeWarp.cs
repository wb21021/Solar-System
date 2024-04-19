using Unity.VisualScripting;
using UnityEngine;

public class GravitationalPotentialCalculator : MonoBehaviour
{
    public float gravitationalConstant = 6.67430e-11f; // Gravitational constant in m^3 kg^-1 s^-2

    public void CalculateGravitationalPotentials(GameObject body)
    {
        GameObject planePrefab = Resources.Load("UIElements/SpaceTimePlane") as GameObject;
        GameObject plane = Instantiate(planePrefab, body.transform) as GameObject;


        Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;
        float[] gravitationalPotentials = new float[planeMesh.vertices.Length];


        Vector3 objectPosition = new Vector3(0, 0, 0);
        float planetMass = body.GetComponent<CelestialBody>().mass;

        for (int i = 0; i < planeMesh.vertices.Length; i++)
        {

            Vector3 vertexPosition = planeMesh.vertices[i];

            float maxPotential = float.MinValue; // Initialize maxPotential as the smallest possible float value

                float distance = Vector3.Distance(vertexPosition, objectPosition);
                

            // Ensure distance is not zero to avoid division by zero
            if (distance > 0)
            {
                // Calculate gravitational potential due to this planet and update maxPotential if necessary
                float potential = CalculateGravitationalPotential(distance, planetMass);

                if (potential > maxPotential)
                {
                    maxPotential = potential;
                }
            }
            

            gravitationalPotentials[i] = maxPotential;
        }

        // Calculate the maximum gravitational potential to scale the warp effect appropriately
        AnimateWarp(gravitationalPotentials, planeMesh);
    }

    float CalculateGravitationalPotential(float distance, float mass)
    {
        return (gravitationalConstant * mass) / (distance * 1E31f); // Gravitational potential formula (without negative sign)
    }

    void AnimateWarp(float[] gravitationalPotentials,Mesh planeMesh) 
    {
        // Calculate the strength of the warp effect based on the maximum gravitational potential
        float maxPot = Mathf.Max(gravitationalPotentials);


        // Apply the warp effect to the space-time plane
        Vector3[] vertices = planeMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            float scaledPot = (gravitationalPotentials[i]/maxPot)*2;
            scaledPot = Mathf.Clamp(scaledPot, 0, 5);
            if(scaledPot == 0) 
            {
                scaledPot = 5f;
            }
            

            vertices[i] = new Vector3(vertices[i].x, -scaledPot, vertices[i].z); // Adjust the y-coordinate based on the gravitational potential
        }

        planeMesh.vertices = vertices;
        planeMesh.RecalculateBounds(); // Recalculate bounds to ensure proper rendering
    }   
}