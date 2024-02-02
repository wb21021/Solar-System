using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultCelestialBodyPrefab;  // Default prefab to use if no match is found
    [SerializeField]
    private Dictionary<string, string> celestialBodyPrefabPaths = new Dictionary<string, string>();

    [SerializeField]
    public string dataFilePath;
    [SerializeField]
    public string prefabsFolder;

    // colection of all the celestial bodies
    public List<CelestialBody> celestialBodiesList = new List<CelestialBody>();

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
            if (celestialBodyPrefabPaths.TryGetValue(celestialBodyName, out prefabPath))
            {
                // Load the prefab dynamically
                GameObject celestialBodyPrefab = Resources.Load<GameObject>(prefabPath);

                // If no matching prefab is found, use the default prefab
                if (celestialBodyPrefab == null)
                {
                    celestialBodyPrefab = defaultCelestialBodyPrefab;
                }

                // Instantiate celestial body prefab
                GameObject celestialBodyInit = Instantiate(celestialBodyPrefab);
                Debug.Log("instantiated");
                celestialBodyInit.SetActive(true);
                CelestialBody celestialBodyScript = celestialBodyInit.GetComponent<CelestialBody>();
                
                // Set properties based on CSV data
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
                    int.Parse(values[18])    // Moons (number of moons)
                );

                celestialBodyScript.bodyName = celestialBodyName;

                celestialBodiesList.Add(celestialBodyScript);
            }
            else
            {
                Debug.LogWarning($"No prefab path found for celestial body: {celestialBodyName}");
                GameObject celestialBodyPrefab = defaultCelestialBodyPrefab;
                GameObject celestialBodyInit = Instantiate(celestialBodyPrefab);
                CelestialBody celestialBodyScript = celestialBodyInit.GetComponent<CelestialBody>();
            }
        }
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
        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.calculate_initial_position_velocity();
        }
    }

    private void UpdateGravitationalAcceleration(CelestialBody celestialBody)
    {   
        celestialBody.acc = Vector3.zero;

        foreach (CelestialBody otherCelestialBody in celestialBodiesList)
        {
            if (celestialBody != otherCelestialBody)
            {
                Vector3 r = otherCelestialBody.pos - celestialBody.pos;
                float distance = r.magnitude;

                float force = (6.67430f * Mathf.Pow(10, -11) * otherCelestialBody.mass) / Mathf.Pow(distance, 2);
                Vector3 accFromOther = r.normalized * force;
                celestialBody.acc += accFromOther;
            }
        }
    }

    public void VerletUpdateCelestialBodies()
    {
        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            // verlet integration

            // half step velocity
            celestialBody.vel = celestialBody.vel + ( 0.5f * celestialBody.acc * Time.fixedDeltaTime );

            // full step position (using half step velocity)
            celestialBody.pos = celestialBody.pos + ( celestialBody.vel * Time.fixedDeltaTime );

            // update acceleration (using new position)
            UpdateGravitationalAcceleration(celestialBody);

            // half step velocity (using updated acceleration)
            celestialBody.vel = celestialBody.vel + ( 0.5f * celestialBody.acc * Time.fixedDeltaTime );
        }
    }
}

