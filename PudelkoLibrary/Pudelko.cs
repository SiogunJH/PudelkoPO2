using System;
using PudelkoLibrary.Enums;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable
    {
        // --==## [CLASS VARIABLES] ##==--
        private readonly int a;
        public double A
        {
            get => SizeToSize(a, UnitOfMeasure.milimeter, Unit);
        }
        private readonly int b;
        public double B
        {
            get => SizeToSize(b, UnitOfMeasure.milimeter, Unit);
        }
        private readonly int c;
        public double C
        {
            get => SizeToSize(c, UnitOfMeasure.milimeter, Unit);
        }
        private UnitOfMeasure Unit;

        // --==## [METHODS] ##==--

        //Internal
        private double SizeToSize(double size, UnitOfMeasure unit, UnitOfMeasure desiredUnit) => size * (double)UnitToNumber(unit) / (double)UnitToNumber(desiredUnit);

        private string UnitToString(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return "m";
                case UnitOfMeasure.centimeter:
                    return "cm";
                default:
                    return "mm";
            }
        }

        private int UnitToNumber(UnitOfMeasure unit)
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

        private UnitOfMeasure StringToUnit(string unit)
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
                    throw new ArgumentOutOfRangeException();
            }
        }

        //String related
        public string ToString(string arg1, IFormatProvider arg2)
            => String.Format("{1} {0} × {2} {0} × {3} {0}", UnitToString(Unit), A, B, C);

        public string ToString(string desiredUnit)
            => String.Format("{1} {0} × {2} {0} × {3} {0}", UnitToString(Unit), SizeToSize(a, UnitOfMeasure.milimeter, StringToUnit(desiredUnit)), SizeToSize(b, UnitOfMeasure.milimeter, StringToUnit(desiredUnit)), SizeToSize(c, UnitOfMeasure.milimeter, StringToUnit(desiredUnit)));


        // --==## [CONSTRUCTORS] ##==--
        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter)
        {
        }

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.a = (int)SizeToSize(a, unit, UnitOfMeasure.milimeter);
            this.b = (int)SizeToSize(b, unit, UnitOfMeasure.milimeter);
            this.c = (int)SizeToSize(c, unit, UnitOfMeasure.milimeter);
            this.Unit = unit;

            if (A <= 0 || B <= 0 || B <= 0) //Negative values
                throw new ArgumentOutOfRangeException();

            if (a > 10000 || b > 10000 || c > 10000) //Too big values
                throw new ArgumentOutOfRangeException();
        }
    }
}