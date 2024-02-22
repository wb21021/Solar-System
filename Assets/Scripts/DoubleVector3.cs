using System;

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

        // default vectors
        public static readonly doubleVector3 zero = new doubleVector3(0, 0, 0);
        public static readonly doubleVector3 one = new doubleVector3(1, 1, 1);

        // Returns the squared length of this vector 
        public double sqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }

        // Return the magnitude of the vector
        public double magnitude
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z);
            }
        }

        // Return the normalized vector
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

        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return x;
                }
                else if (index == 1)
                {
                    return y;
                }
                else if (index == 2)
                {
                    return z;
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid doubleVector3 index!");
                }
            }
            set
            {
                if (index == 0)
                {
                    x = value;
                }
                else if (index == 1)
                {
                    y = value;
                }
                else if (index == 2)
                {
                    z = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Invalid doubleVector3 index!");
                }
            }
        }

        // equals operator
        public override bool Equals(object obj)
        {
            if(obj is doubleVector3)
            {
                doubleVector3 other = (doubleVector3)obj;
                return x == other.x && y == other.y && z == other.z;
            }
            return false;
        }
        public override int GetHashCode()
        {
            // Creates a 'random' hash number using prime numbers
            int hash =  17;
            hash = hash * 31 + x.GetHashCode();
            hash = hash * 31 + y.GetHashCode();
            hash = hash * 31 + z.GetHashCode();
            return hash;
        }
        public static bool operator ==(doubleVector3 a, doubleVector3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(doubleVector3 a, doubleVector3 b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        // Set Vector
        public void Set(double new_x, double new_y, double new_z)
        {
            x = new_x;
            y = new_y;
            z = new_z;
        }

        // To string
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }
        // To Vector3 (unity)
        public UnityEngine.Vector3 ToVector3()
        {
            return new UnityEngine.Vector3((float)x, (float)y, (float)z);
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
        public double Dot(doubleVector3 b)
        {
            return    x * b.x 
                    + y * b.y 
                    + z * b.z;
        }
        public static double Dot(doubleVector3 a, doubleVector3 b)
        {
            return    a.x * b.x 
                    + a.y * b.y 
                    + a.z * b.z;
        }

        // cross product
        public doubleVector3 Cross(doubleVector3 b)
        {
            return new doubleVector3(   y * b.z - z * b.y, 
                                        z * b.x - x * b.z, 
                                        x * b.y - y * b.x);
        }
        public static doubleVector3 Cross(doubleVector3 a, doubleVector3 b)
        {
            return new doubleVector3(   a.y * b.z - a.z * b.y, 
                                        a.z * b.x - a.x * b.z, 
                                        a.x * b.y - a.y * b.x);
        }
        
        // Scale two vectors 
        public static doubleVector3 Scale(doubleVector3 a, doubleVector3 b)
        {
            // Every component in the result is a component of a multiplied by the same component of b.
            return new doubleVector3(   a.x * b.x, 
                                        a.y * b.y, 
                                        a.z * b.z);
        }

        // Distance between two vectors
        public double Distance(doubleVector3 b) // between this vector and another
        {
            return (this - b).magnitude;
        }
        public static double Distance(doubleVector3 a, doubleVector3 b) // between two vectors
        {
            return (a - b).magnitude;
        }

        // multiply by a scalar (both ways)
        public static doubleVector3 operator *(doubleVector3 a, double b) // vector * scalar
        {
            return new doubleVector3(   a.x * b, 
                                        a.y * b, 
                                        a.z * b);
        }
        public static doubleVector3 operator *(double b, doubleVector3 a) // scalar * vector
        {
            return new doubleVector3(   a.x * b, 
                                        a.y * b, 
                                        a.z * b);
        }
        // for float
        public static doubleVector3 operator *(doubleVector3 a, float b) // vector * scalar
        {
            double b_double = b;
            return new doubleVector3(   a.x * b_double, 
                                        a.y * b_double, 
                                        a.z * b_double);
        }
        public static doubleVector3 operator *(float b, doubleVector3 a) // scalar * vector
        {
            double b_double = b;
            return new doubleVector3(   a.x * b_double, 
                                        a.y * b_double, 
                                        a.z * b_double);
        }

        // divide by a scalar (both ways)
        public static doubleVector3 operator /(doubleVector3 a, double b) // vector / scalar
        {
            return new doubleVector3(   a.x / b, 
                                        a.y / b, 
                                        a.z / b);
        }
        public static doubleVector3 operator /(double b, doubleVector3 a) // scalar / vector
        {
            return new doubleVector3(   a.x / b, 
                                        a.y / b, 
                                        a.z / b);
        }
        // for float
        public static doubleVector3 operator /(doubleVector3 a, float b) // vector / scalar
        {
            double b_double = b;
            return new doubleVector3(   a.x / b_double, 
                                        a.y / b_double, 
                                        a.z / b_double);
        }
        public static doubleVector3 operator /(float b, doubleVector3 a) // scalar / vector
        {
            double b_double = b;
            return new doubleVector3(   a.x / b_double, 
                                        a.y / b_double, 
                                        a.z / b_double);
        }
    }
}