#region Assembly AForge.Math.dll, v2.2.5.0
// C:\Users\AntonioPinto\Desktop\Accord.NET-2.14.0-archive\Externals\AForge.NET\AForge.Math.dll
#endregion

using System;

namespace AForge.Math.Random
{
    // Summary:
    //     Interface for random numbers generators.
    //
    // Remarks:
    //     The interface defines set of methods and properties, which should be implemented
    //     by different algorithms for random numbers generatation.
    public interface IRandomNumberGenerator
    {
        // Summary:
        //     Mean value of generator.
        float Mean { get; }
        //
        // Summary:
        //     Variance value of generator.
        float Variance { get; }

        // Summary:
        //     Generate next random number.
        //
        // Returns:
        //     Returns next random number.
        float Next();
        //
        // Summary:
        //     Set seed of the random numbers generator.
        //
        // Parameters:
        //   seed:
        //     Seed value.
        void SetSeed(int seed);
    }
}
