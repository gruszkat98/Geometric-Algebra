using System;

namespace CliffordAlgebra
{
    class Program
    {
        static void Main(string[] args)
        {
            Blade.ImaginaryUnits = "w";
            var b1 = new Blade(1, "x");
            var b2 = new Blade(2, "y");
            var b3 = new Blade(4, "z");
            var b4 = new Blade(4, "w");

            var A = new[] {b1, b2};
            var B = new[] {b3, b4};

            var m1 = new Multivector(A);
            var m2 = new Multivector(B);

            Console.WriteLine(m2 * (m2* m1));
        }
    }
}