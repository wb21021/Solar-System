using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using doubleVector3namespace;
using Unity.XR.CoreUtils;

public class CelestialBody : MonoBehaviour
{
    // Properties of the celestial body
    public int id;                 // ID of the celestial body
    public string bodyName;        // Name of the celestial body
    public float mass;             // Mass of the celestial body in kg
    public float massEarth;        // Mass of the celestial body in Earth masses
    public float radius;           // Mean radius of the celestial body in km
    public float radiusEarth;      // Mean radius of the celestial body in Earth radii
    public float apoapsis;         // Apoapsis of the celestial body's orbit in AU
    public float periapsis;        // Periapsis of the celestial body's orbit in AU
    public float argOfPeriapsis;   // Argument of periapsis in degrees
    public float semiMajorAxis;    // # Semi-major axis of the celestial body's orbit in AU #
    public float inclination;      // # Inclination of the celestial body's orbit in degrees #
    public float eccentricity;     // # Eccentricity of the celestial body's orbit #
    public float longditudeOfAscendingNode; // # Longitude of the ascending node in degrees #
    public float argOfPerihelion;  // # Argument of periapsis in degrees #
    public float trueAnomaly;      // # Mean anomaly in degrees #
    public float gravParameter;      // # Mean anomaly in degrees #
    public float orbitalPeriod;    // Sidereal orbital period in days
    public float rotationalPeriod; // Sidereal rotational period in days
    public float axialTilt;        // Axial tilt of the celestial body in degrees
    public float absoluteMagnitude;// Absolute magnitude
    public float averageDensity;   // Average density of the celestial body in g/cm^3
    public float surfaceTemp;      // Surface temperature of the celestial body in K
    public float surfaceGravity;   // Surface gravity of the celestial body in g
    public int hasMoons;           // Number of moons
    public float ringRadius;       // Ring radius
    public float ringDepth;        // Ring depth
    public float massOfCentralBody; // Mass of the central body
    public int isMoon;            // if not a moon = 0, if a moon = index of the planet it orbits    
    public string notes;
    public Color descColor = Color.white;
    public float nthMoon; // nth moon from its planet, needed for icon offset

    public List<CelestialBody> moonsList; //list of moons this body has

    //References for the UI Elements
    private GameObject InfoBar; //Information tab
    private GameObject UXPanel; //Parent of the information tab, contains the visual body as well
    private GameObject OptionsMenu; //Settings
    public GameObject VisualBodyPrefab; //Prefab of this object
    private GameObject VisualBody = null; //Reference of the currently selected object's body
    public CloseVrMenu closevrmenuScript; //CloseVrMenu.cs reference

    public doubleVector3 pos;            // Position vector
    public doubleVector3 posDouble; // Position vector (using double precision)
    public doubleVector3 vel;            // Velocity vector
    public doubleVector3 acc;            // Acceleration vector

    public List<float> wavelengths = new List<float>();


    private doubleVector3 V_observer;
    private const double c = 299792458;

    private Transform transform; // for position

    private GameObject solarSystemManager;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI nameText;

    public RectTransform button;

    private SolarSystemManager solarScript;
    private double scaleDist;
    private double scaleSize;

    //private Rigidbody rigidbody; // for velocity
    private DopplerGraph dopplerGraph;

    // Set properties based on data from the CSV file
    public void SetPropertiesFromData(int id, 
                                      float mass, float massEarth, float radius, float radiusEarth,
                                      float apoapsis, float periapsis, float argOfPeriapsis,
                                      float semiMajorAxis, float inclination, float orbitalPeriod,
                                      float rotationalPeriod, float axialTilt, float absoluteMagnitude,
                                      float averageDensity, float surfaceTemp, float surfaceGravity,
                                      int hasMoons, float ringRadius, float ringDepth,
                                      float eccentricity, float longditudeOfAscendingNode, 
                                      float trueAnomaly, float massOfCentralBody, string notes, string descColor, float nthMoon)
    {
        this.id = id;
        this.mass = mass;
        this.massEarth = massEarth;
        this.radius = radius;
        this.radiusEarth = radiusEarth;
        this.apoapsis = apoapsis;
        this.periapsis = periapsis;
        this.argOfPeriapsis = argOfPeriapsis;
        this.semiMajorAxis = semiMajorAxis;
        this.inclination = inclination;
        this.orbitalPeriod = orbitalPeriod;
        this.rotationalPeriod = rotationalPeriod;
        this.axialTilt = axialTilt;
        this.absoluteMagnitude = absoluteMagnitude;
        this.averageDensity = averageDensity;
        this.surfaceTemp = surfaceTemp;
        this.surfaceGravity = surfaceGravity;
        this.hasMoons = hasMoons;
        this.ringRadius = ringRadius;
        this.ringDepth = ringDepth;
        this.eccentricity = eccentricity;
        this.longditudeOfAscendingNode = longditudeOfAscendingNode;
        this.trueAnomaly = trueAnomaly;
        this.massOfCentralBody = massOfCentralBody;
                
        gravParameter = 6.67430e-11f * (mass + massOfCentralBody);
        isMoon = 0;  // default to not a moon

        this.notes = notes;
        UnityEngine.ColorUtility.TryParseHtmlString(descColor, out this.descColor);
        
        this.nthMoon = nthMoon;
        
    }

    public double DistanceFromSun()
    {
        return Math.Sqrt(pos.x*pos.x + pos.y*pos.y + pos.z*pos.z);
    }

    public void CalculateInitialPositionVelocity()
    {
        float G = 6.67430e-11f; // Gravitational constant

        // This website goes the other way, but it might work to reverse the process
        // https://phys.libretexts.org/Bookshelves/Astronomy__Cosmology/Celestial_Mechanics_(Tatum)/09%3A_The_Two_Body_Problem_in_Two_Dimensions/9.08%3A_Orbital_Elements_and_Velocity_Vector
        // a later chapter gives the formulas for the position and velocity vectors in cartesian coordinates

        // convert to meters
        float a = semiMajorAxis * 1.496e11f; // times by 1.496e11 to convert from AU to meters
        float e = eccentricity;

        // Convert all the angles to radians
        float i = Mathf.Deg2Rad * inclination;
        float Omega = Mathf.Deg2Rad * longditudeOfAscendingNode;
        float w = Mathf.Deg2Rad * argOfPerihelion;
        float nu = Mathf.Deg2Rad * trueAnomaly;

        float mu = G * (mass + massOfCentralBody); // Gravitational parameter

        float distance = a * (1 - e * e) / (1 + e * Mathf.Cos(nu));
        float speed = Mathf.Sqrt(mu * a * (1 - e * e)) / distance;
        
        // position vector in cartesian coordinates
        pos.x = distance * ( Mathf.Cos(Omega) * Mathf.Cos(w + nu) - Mathf.Sin(Omega) * Mathf.Sin(w + nu) * Mathf.Cos(i) );
        pos.z = distance * ( Mathf.Sin(Omega) * Mathf.Cos(w + nu) + Mathf.Cos(Omega) * Mathf.Sin(w + nu) * Mathf.Cos(i) );
        pos.y = distance * ( Mathf.Sin(i) * Mathf.Sin(w + nu) );

        // velocity vector in cartesian coordinates
        vel.x = speed * ( Mathf.Cos(Omega) * Mathf.Sin(w+nu) + Mathf.Sin(Omega) * Mathf.Cos(w+nu) * Mathf.Cos(i) );
        vel.z = speed * ( Mathf.Sin(Omega) * Mathf.Sin(w+nu) - Mathf.Cos(Omega) * Mathf.Cos(w+nu) * Mathf.Cos(i) );
        vel.y = speed * ( Mathf.Cos(w+nu) * Mathf.Sin(i) );
    }


    void Start()
    {
        //find solarsystemmanager.cs and attach it.
        solarSystemManager = GameObject.Find("Solar System Manager");
        
        //Find the UI Elements in the scene and attach it.
        List<GameObject> UIElements = GameObject.Find("CloseOpenVRMenu").GetComponent<CloseVrMenu>().GetUI();
        closevrmenuScript = GameObject.Find("CloseOpenVRMenu").GetComponent<CloseVrMenu>();
        OptionsMenu = UIElements[1];
        UXPanel = UIElements[0];
        InfoBar = UXPanel.transform.GetChild(0).gameObject;


        dopplerGraph = InfoBar.transform.Find("EmissionSpectra").GetComponent<DopplerGraph>();

        solarScript = solarSystemManager.GetComponent<SolarSystemManager>();
        scaleDist = solarScript.scaleDist > 0 ? solarScript.scaleDist : 1;
        scaleSize = solarScript.scaleSize > 0 ? solarScript.scaleSize : 1;


        foreach(CelestialBody body in solarScript.celestialBodiesList)
        {
            if (body.isMoon == this.id && this.id != 0) 
            {
                this.moonsList.Add(body);
            }
        }
        

        transform = GetComponent<Transform>();
        transform.localScale = new Vector3(2*radiusEarth*(float)scaleSize, 2*radiusEarth*(float)scaleSize, 2*radiusEarth*(float)scaleSize);
    }

    private void FixedUpdate()
    {
        //If the panel is current on and theres a planet being shown
        if (UXPanel.activeSelf == true && VisualBody != null)
        {

            //Set the scale of the visual body and rotate it based on its orbital period
            float final_scale = 0.1f/(float)scaleSize;
            VisualBody.transform.localScale = new Vector3(final_scale, final_scale,final_scale);

            VisualBody.transform.Find(bodyName).GetComponent<Transform>().Rotate(new Vector3(0f, orbitalPeriod / 75, 0f));

            // --------------------------------
            // Doppler

            V_observer = solarScript.GetPlayerVelocity();
            double V_rel = Math.Sqrt(Math.Pow(vel.x - V_observer.x, 2) + Math.Pow(vel.y - V_observer.y,2) + Math.Pow(vel.z - V_observer.z,2));

            if (dopplerGraph.isActiveAndEnabled )
            {
                List<double> wavelengths_new = new List<double>();
                foreach (double wavelength in wavelengths)
                {
                    double wavelength_new = wavelength * Math.Sqrt((1 - (V_rel) / (c)) / (1 + (V_rel) / (c)));
                    wavelengths_new.Add(wavelength_new);
                }

                dopplerGraph.UpdateSpectra(wavelengths_new, wavelengths);
            }
            

            // ---------------------------------
            
        }

    }
    public void ShowInfoBox()
    {
        //THIS FUNCTION RUNS TO ACTIVATE ON THE INFORMATION TAB

        //Turn off the Options menu if its on
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
        }

        //turn on info tab
        UXPanel.SetActive(true);

        //if the newly selected planet is the same as the currently shown one
        if (this.bodyName == InfoBar.transform.Find("NameIn").GetComponent<TMP_Text>().text)
        {
            return;
        }
        closevrmenuScript.hasClickedOnce = true;

        //turn on the emission spectra graph
        InfoBar.transform.Find("EmissionSpectra").gameObject.SetActive(true);


        //Updates the left hand to show a copy of the selected planet, and a canvas containing useful information
        //
        //Clear any children of UXPanel apart from the canvas, so that new planets are not overlaid on top of each other
        if (UXPanel.transform.childCount != 1)
        {
            for (int i = 1; i < UXPanel.transform.childCount; i++)
            {
                Destroy(UXPanel.transform.GetChild(i).gameObject);
            }
            VisualBody = null;
        }

        //Destroy all currently showing buttons that select the moons, if there were any to begin with 
        Transform moonButtons = InfoBar.transform.Find("MoonButtons");
        if (moonButtons.childCount != 0)
        {
            for (int i = 0; i < moonButtons.childCount; i++)
            {
                Destroy(moonButtons.GetChild(i).gameObject);
            }

        }

        


        //Set descriptive information to the InfoMenu canvas
        InfoBar.transform.Find("NameIn").GetComponent<TMP_Text>().text = bodyName;

        string total_info_string = "Mass: " + mass + "kg / " + massEarth + " Earths\n" +
            "Radius: " + radius + "km / " + radiusEarth + " Earths\n" +
            "Period: " + orbitalPeriod + " days\n" +
            "Surface Temperature: " + surfaceTemp + " K";

        InfoBar.transform.Find("ValueIn").GetComponent<TMP_Text>().text = total_info_string;

        InfoBar.transform.Find("DescriptionIn").GetComponent<TMP_Text>().text = notes;

        //Reset what page the long text should be on
        InfoBar.transform.Find("DescriptionIn").GetComponent<TMP_Text>().pageToDisplay = 1;

        //Load the button prefab
        UnityEngine.Object MoonButtonPrefab = Resources.Load("UIElements/MoonButtonPrefab");

        //Incrementally add a button for each moon
        float y = 0;
        foreach (CelestialBody moon in moonsList)
        {
 
            GameObject MoonButton = Instantiate(MoonButtonPrefab, moonButtons) as GameObject;

            MoonButton.GetComponentInChildren<TMP_Text>().text = moon.bodyName;

            //position it further down than the last button
            MoonButton.transform.localPosition = new Vector3(0f, (60 - (y * 40f)), 0f);

            //attach the 'onClick' script, which takes it to this function again but for the selected moon
            MoonButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate() { moon.ShowInfoBox(); });

            MoonButton.GetComponent<UnityEngine.UI.Image>().color = moon.descColor;
            y++;
            
        }

        //Change the colour of the panel to match the selected planet
        InfoBar.transform.Find("OuterPanel").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("DescriptionPanel").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("ValuePanel").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("MoonPanel").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("ButtonClose").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("ButtonBack").GetComponent<UnityEngine.UI.Image>().color = descColor;
        InfoBar.transform.Find("ButtonForward").GetComponent<UnityEngine.UI.Image>().color = descColor;

        

        Transform PanelTransform = GameObject.Find("UXPanel").GetComponent<Transform>();

        //Create a clone of the selected planet
        VisualBody = Instantiate(VisualBodyPrefab, PanelTransform);
        Transform iconTransform = VisualBody.transform.Find("icon");

        //Turn the icons on so that they can be destroyed, as Destroy does not work on inactive gameObjects
        iconTransform.gameObject.SetActive(true);

        //Delete the icons and stds, so that they cannot be shown in the selected planet
        Destroy(VisualBody.GetNamedChild("icon").gameObject);
        try
        {
            Destroy(VisualBody.GetNamedChild("SpaceTimePlane(Clone)").gameObject);
        }
        catch (Exception)
        {
        }
            

        //set scale of body
        float final_scale = 0.1f / (float)scaleSize;

        VisualBody.transform.localScale = new Vector3(final_scale, final_scale, final_scale);

        //load a cylinder to represent the axis of rotation
        GameObject Cylinder = Instantiate(Resources.Load("UIElements/Cylinder"),VisualBody.transform) as GameObject;

        //turn off the trails
        VisualBody.GetComponent<TrailRenderer>().enabled = false;

        //Set position to be just above the hand, and rotate planet so its N/S pole align with the hand.
        VisualBody.transform.localPosition = new Vector3(0.2f, 0f, 0f);
        
        VisualBody.transform.Rotate(new Vector3(0, 0, -axialTilt));

        //if the body has no available emission spectra data, dont show it
        if(wavelengths.Count == 0)
        {
            InfoBar.transform.Find("EmissionSpectra").gameObject.SetActive(false);
        }
    }
}