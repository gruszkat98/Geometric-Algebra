using System;
using System.Text;

namespace CliffordAlgebra
{
    public class Blade: IComparable
    {
        private double scalar;
        private string vectors;
        public static string ImaginaryUnits = "";
        

        private void SortVectors()
        {
            var vectors2 = new StringBuilder(vectors);
            var flippityFloppity = 1;
            for (var i = 0; i < vectors2.Length; i++)
            {
                for (var j = 0; j < vectors2.Length-1-i; j++)
                {
                    if (vectors2[j] > vectors2[j + 1])
                    {
                        var temp = vectors2[j];
                        vectors2[j] = vectors2[j + 1];
                        vectors2[j + 1] = temp;
                        flippityFloppity *= -1;
                    }
                    
                }
            }

            int curr = 0;
            while (curr < vectors2.Length - 1)
            {
                if (vectors2[curr] == vectors2[curr + 1])
                {
                    if (ImaginaryUnits.Contains(vectors2[curr]))
                        flippityFloppity *= -1;
                    vectors2 = vectors2.Remove(curr, 2);
                }
                else
                    curr++;
                
            }

            vectors = vectors2.ToString();
            scalar *= flippityFloppity;
        }

        public Blade()
        {
            scalar = 0;
            vectors = "";
        }

        public Blade(double scalar)
        {
            this.scalar = scalar;
            vectors = "";
        }

        public Blade(double scalar, string vectors)
        {
            this.scalar = scalar;
            this.vectors = vectors;
            SortVectors();
        }

        public Blade(Blade other)
        {
            scalar = other.scalar;
            vectors = other.vectors;
        }

        public string GetVectors()
        {
            return vectors;
        }

        public double GetScalar()
        {
            return scalar;
        }
        public static bool operator == (Blade b1, Blade b2)
        {
            return b1.scalar == b2.scalar && b1.vectors == b2.vectors;
        }

        public static bool operator !=(Blade b1, Blade b2)
        {
            return !(b1 == b2);
        }

        public static bool operator <=(Blade b1, Blade b2)
        {
            if (b1.vectors.Length < b2.vectors.Length)
                return true;
            if (b1.vectors.Length > b2.vectors.Length)
                return false;

            for (var i = 0; i < b1.vectors.Length; i++)
            {
                if (b1.vectors[i] > b2.vectors[i])
                    return false;
            }

            return true;
        }

        public static bool operator <(Blade b1, Blade b2)
        {
            return b1 <= b2 && b1 != b2;
        }

        public static bool operator >(Blade b1, Blade b2)
        {
            return b2 <= b1 && b1 != b2;
        }

        public static bool operator >=(Blade b1, Blade b2)
        {
            return b2 < b1 && b1 != b2;
        }

        public static Blade operator *(Blade b1, Blade b2)
        {
            return new Blade(b1.scalar * b2.scalar, b1.vectors + b2.vectors);
        }

        public static Blade operator *(Blade blade, double r)
        {
            return new Blade(blade.scalar*r, blade.vectors);
        }
        
        public static Blade operator *(double r, Blade blade)
        {
            return blade * r;
        }

        public static Multivector operator +(Blade b1, Blade b2)
        {
            if (b1.vectors == b2.vectors)
                return new Multivector(b1.scalar + b2.scalar, b1.vectors);
            return new Multivector(new []{b1, b2});
        }

        
        public static Multivector operator -(Blade b1, Blade b2)
        {
            if (b1.vectors == b2.vectors)
                return new Multivector(b1.scalar - b2.scalar, b1.vectors);
            var b3 = new Blade(b2);
            b3.scalar *= -1;
            return new Multivector(new []{b1, b3});
        }

        public override string ToString()
        {
            return scalar + vectors;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 1;
            if (obj.GetType() != GetType()) return 1;
            return this < (Blade)obj? -1: 1;
        }

        protected bool Equals(Blade other)
        {
            return scalar.Equals(other.scalar) && string.Equals(vectors, other.vectors);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Blade) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (scalar.GetHashCode() * 397) ^ (vectors != null ? vectors.GetHashCode() : 0);
            }
        }
    }
}