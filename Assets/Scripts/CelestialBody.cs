using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    // Properties of the celestial body
    public string bodyName;        // Name of the celestial body
    public string mass;             // Mass of the celestial body in kg
    public string massEarth;        // Mass of the celestial body in Earth masses
    public string radius;           // Mean radius of the celestial body in km
    public string radiusEarth;      // Mean radius of the celestial body in Earth radii
    public string apoapsis;         // Apoapsis of the celestial body's orbit in AU
    public string periapsis;        // Periapsis of the celestial body's orbit in AU
    public string argOfPeriapsis;   // Argument of periapsis in degrees
    public string semiMajorAxis;    // Semi-major axis of the celestial body's orbit in AU
    public string inclination;      // Inclination of the celestial body's orbit in degrees
    public string orbitalPeriod;    // Sidereal orbital period in days
    public string rotationalPeriod; // Sidereal rotational period in days
    public string axialTilt;        // Axial tilt of the celestial body in degrees
    public string absoluteMagnitude; // Absolute magnitude
    public string averageDensity;   // Average density of the celestial body in g/cm^3
    public string surfaceTemp;      // Surface temperature of the celestial body in K
    public string surfaceGravity;   // Surface gravity of the celestial body in g
    public string hasMoons;          // Does the celestial body have moons?

    // Set properties based on data from the CSV file
    public void SetPropertiesFromData(string mass, string massEarth, string radius, string radiusEarth,
                                      string apoapsis, string periapsis, string argOfPeriapsis,
                                      string semiMajorAxis, string inclination, string orbitalPeriod,
                                      string rotationalPeriod, string axialTilt, string absoluteMagnitude,
                                      string averageDensity, string surfaceTemp, string surfaceGravity,
                                      string hasMoons)
    {
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
    }

    // Add methods here to simulate the physics
    
}