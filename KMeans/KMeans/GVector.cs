using System;
using System.Collections.Generic;

using System.Text;

namespace KMeans
{
    public class GVector
    {
        public double r, g, b;
        public int x, y;
        public GVector(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public GVector(double r, double g, double b, int x, int y)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.x = x;
            this.y = y;
        }

        public void SetCoordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(GVector v2)
        {
            double r_power = Math.Pow((r - v2.r), 2);
            double g_power = Math.Pow((g - v2.g), 2);
            double b_power = Math.Pow((b - v2.b), 2);

            double distance = Math.Sqrt((r_power + g_power + b_power));

            return distance;
        }

        public GVector Product(int scalar)
        {
            return new GVector(r * scalar, g * scalar, b * scalar);
        }

        public double Length()
        {
            return Math.Sqrt((Math.Pow(r, 2) + Math.Pow(g, 2) + Math.Pow(b, 2)));
        }

        public GVector Sum(GVector v2)
        {
            double new_r = r + v2.r;
            double new_g = g + v2.g;
            double new_b = b + v2.b;

            return new GVector(new_r, new_g, new_b);
        }
    }
}
