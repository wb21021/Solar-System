using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using doubleVector3namespace;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UIElements;

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
    public doubleVector3 pos;            // Position vector
    public doubleVector3 posDouble; // Position vector (using double precision)
    public doubleVector3 vel;            // Velocity vector
    public doubleVector3 velDouble;      // Velocity vector (using double precision)
    public doubleVector3 acc;            // Acceleration vector
    public doubleVector3 accDouble;      // Acceleration vector (using double precision)


    private Transform transform; // for position

    private GameObject solarSystemManager;
    private GameObject InfoBar;

    private SolarSystemManager solarScript;
    private double scaleDist;
    private double scaleSize;
    //private Rigidbody rigidbody; // for velocity

    // Set properties based on data from the CSV file
    public void SetPropertiesFromData(int id, 
                                      float mass, float massEarth, float radius, float radiusEarth,
                                      float apoapsis, float periapsis, float argOfPeriapsis,
                                      float semiMajorAxis, float inclination, float orbitalPeriod,
                                      float rotationalPeriod, float axialTilt, float absoluteMagnitude,
                                      float averageDensity, float surfaceTemp, float surfaceGravity,
                                      int hasMoons, float ringRadius, float ringDepth,
                                      float eccentricity, float longditudeOfAscendingNode, 
                                      float trueAnomaly, float massOfCentralBody, string notes, string descColor)
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
        pos.y = distance * ( Mathf.Sin(Omega) * Mathf.Cos(w + nu) + Mathf.Cos(Omega) * Mathf.Sin(w + nu) * Mathf.Cos(i) );
        pos.z = distance * ( Mathf.Sin(i) * Mathf.Sin(w + nu) );
        //Debug.Log(pos + "THIS IS INITIALPOS");

        // velocity vector in cartesian coordinates
        vel.x = speed * ( Mathf.Cos(Omega) * Mathf.Sin(w+nu) + Mathf.Sin(Omega) * Mathf.Cos(w+nu) * Mathf.Cos(i) );
        vel.y = speed * ( Mathf.Sin(Omega) * Mathf.Sin(w+nu) - Mathf.Cos(Omega) * Mathf.Cos(w+nu) * Mathf.Cos(i) );
        vel.z = speed * ( Mathf.Cos(w+nu) * Mathf.Sin(i) );
    }

    void Start()
    {
        solarSystemManager = GameObject.Find("Solar System Manager");

        InfoBar = GameObject.Find("InfoMenu");

        solarScript = solarSystemManager.GetComponent<SolarSystemManager>();
        scaleDist = solarScript.scaleDist > 0 ? solarScript.scaleDist : 1;
        scaleSize = solarScript.scaleSize > 0 ? solarScript.scaleSize : 1;

        transform = GetComponent<Transform>();
        transform.localScale = new Vector3(radius*(float)scaleSize, radius*(float)scaleSize, radius*(float)scaleSize);
    }


    public void ShowInfoBox()
    {

        
        InfoBar.SetActive(true);


        InfoBar.transform.Find("NameIn").GetComponent<TMP_Text>().text = bodyName;

        string total_info_string = "Mass: " + mass + "kg / " + massEarth + " Earths\n" +
            "Radius: " + radius + "km / " + radiusEarth + " Earths\n" +
            "Period: " + orbitalPeriod + " days\n" +
            "Surface Temperature: " + surfaceTemp + " K";

        InfoBar.transform.Find("ValueIn").GetComponent<TMP_Text>().text = total_info_string;

        InfoBar.transform.Find("Scroll View/Viewport/DescriptionIn").GetComponent<TMP_Text>().text = notes;

        InfoBar.transform.Find("OuterPanel").GetComponent<UnityEngine.UI.Image>().color = descColor;

        InfoBar.transform.Find("DescriptionPanel").GetComponent<UnityEngine.UI.Image>().color = descColor;


    }
}