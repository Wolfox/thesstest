#region Assembly System.ComponentModel.DataAnnotations.dll, v3.5.0.0
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5\System.ComponentModel.DataAnnotations.dll
#endregion

using System;

namespace System.ComponentModel.DataAnnotations
{
    // Summary:
    //     Specifies the numeric range constraints for the value of a data field in
    //     Dynamic Data.
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeAttribute : ValidationAttribute
    {
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute
        //     class by using the specified minimum and maximum values.
        //
        // Parameters:
        //   minimum:
        //     Specifies the minimum value allowed for the data field value.
        //
        //   maximum:
        //     Specifies the maximum value allowed for the data field value.
        public RangeAttribute(double minimum, double maximum);
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute
        //     class by using the specified minimum and maximum values.
        //
        // Parameters:
        //   minimum:
        //     Specifies the minimum value allowed for the data field value.
        //
        //   maximum:
        //     Specifies the maximum value allowed for the data field value.
        public RangeAttribute(int minimum, int maximum);
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.RangeAttribute
        //     class by using the specified minimum and maximum values and the specific
        //     type.
        //
        // Parameters:
        //   type:
        //     Specifies the type of the object to test.
        //
        //   minimum:
        //     Specifies the minimum value allowed for the data field value.
        //
        //   maximum:
        //     Specifies the maximum value allowed for the data field value.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     type is null.
        public RangeAttribute(Type type, string minimum, string maximum);

        // Summary:
        //     Gets the maximum allowed field value.
        //
        // Returns:
        //     The maximum value that is allowed for the data field.
        public object Maximum { get; }
        //
        // Summary:
        //     Gets the minimum allowed field value.
        public object Minimum { get; }
        //
        // Summary:
        //     Gets the type of the data field whose value must be validated.
        //
        // Returns:
        //     The type of the data field whose value must be validated.
        public Type OperandType { get; }

        // Summary:
        //     Formats the error message that is displayed when range validation fails.
        //
        // Parameters:
        //   name:
        //     The name of the field that caused the validation failure.
        //
        // Returns:
        //     The formatted error message.
        public override string FormatErrorMessage(string name);
        //
        // Summary:
        //     Checks that the value of the data field is in the specified range.
        //
        // Parameters:
        //   value:
        //     The data field value to validate.
        //
        // Returns:
        //     true if the specified value is in the range; otherwise, false.
        //
        // Exceptions:
        //   System.ComponentModel.DataAnnotations.ValidationException:
        //     The data field value was outside the allowed range.
        public override bool IsValid(object value);
    }
}
