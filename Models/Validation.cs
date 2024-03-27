namespace CoronaManagementSystem.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.Text.RegularExpressions;

	public class PersonIdAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				// If the value is null, it's considered valid as it's optional.
				return ValidationResult.Success;
			}

			// Convert the value to a string and trim any leading or trailing whitespace
			string? personId = value.ToString()?.Trim();

			// Check if the person ID is empty after trimming whitespace
			if (string.IsNullOrEmpty(personId))
			{
				// If the person ID is empty, it's considered invalid.
				return new ValidationResult("תעודת זהות לא יכולה להיות ריקה");
			}

			// Perform additional validation checks here if needed.
			// For example, you might check the length, format, or uniqueness of the person ID.

			// Example: Validate that the person ID consists only of digits
			if (!System.Text.RegularExpressions.Regex.IsMatch(personId, @"^\d+$"))
			{
				return new ValidationResult("תעודת זהות חיבת להכיל ספרות בלבד");
			}

			// Example: Validate that the person ID has a specific length
			if (personId.Length != 9)
			{
				return new ValidationResult("תעודת זהות חיבת להיות 9 ספרות");
			}

			// Add more validation logic as needed...

			// If the person ID passes all validation checks, it's considered valid.
			return ValidationResult.Success;
		}
	}

	public class IsraeliPhoneAttribute : ValidationAttribute
	{
		private const string IsraeliPhoneRegex = @"^(?:\+972|0)(?:\s?-?\d{1,2}\s?-?\d{3}\s?-?\d{4})|(?:\d{7,8})$";

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success; ; // Allow null values
			}

			string phone = value.ToString();
			if (Regex.IsMatch(phone, IsraeliPhoneRegex))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("לא טלפון ישראלי חוקי");
		}

		public override string FormatErrorMessage(string name)
		{
			return string.Format(ErrorMessage ?? "The {0} number is not a valid Israeli phone number.", name);
		}
	}

	public class IsraeliMobilePhoneAttribute : ValidationAttribute
	{
		private const string IsraeliMobileRegex = @"^(?:\+972|0)5(?:\d{7,8})$";
		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
			{
				// If the value is null, it's considered valid as it's optional.
				return ValidationResult.Success;
			}

			// Convert the value to a string and trim any leading or trailing whitespace
			string phone = value.ToString();
			if (Regex.IsMatch(phone, IsraeliMobileRegex))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("לא טלפון נייד ישראלי חוקי");
		}
	}
	public class AfterPositiveResultAttribute : ValidationAttribute
	{
		private readonly string _comparisonProperty;

		public AfterPositiveResultAttribute(string comparisonProperty)
		{
			_comparisonProperty = comparisonProperty;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

			if (comparisonProperty == null)
			{
				throw new ArgumentException("Property with this name not found");
			}

			var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);
			var currentValue = (DateTime?)value;

			if (currentValue.HasValue && comparisonValue.HasValue && currentValue < comparisonValue)
			{
				return new ValidationResult($" חייב להיות קודם {_comparisonProperty} ");
			}

			return ValidationResult.Success;
		}
	}
}
