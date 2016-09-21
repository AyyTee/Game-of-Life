using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    /// <summary>
    /// Basic 2 dimensional vector class.
    /// </summary>
    public struct Vector2
    {
        public double X, Y;

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the addition of two vectors.
        /// </summary>
        public static Vector2 operator +(Vector2 vLeft, Vector2 vRight)
        {
            return new Vector2(vLeft.X + vRight.X, vLeft.Y + vRight.Y);
        }

        /// <summary>
        /// Returns the subtraction of two vectors.
        /// </summary>
        public static Vector2 operator -(Vector2 vLeft, Vector2 vRight)
        {
            return new Vector2(vLeft.X - vRight.X, vLeft.Y - vRight.Y);
        }

        /// <summary>
        /// Returns the component-wise multiplication of two vectors.
        /// </summary>
        public static Vector2 operator *(Vector2 vLeft, Vector2 vRight)
        {
            return new Vector2(vLeft.X * vRight.X, vLeft.Y * vRight.Y);
        }

        /// <summary>
        /// Returns the component-wise division of two vectors.
        /// </summary>
        public static Vector2 operator /(Vector2 vLeft, Vector2 vRight)
        {
            return new Vector2(vLeft.X / vRight.X, vLeft.Y / vRight.Y);
        }

        /// <summary>
        /// Returns a vector multiplied with a scalar value.
        /// </summary>
        public static Vector2 operator *(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Returns a vector multiplied by a scalar value.
        /// </summary>
        public static Vector2 operator *(double scalar, Vector2 vector)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Returns a vector divided by a scalar value.
        /// </summary>
        public static Vector2 operator /(Vector2 vector, double scalar)
        {
            return new Vector2(vector.X / scalar, vector.Y / scalar);
        }
    }
}
