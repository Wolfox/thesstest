#region Assembly AForge.dll, v2.2.5.0
// C:\Users\AntonioPinto\Desktop\Accord.NET-2.14.0-archive\Externals\AForge.NET\AForge.dll
#endregion

using System;

namespace AForge
{
    // Summary:
    //     Represents a double range with minimum and maximum values.
    //
    // Remarks:
    //     The class represents a double range with inclusive limits - both minimum
    //     and maximum values of the range are included into it.  Mathematical notation
    //     of such range is [min, max].
    //     Sample usage:
    //     // create [0.25, 1.5] range DoubleRange range1 = new DoubleRange( 0.25, 1.5
    //     ); // create [1.00, 2.25] range DoubleRange range2 = new DoubleRange( 1.00,
    //     2.25 ); // check if values is inside of the first range if ( range1.IsInside(
    //     0.75 ) ) { // ...  } // check if the second range is inside of the first
    //     range if ( range1.IsInside( range2 ) ) { // ...  } // check if two ranges
    //     overlap if ( range1.IsOverlapping( range2 ) ) { // ...  }
    [Serializable]
    public struct DoubleRange
    {
        //
        // Summary:
        //     Initializes a new instance of the AForge.DoubleRange class.
        //
        // Parameters:
        //   min:
        //     Minimum value of the range.
        //
        //   max:
        //     Maximum value of the range.
        public DoubleRange(double min, double max);

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
        public static bool operator !=(DoubleRange range1, DoubleRange range2);
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
        public static bool operator ==(DoubleRange range1, DoubleRange range2);

        // Summary:
        //     Length of the range (deffirence between maximum and minimum values).
        public double Length { get; }
        //
        // Summary:
        //     Maximum value of the range.
        //
        // Remarks:
        //     The property represents maximum value (right side limit) or the range - [min,
        //     max].
        public double Max { get; set; }
        //
        // Summary:
        //     Minimum value of the range.
        //
        // Remarks:
        //     The property represents minimum value (left side limit) or the range - [min,
        //     max].
        public double Min { get; set; }

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
        public bool IsInside(double x);
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
        public bool IsInside(DoubleRange range);
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
        public bool IsOverlapping(DoubleRange range);
        //
        // Summary:
        //     Convert the signle precision range to integer range.
        //
        // Parameters:
        //   provideInnerRange:
        //     Specifies if inner integer range must be returned or outer range.
        //
        // Returns:
        //     Returns integer version of the range.
        //
        // Remarks:
        //     If provideInnerRange is set to true, then the returned integer range will
        //     always fit inside of the current single precision range.  If it is set to
        //     false, then current single precision range will always fit into the returned
        //     integer range.
        public IntRange ToIntRange(bool provideInnerRange);
        //
        // Summary:
        //     Get string representation of the class.
        //
        // Returns:
        //     Returns string, which contains min/max values of the range in readable form.
        public override string ToString();
    }
}
