using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using doubleVector3namespace;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultCelestialBodyPrefab;  // Default prefab to use if no match is found
    private Dictionary<string, string> celestialBodyPrefabPaths = new Dictionary<string, string>();
    public string dataFilePath;
    private long initialisationTime; // the time and date of the data in the csv (2nd of feb 2024)
    private long simulationTime; // the time of the simulation (changes as the simulation runs)
    private uint IterPerFrame = 1; // Number of iterations per frame
    public string prefabsFolder;

    // colection of all the celestial bodies
    public List<CelestialBody> celestialBodiesList = new List<CelestialBody>();

    // Using the plane size as the maximum size possible for the solar system
    public GameObject plane;

    public GameObject WholeSolarSystem;

    public double scaleDist;
    public double scaleSize;

    public TMP_Text PlanetNameText;

    //BUILD DEBUG ONLY
    public TMP_Text buildDebugLog;



    //TIMESTEP (DEFAULT 400000f)
    public float customTimeScale = 400000.0f;

    void Start()
    {
        //IF YOURE NOT ABLE TO RUN THIS IN VR, UNCOMMENT THIS LINE SO THE SIMULATION RUNS ON STARTUP

        //Init();
    }
    public void Init()
    {

        // Set the initialisation time to the 2nd of February 2024
        string dateString = "2024-02-02 00:00:00"; 
        DateTime dateTime;
        if (DateTime.TryParse(dateString, out dateTime))
        {
            initialisationTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }
        else 
        {
            Debug.LogError("Failed to parse date");
        }


        //Debug build ONLY
        buildDebugLog.text = "START";

        // Populate the dictionary with celestial body names and prefab paths
        PopulatePrefabPathsDictionary();


        string text = Resources.Load<TextAsset>(dataFilePath).text;

        string[] lines = text.Split('\n');
        
        //Debug build only
        buildDebugLog.text = buildDebugLog.text + "\nLoading: " + lines.Length + " bodies";

        for (int i = 1; i< lines.Length; i++)
        {
            string line = lines[i];
            string[] values = line.Split(',');

            // Get the name from the CSV data
            string celestialBodyName = values[0].Trim();


            //Debug build only
            buildDebugLog.text = buildDebugLog.text + "\nLoading: " + celestialBodyName + " bodies";



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

                //Debug build only
                buildDebugLog.text = buildDebugLog.text + "\nNo prefab found for body, defaulting.";
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
                    i-1, // ID of the celestial body
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
                    values[27].Trim() // Color

                );

            //Debug build only
            buildDebugLog.text = buildDebugLog.text + "\nProperties set";

            celestialBodyScript.bodyName = celestialBodyName;

            celestialBodiesList.Add(celestialBodyScript);

            //Added by Iris ---------------------------------------------------

            celestialBodyInit.transform.parent = WholeSolarSystem.transform;



            //Only create a hovering text box if its a proper planet / the sun
            if (values[25] == "Sun" || values[25] == "N/A")
            {
                //Create text box over planet
                GameObject hoverUIPrefab = Resources.Load<GameObject>($"UIElements/CanvasCelestialBodyInfo");
                GameObject hoveringUIbox = Instantiate(hoverUIPrefab);
                hoveringUIbox.transform.SetParent(celestialBodyInit.transform);
                hoveringUIbox.transform.localPosition = new Vector3(0, 1, 0);
                hoveringUIbox.GetComponentInChildren<TMP_Text>().text = celestialBodyName;
                //hoveringUIbox.transform.localScale = new Vector3((float)scaleSize,(float)scaleSize,(float)scaleSize);
                hoveringUIbox.SetActive(true);

                //Debug build only
                buildDebugLog.text = buildDebugLog.text + "\nHoveringUIBox set";
            }
            

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
        scaleSize = scaleDist;//(planeScale.magnitude*0.005f)/largestScale;

        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.transform.localPosition = celestialBody.posDouble.ToVector3() * (float)scaleDist;
            celestialBody.GetComponent<TrailRenderer>().Clear();
        }
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
                        celestialBody.pos += otherCelestialBody.pos;  // + new doubleVector3(celestialBody.semiMajorAxis, 0, 0); ???
                        // add the velocity of the central body to the moon
                        celestialBody.vel += otherCelestialBody.vel;
                    }
                }
            }
        }

    }

    // Get Datetime
    public string GetDateTimeString()
    {
        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(initialisationTime + simulationTime).UtcDateTime;
        // Debug.Log(dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public long GetDateTimeNum()
    {
        return simulationTime;
    }

    void FixedUpdate()
    {
        for (int n = 0; n < IterPerFrame; n++)
        {
            yoshidaMethod();
        }
    }

    void velocitiesVerletMethod()
    {
        float dt = Time.fixedDeltaTime * customTimeScale / IterPerFrame;
        simulationTime += (long)(dt) ;
        Debug.Log(dt*Time.timeScale + "TJIOS IS THE DT");

        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            // Verlet Integration

            // half step velocity
            celestialBody.vel += 0.5f * dt * celestialBody.acc;
            celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

            // Debug.Log(celestialBody.vel + "VEL");

            // full step position (using half step velocity)
            celestialBody.pos += celestialBody.vel * dt;
            Vector3 newPos = celestialBody.pos.ToVector3();
            celestialBody.transform.localPosition = newPos*(float)scaleDist;

            // update acceleration (using new position)
            UpdateGravitationalAcceleration(celestialBody);

            // half step velocity (using updated acceleration)
            celestialBody.vel += 0.5f * dt * celestialBody.acc;
        }
    }
    void rungeKuttaMethod()
    {
        float dt = Time.fixedDeltaTime * customTimeScale / IterPerFrame;
        simulationTime += (long)(dt) ;

        foreach(CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

            // initial pos and vel
            doubleVector3 initialPos = celestialBody.pos;
            doubleVector3 initialVel = celestialBody.vel;
            
            // calculate k1
            UpdateGravitationalAcceleration(celestialBody);
            doubleVector3 k1v = dt * celestialBody.acc;
            doubleVector3 k1r = dt * initialVel;

            // calculate k2
            celestialBody.pos = initialPos + 0.5f * k1r;
            celestialBody.vel = initialVel + 0.5f * k1v;
            UpdateGravitationalAcceleration(celestialBody);
            doubleVector3 k2v = dt * celestialBody.acc;
            doubleVector3 k2r = dt * celestialBody.vel;

            // calculate k3
            celestialBody.pos = initialPos + 0.5f * k2r;
            celestialBody.vel = initialVel + 0.5f * k2v;
            UpdateGravitationalAcceleration(celestialBody);
            doubleVector3 k3v = dt * celestialBody.acc;
            doubleVector3 k3r = dt * celestialBody.vel;

            // calculate k4
            celestialBody.pos = initialPos + k3r;
            celestialBody.vel = initialVel + k3v;
            UpdateGravitationalAcceleration(celestialBody);
            doubleVector3 k4v = dt * celestialBody.acc;
            doubleVector3 k4r = dt * celestialBody.vel;

            // update position and velocity
            celestialBody.pos = initialPos + (1.0f / 6.0f) * (k1r + 2.0f * k2r + 2.0f * k3r + k4r);
            celestialBody.vel = initialVel + (1.0f / 6.0f) * (k1v + 2.0f * k2v + 2.0f * k3v + k4v);

            Vector3 newPos = celestialBody.pos.ToVector3();
            celestialBody.transform.localPosition = newPos*(float)scaleDist;
        }
    }

    // Old yoshida method (Legacy)
    // void yoshidaMethod()
    // {
    //     // fourth order numerical integrator using the Yoshida method

    //     float customTimeScale = 400000.0f;
    //     float dt = Time.fixedDeltaTime * customTimeScale / IterPerFrame;
    //     simulationTime += (long)(dt) ;

    //     float third = 1.0f / 3.0f;

    //     double w_0 = - Mathf.Pow(2, third) / (2 - Mathf.Pow(2, third));
    //     double w_1 = 1 / (2 - Mathf.Pow(2, third));

    //     double c_1 = w_1 / 2;
    //     double c_2 = (w_0 + w_1) / 2;
    //     double c_3 = c_2;
    //     double c_4 = c_1;

    //     double d_1 = w_1;
    //     double d_2 = w_0;
    //     double d_3 = w_1;

    //     foreach(CelestialBody celestialBody in celestialBodiesList)
    //     {
    //         celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

    //         // first step
    //         //////////////////////////////////////////
    //         // update pos (x_1)
    //         celestialBody.pos = celestialBody.pos + (c_1 * dt * celestialBody.vel);
    //         // update acc
    //         UpdateGravitationalAcceleration(celestialBody);
    //         // update vel (v_1)
    //         celestialBody.vel = celestialBody.vel + (d_1 * dt * celestialBody.acc);



    //         // second step
    //         //////////////////////////////////////////
    //         // update pos (x_2)
    //         celestialBody.pos = celestialBody.pos + (c_2 * dt * celestialBody.vel);
    //         // update acc
    //         UpdateGravitationalAcceleration(celestialBody);
    //         // update vel (v_2)
    //         celestialBody.vel = celestialBody.vel + (d_2 * dt * celestialBody.acc);



    //         // third step
    //         //////////////////////////////////////////
    //         // update pos (x_3)
    //         celestialBody.pos = celestialBody.pos + (c_3 * dt * celestialBody.vel);
    //         // update acc
    //         UpdateGravitationalAcceleration(celestialBody);
    //         // update vel (v_3)
    //         celestialBody.vel = celestialBody.vel + (d_3 * dt * celestialBody.acc);



    //         // full step (last step)
    //         //////////////////////////////////////////
    //         // update pos (x_4)
    //         celestialBody.pos = celestialBody.pos + (c_4 * dt * celestialBody.vel);
    //         // update vel (v_4 = v_3) 
    //         // celestialBody.vel = celestialBody.vel;



    //         // update position of the asset
    //         Vector3 newPos = celestialBody.pos.ToVector3();
    //         celestialBody.transform.localPosition = newPos*(float)scaleDist;
    //     }   
    // }

    private void yoshidaMethod()
    {
        // fourth order numerical integrator using the Yoshida method

        float dt = Time.fixedDeltaTime * customTimeScale / IterPerFrame;
        simulationTime += (long)(dt);

        float third = 1.0f / 3.0f;

        double w_0 = - Mathf.Pow(2, third) / (2 - Mathf.Pow(2, third));
        double w_1 = 1 / (2 - Mathf.Pow(2, third));

        double[] c = { w_1 / 2, (w_0 + w_1) / 2, (w_0 + w_1) / 2, w_1 / 2 };
        double[] d = { w_1, w_0, w_1 };

        foreach(CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

            for (int i = 0; i < 4; i++)
            {
                // update pos
                celestialBody.pos += c[i] * dt * celestialBody.vel;
                // update acc
                UpdateGravitationalAcceleration(celestialBody);
                // update vel
                if (i < 3) // no velocity update on the last step
                {
                    celestialBody.vel += d[i] * dt * celestialBody.acc;
                }
            }

            // update position of the asset
            Vector3 newPos = celestialBody.pos.ToVector3();
            celestialBody.transform.localPosition = newPos*(float)scaleDist;
        }   
    }

    private void yoshidaMethod_6() 
    {
        // sixth order numerical integrator using the Yoshida method

        float dt = Time.fixedDeltaTime * customTimeScale / IterPerFrame;
        simulationTime += (long)(dt) ;

        // Coefficients for the 6th order Yoshida integrator

        double[] c = new double[] {0.392256805238780, 0.510043411918458, -0.471053385409757, 0.068753168252518, 0.068753168252518, -0.471053385409757, 0.510043411918458, 0.392256805238780};
        double[] d = new double[] {0.784513610477560, 0.235573213359357, -1.177679984178870, 1.315186320683906, -1.177679984178870, 0.235573213359357, 0.784513610477560, 0};

        foreach (CelestialBody celestialBody in celestialBodiesList)
        {
            celestialBody.vel = new doubleVector3(double.IsNaN(celestialBody.vel.x) ? 0 : celestialBody.vel.x, double.IsNaN(celestialBody.vel.y) ? 0 : celestialBody.vel.y, double.IsNaN(celestialBody.vel.z) ? 0 : celestialBody.vel.z);

            for (int i = 0; i < 8; i++)
            {
                // update pos
                celestialBody.pos += c[i] * dt * celestialBody.vel;
                // update acc
                UpdateGravitationalAcceleration(celestialBody);
                // update vel
                if (i < 7) // no velocity update on the last step
                {
                    celestialBody.vel += d[i] * dt * celestialBody.acc;
                }
            }

            // update position of the asset
            Vector3 newPos = celestialBody.pos.ToVector3();
            celestialBody.transform.localPosition = newPos*(float)scaleDist;
        }
    }

}

