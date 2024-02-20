using UnityEngine;
using System;


public class longVector3
{
    public long x;
    public long y;
    public long z;

    public longVector3(long x, long y, long z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public longVector3(Vector3 vector)
    {
        this.x = (long)vector.x;
        this.y = (long)vector.y;
        this.z = (long)vector.z;
    }

    // function to return the magnitude of the vector
    public long magnitude()
    {
        return (long)Math.Sqrt(x * x + y * y + z * z);
    }

    // function to return the normalized vector
    public longVector3 normalized()
    {
        long mag = magnitude();
        return new longVector3( x / mag, 
                                y / mag, 
                                z / mag);
    }

    public static longVector3 operator +(longVector3 a, longVector3 b)
    {
        return new longVector3( a.x + b.x, 
                                a.y + b.y, 
                                a.z + b.z);
    }

    public static longVector3 operator -(longVector3 a, longVector3 b)
    {
        return new longVector3( a.x - b.x, 
                                a.y - b.y, 
                                a.z - b.z);
    }

    public static longVector3 operator *(longVector3 a, long b)
    {
        return new longVector3( a.x * b, 
                                a.y * b, 
                                a.z * b);
    }

    public static longVector3 operator /(longVector3 a, long b)
    {
        return new longVector3( a.x / b, 
                                a.y / b, 
                                a.z / b);
    }

    public static longVector3 operator *(long a, longVector3 b)
    {
        return new longVector3( a * b.x, 
                                a * b.y, 
                                a * b.z);
    }

    public static longVector3 operator /(long a, longVector3 b)
    {
        return new longVector3( a / b.x,
                                a / b.y, 
                                a / b.z);
    }
}