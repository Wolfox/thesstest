#region Assembly System.ComponentModel.DataAnnotations.dll, v3.5.0.0
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\v3.5\System.ComponentModel.DataAnnotations.dll
#endregion

using System;

namespace System.ComponentModel.DataAnnotations
{
    // Summary:
    //     Base class for all validation attributes.
    //
    // Exceptions:
    //   System.ComponentModel.DataAnnotations.ValidationException:
    //     If System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType
    //     and System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceName
    //     are set at the same time as System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessage.
    public abstract class ValidationAttribute : Attribute
    {
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.ValidationAttribute
        //     class.
        protected ValidationAttribute();
        //
        // Summary:
        //     Initializes a new instance of the System.ComponentModel.DataAnnotations.ValidationAttribute
        //     class using the function that enables access to validation resources.
        //
        // Parameters:
        //   errorMessageAccessor:
        //     The function that enables access of validation resources.
        //
        // Exceptions:
        //   ArgumentNullException:
        //     errorMessageAccessor is null.
        protected ValidationAttribute(Func<string> errorMessageAccessor);
        //
        // Summary:
        //     Initializes a new instance of System.ComponentModel.DataAnnotations.ValidationAttribute
        //     class using the error message to associate with a validation control.
        //
        // Parameters:
        //   errorMessage:
        //     The error message to associate with a validation control if a validation
        //     fails.
        protected ValidationAttribute(string errorMessage);

        // Summary:
        //     Gets or sets an error message to associate with a validation control if a
        //     validation fails.
        //
        // Returns:
        //     The error message associated with the validation control.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     If a System.ComponentModel.DataAnnotations.ValidationAttribute is already
        //     in resource mode or if you try to reset this property multiple times.
        //
        //   System.ArgumentException:
        //     If the value of the error message is an empty string or null.
        public string ErrorMessage { get; set; }
        //
        // Summary:
        //     Gets or sets the error message resource name to use as lookup for the System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType
        //     if a validation fails.
        //
        // Returns:
        //     The error message resource associated with a validation control.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     If the System.ComponentModel.DataAnnotations.ValidationAttribute is already
        //     in explicit mode or if you set the property multiple times.
        //
        //   System.ArgumentException:
        //     If the value of error message is null.
        public string ErrorMessageResourceName { get; set; }
        //
        // Summary:
        //     Gets or sets the resource type to use for error message lookup if a validation
        //     fails.
        //
        // Returns:
        //     The type of error message associated with a validation control.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     If you try to set an error message or System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType
        //     multiple times.
        //
        //   System.ArgumentException:
        //     If the System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType
        //     is null or an empty string.
        public Type ErrorMessageResourceType { get; set; }
        //
        // Summary:
        //     Gets the error message to associate with a validation control when a validation
        //     fails.
        //
        // Returns:
        //     The error message to display when a validation fails.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The resource accessors used to retrieve an error message fails. Requires
        //     both System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceType
        //     and System.ComponentModel.DataAnnotations.ValidationAttribute.ErrorMessageResourceName
        protected string ErrorMessageString { get; }

        // Summary:
        //     Applies formatting to an error message based on the data field where the
        //     error occurred.
        //
        // Parameters:
        //   name:
        //     The name of the data field where the error occurred.
        //
        // Returns:
        //     An instance of the formatted error message.
        public virtual string FormatErrorMessage(string name);
        //
        // Summary:
        //     Determines whether the specified value of the object is valid.
        //
        // Parameters:
        //   value:
        //     The value of the specified validation object on which the System.ComponentModel.DataAnnotations.ValidationAttribute
        //     is declared.
        //
        // Returns:
        //     true if the specified value is valid; otherwise, false.
        public abstract bool IsValid(object value);
        //
        // Summary:
        //     Validates an object or property on which the System.ComponentModel.DataAnnotations.ValidationAttribute
        //     is declared.
        //
        // Parameters:
        //   value:
        //     The value of the object on which a System.ComponentModel.DataAnnotations.ValidationAttribute
        //     is declared.
        //
        //   name:
        //     The name of the object or data field on which a System.ComponentModel.DataAnnotations.ValidationAttribute
        //     is declared.
        //
        // Exceptions:
        //   System.ComponentModel.DataAnnotations.ValidationException:
        //     Value is not valid.
        public void Validate(object value, string name);
    }
}
