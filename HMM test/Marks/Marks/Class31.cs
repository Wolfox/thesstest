#region Assembly AForge.dll, v2.2.5.0
// C:\Users\AntonioPinto\Desktop\Accord.NET-2.14.0-archive\Externals\AForge.NET\AForge.dll
#endregion

using System;

namespace AForge
{
    // Summary:
    //     Represents an integer range with minimum and maximum values.
    //
    // Remarks:
    //     The class represents an integer range with inclusive limits - both minimum
    //     and maximum values of the range are included into it.  Mathematical notation
    //     of such range is [min, max].
    //     Sample usage:
    //     // create [1, 10] range IntRange range1 = new IntRange( 1, 10 ); // create
    //     [5, 15] range IntRange range2 = new IntRange( 5, 15 ); // check if values
    //     is inside of the first range if ( range1.IsInside( 7 ) ) { // ...  } // check
    //     if the second range is inside of the first range if ( range1.IsInside( range2
    //     ) ) { // ...  } // check if two ranges overlap if ( range1.IsOverlapping(
    //     range2 ) ) { // ...  }
    [Serializable]
    public struct IntRange
    {
        //
        // Summary:
        //     Initializes a new instance of the AForge.IntRange structure.
        //
        // Parameters:
        //   min:
        //     Minimum value of the range.
        //
        //   max:
        //     Maximum value of the range.
        public IntRange(int min, int max);

        // Summary:
        //     Inequality operator - checks if two ranges have different min/max values.
        //
        // Parameters:
        //   range1:
        //     First range to check.
        //
        //   range2:
        //     Second range to check.
        //
        // Returns:
        //     Returns true if min/max values of specified ranges are not equal.
        public static bool operator !=(IntRange range1, IntRange range2);
        //
        // Summary:
        //     Equality operator - checks if two ranges have equal min/max values.
        //
        // Parameters:
        //   range1:
        //     First range to check.
        //
        //   range2:
        //     Second range to check.
        //
        // Returns:
        //     Returns true if min/max values of specified ranges are equal.
        public static bool operator ==(IntRange range1, IntRange range2);
        //
        // Summary:
        //     Implicit conversion to AForge.Range.
        //
        // Parameters:
        //   range:
        //     Integer range to convert to single precision range.
        //
        // Returns:
        //     Returns new single precision range which min/max values are implicitly converted
        //     to floats from min/max values of the specified integer range.
        public static implicit operator Range(IntRange range);

        // Summary:
        //     Length of the range (deffirence between maximum and minimum values).
        public int Length { get; }
        //
        // Summary:
        //     Maximum value of the range.
        //
        // Remarks:
        //     The property represents maximum value (right side limit) or the range - [min,
        //     max].
        public int Max { get; set; }
        //
        // Summary:
        //     Minimum value of the range.
        //
        // Remarks:
        //     The property represents minimum value (left side limit) or the range - [min,
        //     max].
        public int Min { get; set; }

        // Summary:
        //     Check if this instance of AForge.Range equal to the specified one.
        //
        // Parameters:
        //   obj:
        //     Another range to check equalty to.
        //
        // Returns:
        //     Return true if objects are equal.
        public override bool Equals(object obj);
        //
        // Summary:
        //     Get hash code for this instance.
        //
        // Returns:
        //     Returns the hash code for this instance.
        public override int GetHashCode();
        //
        // Summary:
        //     Check if the specified value is inside of the range.
        //
        // Parameters:
        //   x:
        //     Value to check.
        //
        // Returns:
        //     True if the specified value is inside of the range or false otherwise.
        public bool IsInside(int x);
        //
        // Summary:
        //     Check if the specified range is inside of the range.
        //
        // Parameters:
        //   range:
        //     Range to check.
        //
        // Returns:
        //     True if the specified range is inside of the range or false otherwise.
        public bool IsInside(IntRange range);
        //
        // Summary:
        //     Check if the specified range overlaps with the range.
        //
        // Parameters:
        //   range:
        //     Range to check for overlapping.
        //
        // Returns:
        //     True if the specified range overlaps with the range or false otherwise.
        public bool IsOverlapping(IntRange range);
        //
        // Summary:
        //     Get string representation of the class.
        //
        // Returns:
        //     Returns string, which contains min/max values of the range in readable form.
        public override string ToString();
    }
}
