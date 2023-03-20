using System;
using PudelkoLibrary.Enums;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable
    {
        // --==## [CLASS VARIABLES] ##==--
        public readonly int a;
        public double A
        {
            get => SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
        }
        private readonly int b;
        public double B
        {
            get => SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
        }
        private readonly int c;
        public double C
        {
            get => SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter);
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
                    throw new FormatException();
            }
        }

        //String related
        
        public string ToString(string desiredUnit)
        {
            if (desiredUnit == null) desiredUnit = "m";

            switch (desiredUnit)
            {
                case "m":
                    return string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.meter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.meter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.meter));
                case "cm":
                    return string.Format("{1:0.0} {0} × {2:0.0} {0} × {3:0.0} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter));
                case "mm":
                    return string.Format("{1:0} {0} × {2:0} {0} × {3:0} {0}", desiredUnit, SizeToSize(a, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter), SizeToSize(b, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter), SizeToSize(c, UnitOfMeasure.milimeter, UnitOfMeasure.milimeter));
                default:
                    throw new FormatException();
            }
        }

        public override string ToString()
    => string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", UnitToString(UnitOfMeasure.meter), A, B, C);

        public string ToString(string format, IFormatProvider formatProvider)
            => string.Format("{1:0.000} {0} × {2:0.000} {0} × {3:0.000} {0}", UnitToString(UnitOfMeasure.meter), A, B, C);


        // --==## [CONSTRUCTORS] ##==--
        public Pudelko() : this(0.1, 0.1, 0.1, UnitOfMeasure.meter)
        {
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            // Set default values
            if (a == null) a = SizeToSize(0.1, UnitOfMeasure.meter, unit);
            if (b == null) b = SizeToSize(0.1, UnitOfMeasure.meter, unit);
            if (c == null) c = SizeToSize(0.1, UnitOfMeasure.meter, unit);

            // Assign values
            this.a = (int)Math.Floor(SizeToSize((double)a, unit, UnitOfMeasure.milimeter));
            this.b = (int)Math.Floor(SizeToSize((double)b, unit, UnitOfMeasure.milimeter));
            this.c = (int)Math.Floor(SizeToSize((double)c, unit, UnitOfMeasure.milimeter));
            this.Unit = unit;

            //Test for values out of range from 1mm to 10000mm
            if (this.a <= 0 || this.b <= 0 || this.c <= 0 || this.a > 10000 || this.b > 10000 || this.c > 10000)
                throw new ArgumentOutOfRangeException();
        }
    }
}