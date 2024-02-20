using UnityEngine;
using System;

public class doubleVector3
{
    public double x;
    public double y;
    public double z;

    public doubleVector3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public doubleVector3(Vector3 vector)
    {
        this.x = (double)vector.x;
        this.y = (double)vector.y;
        this.z = (double)vector.z;
    }

    // public Vector3 ToVector3()
    // {
    //     return new Vector3(x, y, z);
    // }

    // function to return the magnitude of the vector
    public double magnitude()
    {
        return (double)Math.Sqrt(x * x + y * y + z * z);
    }

    // function to return the normalized vector
    public doubleVector3 normalized()
    {
        double mag = magnitude();
        return new doubleVector3( x / mag, 
                                y / mag, 
                                z / mag);
    }

    public static doubleVector3 operator +(doubleVector3 a, doubleVector3 b)
    {
        return new doubleVector3( a.x + b.x, 
                                a.y + b.y, 
                                a.z + b.z);
    }

    public static doubleVector3 operator -(doubleVector3 a, doubleVector3 b)
    {
        return new doubleVector3( a.x - b.x, 
                                a.y - b.y, 
                                a.z - b.z);
    }

    public static doubleVector3 operator *(doubleVector3 a, double b)
    {
        return new doubleVector3( a.x * b, 
                                a.y * b, 
                                a.z * b);
    }

    public static doubleVector3 operator /(doubleVector3 a, double b)
    {
        return new doubleVector3( a.x / b, 
                                a.y / b, 
                                a.z / b);
    }

    public static doubleVector3 operator *(double a, doubleVector3 b)
    {
        return new doubleVector3( a * b.x, 
                                a * b.y, 
                                a * b.z);
    }

    public static doubleVector3 operator /(double a, doubleVector3 b)
    {
        return new doubleVector3( a / b.x,
                                a / b.y, 
                                a / b.z);
    }
}

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

    // public Vector3 ToVector3()
    // {
    //     return new Vector3(x, y, z);
    // }

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

public class decimalVector3
{
    public decimal x;
    public decimal y;
    public decimal z;

    public decimalVector3(decimal x, decimal y, decimal z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public decimalVector3(Vector3 vector)
    {
        this.x = (decimal)vector.x;
        this.y = (decimal)vector.y;
        this.z = (decimal)vector.z;
    }

    // public Vector3 ToVector3()
    // {
    //     return new Vector3(x, y, z);
    // }

    // function to return the magnitude of the vector
    public decimal magnitude()
    {
        double temp_x = (double)x;
        double temp_y = (double)y;
        double temp_z = (double)z;
        return (decimal)Math.Sqrt(temp_x*temp_x+temp_y*temp_y+temp_z*temp_z);
    }

    // function to return the normalized vector
    public decimalVector3 normalized()
    {
        decimal mag = magnitude();
        return new decimalVector3( x / mag, 
                                y / mag, 
                                z / mag);
    }

    public static decimalVector3 operator +(decimalVector3 a, decimalVector3 b)
    {
        return new decimalVector3( a.x + b.x, 
                                a.y + b.y, 
                                a.z + b.z);
    }

    public static decimalVector3 operator -(decimalVector3 a, decimalVector3 b)
    {
        return new decimalVector3( a.x - b.x, 
                                a.y - b.y, 
                                a.z - b.z);
    }

    public static decimalVector3 operator *(decimalVector3 a, decimal b)
    {
        return new decimalVector3( a.x * b, 
                                a.y * b, 
                                a.z * b);
    }

    public static decimalVector3 operator /(decimalVector3 a, decimal b)
    {
        return new decimalVector3( a.x / b, 
                                a.y / b, 
                                a.z / b);
    }

    public static decimalVector3 operator *(decimal a, decimalVector3 b)
    {
        return new decimalVector3( a * b.x, 
                                a * b.y, 
                                a * b.z);
    }

    public static decimalVector3 operator /(decimal a, decimalVector3 b)
    {
        return new decimalVector3( a / b.x,
                                a / b.y, 
                                a / b.z);
    }
}