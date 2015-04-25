#region Assembly AForge.Math.dll, v2.2.5.0
// C:\Users\AntonioPinto\Desktop\Accord.NET-2.14.0-archive\Externals\AForge.NET\AForge.Math.dll
#endregion

using System;

namespace AForge.Math.Random
{
    // Summary:
    //     Gaussian random numbers generator.
    //
    // Remarks:
    //     The random number generator generates gaussian random numbers with specified
    //     mean and standard deviation values.
    //     The generator uses AForge.Math.Random.StandardGenerator generator as base
    //     to generate random numbers.
    //     Sample usage:
    //     // create instance of random generator IRandomNumberGenerator generator =
    //     new GaussianGenerator( 5.0, 1.5 ); // generate random number float randomNumber
    //     = generator.Next( );
    public class GaussianGenerator : IRandomNumberGenerator
    {
        // Summary:
        //     Initializes a new instance of the AForge.Math.Random.GaussianGenerator class.
        //
        // Parameters:
        //   mean:
        //     Mean value.
        //
        //   stdDev:
        //     Standard deviation value.
        public GaussianGenerator(float mean, float stdDev);
        //
        // Summary:
        //     Initializes a new instance of the AForge.Math.Random.GaussianGenerator class.
        //
        // Parameters:
        //   mean:
        //     Mean value.
        //
        //   stdDev:
        //     Standard deviation value.
        //
        //   seed:
        //     Seed value to initialize random numbers generator.
        public GaussianGenerator(float mean, float stdDev, int seed);

        // Summary:
        //     Mean value of the generator.
        public float Mean { get; }
        //
        // Summary:
        //     Standard deviation value.
        public float StdDev { get; }
        //
        // Summary:
        //     Variance value of the generator.
        public float Variance { get; }

        // Summary:
        //     Generate next random number.
        //
        // Returns:
        //     Returns next random number.
        public float Next();
        //
        // Summary:
        //     Set seed of the random numbers generator.
        //
        // Parameters:
        //   seed:
        //     Seed value.
        //
        // Remarks:
        //     Resets random numbers generator initializing it with specified seed value.
        public void SetSeed(int seed);
    }
}
