using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

namespace doubleVector3namespace
{
    public struct doubleVector3
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

        // zero vector
        public static readonly doubleVector3 zero = new doubleVector3(0, 0, 0);

        // return the magnitude of the vector
        public double magnitude
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z);
            }
        }

        // return the normalized vector
        public doubleVector3 normalized
        {
            get
            {
                double mag = magnitude;
                return new doubleVector3(   x / mag, 
                                            y / mag, 
                                            z / mag     );
            }
        }



        // add to vectors
        public static doubleVector3 operator +(doubleVector3 a, doubleVector3 b)
        {
            return new doubleVector3(   a.x + b.x, 
                                        a.y + b.y, 
                                        a.z + b.z);
        }

        // subtract two vectors
        public static doubleVector3 operator -(doubleVector3 a, doubleVector3 b)
        {
            return new doubleVector3(   a.x - b.x, 
                                        a.y - b.y, 
                                        a.z - b.z);
        }

        // dot product
        public double dot(doubleVector3 b)
        {
            return    x * b.x 
                    + y * b.y 
                    + z * b.z;
        }

        // cross product
        public doubleVector3 cross(doubleVector3 b)
        {
            return new doubleVector3(   y * b.z - z * b.y, 
                                        z * b.x - x * b.z, 
                                        x * b.y - y * b.x);
        }

        // multiply by a scalar (both ways)
        public static doubleVector3 operator *(doubleVector3 a, double b)
        {
            return new doubleVector3(   a.x * b, 
                                        a.y * b, 
                                        a.z * b);
        }
        public static doubleVector3 operator *(double b, doubleVector3 a)
        {
            return new doubleVector3(   a.x * b, 
                                        a.y * b, 
                                        a.z * b);
        }
        // for float
        public static doubleVector3 operator *(doubleVector3 a, float b)
        {
            double b_double = b;
            return new doubleVector3(   a.x * b_double, 
                                        a.y * b_double, 
                                        a.z * b_double);
        }
        public static doubleVector3 operator *(float b, doubleVector3 a)
        {
            double b_double = b;
            return new doubleVector3(   a.x * b_double, 
                                        a.y * b_double, 
                                        a.z * b_double);
        }

        // divide by a scalar (both ways)
        public static doubleVector3 operator /(doubleVector3 a, double b)
        {
            return new doubleVector3(   a.x / b, 
                                        a.y / b, 
                                        a.z / b);
        }
        public static doubleVector3 operator /(double b, doubleVector3 a)
        {
            return new doubleVector3(   a.x / b, 
                                        a.y / b, 
                                        a.z / b);
        }
        // for float
        public static doubleVector3 operator /(doubleVector3 a, float b)
        {
            double b_double = b;
            return new doubleVector3(   a.x / b_double, 
                                        a.y / b_double, 
                                        a.z / b_double);
        }
        public static doubleVector3 operator /(float b, doubleVector3 a)
        {
            double b_double = b;
            return new doubleVector3(   a.x / b_double, 
                                        a.y / b_double, 
                                        a.z / b_double);
        }
    }
}