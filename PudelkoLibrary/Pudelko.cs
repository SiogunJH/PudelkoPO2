using System;
using PudelkoLibrary.Enums;


namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable
    {
        //============================================================================

        public readonly int a;
        public double A
        {
            get => MilimeterToAny(a, Unit);
        }
        public readonly int b;
        public double B
        {
            get => MilimeterToAny(b, Unit);
        }
        public readonly int c;
        public double C
        {
            get => MilimeterToAny(c, Unit);
        }
        public UnitOfMeasure Unit { get; set; }

        //============================================================================
        //                                   METHODS
        //============================================================================

        public double Objetosc
        {
            get
            {
                return Math.Round(A * B * C, 9);
            }
        }

        public double Pole
        {
            get
            {
                return Math.Round(2 * ((A * B) + (B * C) + (C * A)), 6);
            }
        }

        //============================================================================
        //                                     TOOLS
        //============================================================================

        public double MilimeterToAny(int size, UnitOfMeasure unit) =>
            (double)size / UnitToNumber(unit);

        public int AnyToMilimeter(double size, UnitOfMeasure unit) =>
            (int)(size * (double)UnitToNumber(unit));

        public int UnitToNumber(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return 1000;
                case UnitOfMeasure.centimeter:
                    return 10;
                default:
                    return 1;
            }
        }

        public UnitOfMeasure StringToUnit(string unit)
        {
            switch (unit)
            {
                case "m":
                    return UnitOfMeasure.meter;
                case "cm":
                    return UnitOfMeasure.centimeter;
                case "mm":
                    return UnitOfMeasure.milimeter;
                default:
                    throw new FormatException();
            }
        }

        public string ToString(string format)
        {
            string toReturn = "";
            switch (format)
            {
                case "m":
                    toReturn = String.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", format, MilimeterToAny(a, StringToUnit(format)), MilimeterToAny(b, StringToUnit(format)), MilimeterToAny(c, StringToUnit(format)));
                    break;
                case "cm":
                    toReturn = String.Format("{1:0.0} {0} × {2:0.0} {0} × {3:0.0} {0}", format, MilimeterToAny(a, StringToUnit(format)), MilimeterToAny(b, StringToUnit(format)), MilimeterToAny(c, StringToUnit(format)));
                    break;
                case "mm":
                    toReturn = String.Format("{1:0} {0} × {2:0} {0} × {3:0} {0}", format, MilimeterToAny(a, StringToUnit(format)), MilimeterToAny(b, StringToUnit(format)), MilimeterToAny(c, StringToUnit(format)));
                    break;
            }
            return toReturn;
        }

        public string ToString(string format, IFormatProvider formatProvider) =>
            String.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", "m", MilimeterToAny(a, UnitOfMeasure.meter), MilimeterToAny(b, UnitOfMeasure.meter), MilimeterToAny(c, UnitOfMeasure.meter));

        //============================================================================
        //                                  CONSTRUCTOR
        //============================================================================
        public Pudelko(double a = 0.1f, double b = 0.1f, double c = 0.1f, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            // Set Unit
            Unit = unit;

            // Check if within borders
            if (a < 0 || b < 0 || c < 0 || AnyToMilimeter(a, Unit) > 10000 || AnyToMilimeter(b, Unit) > 10000 || AnyToMilimeter(c, Unit) > 10000)
                throw new ArgumentOutOfRangeException();

            // Set Size
            this.a = AnyToMilimeter(a, Unit);
            this.b = AnyToMilimeter(b, Unit);
            this.c = AnyToMilimeter(c, Unit);
        }

        //============================================================================

    }
}