using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }


        public static class Factory
        {
            public static Point CreateNewPolar(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }

            public static Point CreateNewCartesian(double x, double y)
            {
                return new Point(x, y);
            }
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            var point = Point.Factory.CreateNewCartesian(1, 5);
            var point2 = Point.Factory.CreateNewPolar(1,5);

            Console.WriteLine(point);
            Console.WriteLine(point2);
        }
    }
}
