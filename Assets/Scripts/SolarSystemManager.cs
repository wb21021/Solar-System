using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Rendering;
using System.Linq.Expressions;
using UnityEditor.MemoryProfiler;
using UnityEngine.InputSystem.Utilities;
using doubleVector3namespace;
using System.Data.Common;


public class SolarSystemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultCelestialBodyPrefab;  // Default prefab to use if no match is found
    private Dictionary<string, string> celestialBodyPrefabPaths = new Dictionary<string, string>();
    public string dataFilePath;
    public string prefabsFolder;

    // colection of all the celestial bodies
    public List<CelestialBody> celestialBodiesList = new List<CelestialBody>();

    // Using the plane size as the maximum size possible for the solar system
    public GameObject plane;

    public double scaleDist;
    public double scaleSize;

    public TMP_Text PlanetNameText;

    void Start()
    {
        // Populate the dictionary with celestial body names and prefab paths
        PopulatePrefabPathsDictionary();

        string[] lines = System.IO.File.ReadAllLines(dataFilePath);

        for (int i = 1; i< lines.Length; i++)
        {
            string line = lines[i];
            string[] values = line.Split(',');

            // Get the name from the CSV data
            string celestialBodyName = values[0].Trim();

            // Determine the prefab path to use based on the name
            string prefabPath;
            GameObject celestialBodyPrefab;
            if (celestialBodyPrefabPaths.TryGetValue(celestialBodyName, out prefabPath))
            {
                // Load the prefab dynamically
                celestialBodyPrefab = Resources.Load<GameObject>(prefabPath);
            }
            else
            {
                Debug.LogWarning($"No prefab path found for celestial body: {celestialBodyName}");
                celestialBodyPrefab = defaultCelestialBodyPrefab;
            }
            // Instantiate celestial body prefab
            GameObject celestialBodyInit = Instantiate(celestialBodyPrefab);
            //Debug.Log("instantiated");
            celestialBodyInit.SetActive(true);
            CelestialBody celestialBodyScript = celestialBodyInit.GetComponent<CelestialBody>();
                
            // Set properties based on CSV data
            float centralBodyMass;
            if (!float.TryParse(values[24], out centralBodyMass)){
                centralBodyMass = 0;
            }
            celestialBodyScript.SetPropertiesFromData(
                    i, // ID of the celestial body
                    float.Parse(values[2]), // Mass of the celestial body in kg
                    float.Parse(values[3]), // Mass of the celestial body in Earth masses
                    float.Parse(values[4]), // Mean radius of the celestial body in km
                    float.Parse(values[5]), // Mean radius of the celestial body in Earth radii
                    float.Parse(values[6]), // Apoapsis of the celestial body's orbit in AU
                    float.Parse(values[7]), // Periapsis of the celestial body's orbit in AU
                    float.Parse(values[8]), // Argument of periapsis in degrees
                    float.Parse(values[9]), // Semi-major axis of the celestial body's orbit in AU
                    float.Parse(values[10]), // Inclination of the celestial body's orbit in degrees
                    float.Parse(values[11]), // Sidereal orbital period in days
                    float.Parse(values[12]), // Sidereal rotational period in days
                    float.Parse(values[13]), // Axial tilt of the celestial body in degrees
                    float.Parse(values[14]), // Absolute magnitude
                    float.Parse(values[15]), // Average density of the celestial body in g/cm^3
                    float.Parse(values[16]), // Surface temperature of the celestial body in K
                    float.Parse(values[17]), // Surface gravity of the celestial body in g
                    int.Parse(values[18]),   // Moons (number of moons)
                    float.Parse(values[19]), // Ring radius
                    float.Parse(values[20]), // Ring depth
                    float.Parse(values[21]), // Eccentricity of the celestial body's orbit
                    float.Parse(values[22]), // Longitude of the ascending node in degrees
                    float.Parse(values[23]),  // True Anomaly in degrees

                    centralBodyMass,  // Mass of the central body
                    values[26].Replace(";",","), // Notes
                    values[27] // Color

                );
            //Debug.Log(values[26].Replace(";", ","));
            celestialBodyScript.bodyName = celestialBodyName;

            celestialBodiesList.Add(celestialBodyScript);

            //Added by Iris ---------------------------------------------------

            //Set tag so that object can be highlighted
            celestialBodyInit.tag = "interactable";


             
            //Create small sphere within the celestial body, so that if the body shrinks down too small,
            //this sphere will always be visible.
            GameObject MinimumSizeBodyPrefab = Resources.Load<GameObject>($"UIElements/MinimumSizeBody");
            GameObject MinimumSizeBody = Instantiate(MinimumSizeBodyPrefab);
            MinimumSizeBody.tag = "interactable";
            MinimumSizeBody.transform.SetParent(celestialBodyInit.transform);
            MinimumSizeBody.transform.localPosition = new Vector3(0, 0, 0);


            //Create text box over planet
            GameObject hoverUIPrefab = Resources.Load<GameObject>($"UIElements/CanvasCelestialBodyInfo");
            GameObject hoveringUIbox = Instantiate(hoverUIPrefab);
            hoveringUIbox.transform.SetParent(celestialBodyInit.transform);
            hoveringUIbox.transform.localPosition = new Vector3(0, 2, 0);
            hoveringUIbox.GetComponentInChildren<TMP_Text>().text = celestialBodyName;
            hoveringUIbox.SetActive(true);

                

                //------------------------------------------------------------------
        }
        // Set the initial positions and velocities of the celestial bodies
        InitialiseCelestialBodies();

        // Use the position components to position the assests and scale the in the scene
        // foreach (CelestialBody celestialBody in celestialBodiesList)
        // {
        //     celestialBody.transform.position = celestialBody.pos;
        //     celestialBody.transform.localScale = new doubleVector3(celestialBody.radius, celestialBody.radius, celestialBody.radius);
        // }


    }

    private void PopulatePrefabPathsDictionary()
    {
        // Get all prefabs in the specified folder
        GameObject[] celestialBodyPrefabs = Resources.LoadAll<GameObject>(prefabsFolder);

        // Add celestial body names and prefab paths to the dictionary
        foreach (var prefab in celestialBodyPrefabs)
        {
            string celestialBodyName = prefab.name;
            string prefabPath = $"{prefabsFolder}/{celestialBodyName}";
            celestialBodyPrefabPaths.Add(celestialBodyName, prefabPath);
        }
    }

    public void InitialiseCelestialBodies()
    {
        double distFromSun = 0;
        double newDistFromSun = 0;
        double largestScale = 0;
        double newLargestScale = 0;
        doubleVector3 planeScale2D = plane.transform.localScale;
        doubleVector3 planeScale = new doubleVector3(planeScale2D.x, planeScale2D.x,planeScale2D.z);
        
        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.CalculateInitialPositionVelocity();
            newDistFromSun = celestialBody.DistanceFromSun();
            if (newDistFromSun > distFromSun)
            {
                distFromSun = newDistFromSun;
            }
            newLargestScale = new doubleVector3(celestialBody.radius, celestialBody.radius, celestialBody.radius).magnitude;
            if (newLargestScale > largestScale)
            {
                largestScale = newLargestScale;
            }
        }
        
        // Offset the moons
        offsetMoons();

        scaleDist = planeScale.magnitude/distFromSun;
        //Debug.Log(largestScale);
        scaleSize = (planeScale.magnitude*0.01f)/largestScale;
    }

    private void UpdateGravitationalAcceleration(CelestialBody celestialBody)
    {
        celestialBody.acc = doubleVector3.zero;
        doubleVector3 tempAcc = doubleVector3.zero;

        foreach (CelestialBody otherCelestialBody in celestialBodiesList)
        {
            if (celestialBody.id != otherCelestialBody.id)
            {
                // Displacment vector from the current celestial body to the other celestial body
                doubleVector3 r = otherCelestialBody.pos - celestialBody.pos;
                //Debug.Log(otherCelestialBody.pos + " " + celestialBody.pos + " THIS IS THE DISTANCE");

                if (r.magnitude != 0.0f)
                {
                    // Gravitational force between the two celestial bodies (factored out G and mass of body to save calculations)
                    //Debug.Log(r.magnitude + " THIS IS THE MAGNITEUED");
                    tempAcc +=  r.normalized * otherCelestialBody.mass / Math.Pow(r.magnitude, 2);
                }
            }
        }
        // Multiply by G to get the acceleration
        celestialBody.acc = 6.67430e-11f * tempAcc;
    }


    private void offsetMoons()
    {
        // Mass of the sun
        float massOfSun = 1.99e30f; // placeholder with the mass of our sun 1.99E+30

        // Updates the mass of the sun to the actual mass of the sun if it differs
        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            if (celestialBody.bodyName == "Sun")
            {
                massOfSun = celestialBody.mass;
            }
        }

        // Find the central body of each celestial body if it is not the sun
        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            if (celestialBody.massOfCentralBody != massOfSun)
            {
                foreach (CelestialBody otherCelestialBody in celestialBodiesList)
                {
                    if (celestialBody.massOfCentralBody == otherCelestialBody.mass)
                    {
                        // Set the central body of the celestial body as the other celestial body
                        celestialBody.isMoon = otherCelestialBody.id;
                        // offset the body.
                        celestialBody.pos = otherCelestialBody.pos;  // + new doubleVector3(celestialBody.semiMajorAxis, 0, 0); ???
                    }
                }
            }
        }

    }

    void FixedUpdate()
    {
        float customTimeScale = 400000.0f;
        float dt = Time.fixedDeltaTime * customTimeScale;
        Debug.Log(dt*Time.timeScale + "TJIOS IS THE DT");

        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            // Verlet Integration

            // half step velocity
            celestialBody.vel += 0.5f * dt * celestialBody.acc;
            celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

            Debug.Log(celestialBody.vel + "VEL");

            // full step position (using half step velocity)
            celestialBody.pos += celestialBody.vel * dt;
            Vector3 newPos = celestialBody.pos.ToVector3();
            celestialBody.transform.position = newPos*(float)scaleDist;

            // update acceleration (using new position)
            UpdateGravitationalAcceleration(celestialBody);

            // half step velocity (using updated acceleration)
            celestialBody.vel += 0.5f * dt * celestialBody.acc;
        }
        offsetMoons();
        
    }
}

