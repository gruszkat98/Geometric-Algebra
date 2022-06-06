using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliffordAlgebra
{
    public class Multivector
    {
        private Blade[] Blades;

        private static Blade[] Concat(Blade[] blades1, Blade[] blades2)
        {
            var rez = new Blade[blades1.Length + blades2.Length];
            for (var i = 0; i < blades1.Length; i++)
                rez[i] = blades1[i];
            for (var i = 0; i < blades2.Length; i++)
                rez[blades1.Length + i] = blades2[i];
            return rez;
        }

        public static string[] Span(Blade[] blades)
        {
            var s = new string[blades.Length];
            for (var i = 0; i < blades.Length; i++)
            {
                s[i] = blades[i].GetVectors();
            }
            var set = new HashSet<string>(s);
            var result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }
        
        public Multivector()
        {
            Blades = new[] {new Blade()};
        }

        public Multivector(double scalar)
        {
            Blades = new[] {new Blade(scalar)};
        }

        public Multivector(double scalar, string vectors)
        {
            Blades = new[] {new Blade(scalar, vectors)};
        }

        public Multivector(Blade blade)
        {
            Blades = new[] {new Blade(blade)};
        }

        
        public Multivector(Blade[] blades)
        {
            var s = Span(blades);
            var tempList = new List<Blade>();
            foreach (var dimension in s)
            {
                var rez = 0.0;
                foreach (var t in blades)
                    if (t.GetVectors() == dimension)
                        rez += t.GetScalar();
                if(rez != 0) tempList.Add(new Blade(rez, dimension));
            }
            Blades = tempList.ToArray();
            Array.Sort(Blades);
        }

        public Multivector(Multivector other)
        {
            Blades = new Blade[other.Blades.Length];
            for (var i = 0; i < Blades.Length; i++)
            {
                Blades[i] = new Blade(other.Blades[i]);
            }
        }

        public static Multivector operator +(Multivector m1, Multivector m2)
        {
            var rezArr = Concat(m1.Blades, m2.Blades);
            return new Multivector(rezArr);
        }

        public static Multivector operator -(Multivector m1, Multivector m2)
        {
            return m1 + -1*m2;
        }

        public static Multivector operator *(Multivector m, double r)
        {
            var rezArr = new Blade[m.Blades.Length];
            for (var i = 0; i < m.Blades.Length; i++)
            {
                rezArr[i] = m.Blades[i] * r;
            }
            return new Multivector(rezArr);
        }

        public static Multivector operator *(double r, Multivector m)
        {
            return m * r;
        }

        public static Multivector operator *(Multivector m, Blade blade)
        {
            var rezArr = new Blade[m.Blades.Length];
            for (var i = 0; i < rezArr.Length; i++)
                rezArr[i] = m.Blades[i] * blade;
            return new Multivector(rezArr);
        }
        
        public static Multivector operator *(Blade blade, Multivector m)
        {
            var rezArr = new Blade[m.Blades.Length];
            for (var i = 0; i < rezArr.Length; i++)
                rezArr[i] =blade* m.Blades[i];
            return new Multivector(rezArr);
        }

        public static Multivector operator *(Multivector m1, Multivector m2)
        {
            var sums = new Multivector[m2.Blades.Length];
            for (var i = 0; i < sums.Length; i++)
                sums[i] = m1 * m2.Blades[i];
            var rez = new Multivector();
            foreach (var sum in sums)
                rez += sum;
            return rez;
        }

        public static bool operator ==(Multivector m1, Multivector m2)
        {
            if (m1.Blades.Length != m2.Blades.Length) return false;
            for (var i = 0; i < m1.Blades.Length; i++)
                if (m1.Blades[i] != m2.Blades[i])
                    return false;
            return true;
        }

        public static bool operator !=(Multivector m1, Multivector m2)
        {
            return !(m1 == m2);
        }

        public override string ToString()
        {
            
            var stringBuilder = new StringBuilder("DIMENSION: "+ Blades.Length+ " ");
            for (var i = 0; i < Blades.Length; i++)
            {
                stringBuilder.Append(Blades[i]);
                stringBuilder.Append(" ");
            }

            return stringBuilder.ToString();
        }
        protected bool Equals(Multivector other)
        {
            return Equals(Blades, other.Blades);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Multivector) obj);
        }

        public override int GetHashCode()
        {
            return Blades != null ? Blades.GetHashCode() : 0;
        }
    }
}