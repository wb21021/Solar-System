using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
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
                    values[2], // Mass of the celestial body in kg
                    values[3], // Mass of the celestial body in Earth masses
                    values[4], // Mean radius of the celestial body in km
                    values[5], // Mean radius of the celestial body in Earth radii
                    values[6], // Apoapsis of the celestial body's orbit in AU
                    values[7], // Periapsis of the celestial body's orbit in AU
                    values[8], // Argument of periapsis in degrees
                    values[9], // Semi-major axis of the celestial body's orbit in AU
                    values[10], // Inclination of the celestial body's orbit in degrees
                    values[11], // Sidereal orbital period in days
                    values[12], // Sidereal rotational period in days
                    values[13], // Axial tilt of the celestial body in degrees
                    values[14], // Absolute magnitude
                    values[15], // Average density of the celestial body in g/cm^3
                    values[16], // Surface temperature of the celestial body in K
                    values[17], // Surface gravity of the celestial body in g
                    values[18]  // Moons
                );

                celestialBodyScript.bodyName = celestialBodyName;

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
                GameObject hoverUIPrefab = Resources.Load<GameObject>($"UIElements/hoverUIpanel");
                GameObject hoveringUIbox = Instantiate(hoverUIPrefab);
                hoveringUIbox.transform.SetParent(celestialBodyInit.transform);
                hoveringUIbox.transform.localPosition = new Vector3(0, 2, 0);
                hoveringUIbox.GetComponentInChildren<TMP_Text>().text = celestialBodyName;
                hoveringUIbox.SetActive(true);

                //------------------------------------------------------------------
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
}
