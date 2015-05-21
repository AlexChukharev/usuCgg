using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CGG
{
    public class Poly
    {
        public Verb End;
        public Verb Start;
        public Object Color;
        public List<Edge> _edges;
        public List<Edge> Edges 
        {
            get 
            {
                var result = _edges.ToList();
                if (Start != null)
                    result.Add(new Edge(Start, End));
                return result;
            }
        }

        public Poly(object input)
        {
            _edges = new List<Edge>();
            Color = input;
        }

        public void AddVerb(double x, double y)
        {
            var current = new Verb(x, y);
            if (Start == null)
            {
                Start = current;
                End = current;
            }
            else
            {
                _edges.Add(new Edge(End, current));
                End = current;
            }

        }

        public static Poly Intersection(Poly a, Poly b)
        {
            var result = new Poly(Brushes.DarkRed);
            foreach (var current in a.Edges)
                result._edges.AddRange(current.Partion(b));
            foreach (var current in b.Edges)
                result._edges.AddRange(current.Partion(a));
            return result;
        }
    }

    public class Edge
    {
        public Verb A;
        public Verb B;

        public Edge(Verb a, Verb b)
        {
            if (a.X < b.X)
            {
                A = a; B = b;
            }
            else
            {
                A = b; B = a;
            }
        }

        public List<Edge> Partion(Poly input)
        {
            var result = new List<Edge>();
            var flag = true;

            foreach (var edge in input.Edges)                   //перебор отсекающих ребер
            {
                var verb = Intersection(edge);
                if (verb != null)
                {
                    result.AddRange(new Edge(A, verb).Partion(input));
                    result.AddRange(new Edge(B, verb).Partion(input));
                    flag = false;
                    break;
                }
            }
//            if (flag)
//            {
////                if (!A.IsInternal(input) || !B.IsInternal(input))
//                    if (!(new Verb((A.X + B.X) / 2, (A.Y + B.Y) / 2).IsInternal(input)))
//                        return result;
//                result.Add(this);
//            } 
            if (flag)         //если отрезок более не делим, то проверяем лежит ли он внутри многоуголника а
            {
                if ((new Verb((A.X + B.X) / 2, (A.Y + B.Y) / 2).IsInternal(input)))
                    result.Add(this); 
            }
            return result;
        }


        public const double Tolerance = 0.00001;
        public Verb Intersection(Edge input)
        {
            var a1 = A.Y - B.Y; 
            var b1 = B.X - A.X;
            var c1 = A.X * B.Y - B.X * A.Y;
            var a2 = input.A.Y - input.B.Y;
            var b2 = input.B.X - input.A.X;
            var c2 = input.A.X * input.B.Y - input.B.X * input.A.Y;
            if ((Math.Abs(a1) < Tolerance && Math.Abs(b1) < Tolerance) || (Math.Abs(a2) < Tolerance && Math.Abs(b2) < Tolerance) || (Math.Abs(a1 - a2) < Tolerance && Math.Abs(b1 - b2) < Tolerance))
                return null;
            var x = (b1 * c2 - b2 * c1) / (b2 * a1 - a2 * b1);
            var y = (c1 + a1 * x) / - b1;
            if (x > Math.Max(input.A.X, A.X) && x < Math.Min(input.B.X, B.X))
                return new Verb(x, y);
            return null;
        }
    }

    public class Verb 
    {
        public double X;
        public double Y;

        public Verb(double x, double y)
        {
            X = x; Y = y;
        }

        public bool IsInternal(Poly a)                      //проверка является ли точка внутренней
        {
            var count = 0;
            var edge = new Edge(new Verb(X, Y), new Verb(550, Y+10));

            foreach (var current in a.Edges)
            {
                if (Belongs(current))           //принадлежит ребру
                {
                    return true;
                }
                var verb = edge.Intersection(current);          //пересекает лучом из искомой точки все ребра многоугольника
                if (verb != null)
                {
                    count++;
                }
            }
            return count % 2 == 1;                              //если нечетное - внутри многоуголника
        }

        public bool Belongs(Edge input)
        {
            if (X < input.A.X || X > input.B.X)
                return false;
            return Math.Abs((X - input.B.X)/(input.A.X - input.B.X) - (Y - input.B.Y)/(input.A.Y - input.B.Y)) < 0.1;
                //< 0.000000001;
        }
    }
}
