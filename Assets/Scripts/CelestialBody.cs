using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    public Vector3 pos;            // Position vector
    public Vector3 vel;            // Velocity vector
    public Vector3 acc;            // Acceleration vector


    private Transform transform; // for position

    private GameObject solarSystemManager;
    private GameObject InfoBar;

    private SolarSystemManager solarScript;
    private float scaleDist;
    private float scaleSize;
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
                                      float trueAnomaly, float massOfCentralBody)
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
    }

    public float DistanceFromSun()
    {
        return Mathf.Sqrt(pos.x*pos.x + pos.y*pos.y + pos.z*pos.z);
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
        transform.localScale = new Vector3(radius*scaleSize, radius*scaleSize, radius*scaleSize);
    }

    void FixedUpdate()
    {
        scaleDist = solarScript.scaleDist > 0 ? solarScript.scaleDist : 1;
        scaleSize = solarScript.scaleSize > 0 ? solarScript.scaleSize : 1;
        transform.position = pos*scaleDist;
    }

    public void ShowInfoBox()
    {
        Debug.Log("RUNNIGN");

        
        InfoBar.SetActive(true);

        string total_string = "Name: " + bodyName + "\nMass: " + mass + "kg";
        InfoBar.GetComponentInChildren<TMP_Text>().text = total_string;
    }
}