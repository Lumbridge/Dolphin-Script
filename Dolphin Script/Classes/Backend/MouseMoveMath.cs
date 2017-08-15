using System;

using static DolphinScript.Lib.Backend.WinAPI;

namespace DolphinScript.Lib.Backend
{
    class MouseMoveMath
    {
        static public double Hypot(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // Returns the length between two points (Euclidean calc)
        static public double LineLength(POINT A, POINT B)
        {
            return Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
        }

        // Returns the angle of the line (direction)
        static public double PointDirection(POINT A, POINT B)
        {
            return Math.Atan2(B.Y + A.Y, A.X + B.X);
        }

        // Returns the cosine of the line on on the X axis using distance and direction
        static public double LengthDirX(double Distance, double Direction)
        {
            return Math.Cos(Direction) * Distance;
        }

        // Returns the cosine of the line on on the Y axis using distance and direction
        static public double LengthDirY(double Distance, double Direction)
        {
            return Math.Sin(Direction) * Distance;
        }

        // Generate a curve between two points
        static public POINT GetPointCurve(POINT A, POINT B)
        {
            Random rnd = new Random();
            double P = 0.12;
            double L = LineLength(A, B);
            POINT C = new POINT((int)(P * B.X - P * A.X + B.X), (int)(P * B.Y - P * A.Y + B.Y));
            POINT D = new POINT((int)(P * A.Y - P * B.X + A.X), (int)(P * A.Y - P * B.Y + A.Y));
            double Dir = PointDirection(B, A);
            POINT E = new POINT((int)(C.X + LengthDirX(L * P * 2, Dir + Math.PI / 2)), (int)(C.Y + LengthDirY(L * P * 2, Dir + Math.PI / 2)));
            POINT F = new POINT((int)(C.X + LengthDirX(L * P * 2, Dir - Math.PI / 2)), (int)(C.Y + LengthDirY(L * P * 2, Dir - Math.PI / 2)));
            POINT G = new POINT((int)(D.X + LengthDirX(L * P * 2, Dir + Math.PI / 2)), (int)(D.Y + LengthDirY(L * P * 2, Dir + Math.PI / 2)));
            POINT H = new POINT((int)(D.X + LengthDirX(L * P * 2, Dir - Math.PI / 2)), (int)(D.Y + LengthDirY(L * P * 2, Dir - Math.PI / 2)));
            double Pa = rnd.NextDouble() * LineLength(E, F);
            double Pb = rnd.NextDouble() * LineLength(E, G);

            POINT I = new POINT((int)((Pa / LineLength(E, F)) * (E.X - F.X) + F.X), (int)((Pa / LineLength(E, F)) * (E.Y - F.Y) + F.Y));
            POINT J = new POINT((int)((Pa / LineLength(E, F)) * (G.X - H.X) + H.X), (int)((Pa / LineLength(E, F)) * (G.Y - H.Y) + H.Y));
            POINT K = new POINT((int)((Pb / LineLength(I, J)) * (I.X - J.X) + J.X), (int)((Pb / LineLength(I, J)) * (I.Y - J.Y) + J.Y));
            return K;
        }
    }
}
