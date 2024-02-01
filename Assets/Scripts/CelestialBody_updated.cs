using System.Collections;
using System.Collections.Generic;
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
    public float semiMajorAxis;    // Semi-major axis of the celestial body's orbit in AU
    public float inclination;      // Inclination of the celestial body's orbit in degrees
    public float orbitalPeriod;    // Sidereal orbital period in days
    public float rotationalPeriod; // Sidereal rotational period in days
    public float axialTilt;        // Axial tilt of the celestial body in degrees
    public float absoluteMagnitude;// Absolute magnitude
    public float averageDensity;   // Average density of the celestial body in g/cm^3
    public float surfaceTemp;      // Surface temperature of the celestial body in K
    public float surfaceGravity;   // Surface gravity of the celestial body in g
    public int hasMoons;           // Number of moons
    public vector3 pos;            // Position vector
    public vector3 vel;            // Velocity vector

    // Set properties based on data from the CSV file
    public void SetPropertiesFromData(int id, 
                                      float mass, float massEarth, float radius, float radiusEarth,
                                      float apoapsis, float periapsis, float argOfPeriapsis,
                                      float semiMajorAxis, float inclination, float orbitalPeriod,
                                      float rotationalPeriod, float axialTilt, float absoluteMagnitude,
                                      float averageDensity, float surfaceTemp, float surfaceGravity,
                                      int hasMoons)
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
    }

    public void calculate_initial_position_velocity()
    {
        // Maybe this can be done using the imformation about the orbits from above?
    }

    // Add methods here to simulate the physics

    // This would need a reference to the other celestial bodies 
    // it might be easier to do this from the solar system manager??
    
}