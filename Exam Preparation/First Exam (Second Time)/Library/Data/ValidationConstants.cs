namespace Library.Data
{
	public static class ValidationConstants
	{
		public class BookValidationConstants
		{
			public const int TitleMaxLength = 50;
			public const int TitleMinLength = 10;

			public const int AuthorMaxLength = 50;
			public const int AuthorMinLength = 5;

			public const int DescriptionMaxLength = 5000;
			public const int DescriptionMinLength = 5;

			public const double RatingMaxValue = 10.00;
			public const double RatingMinValue = 0.00;
		}

		public class CategoryValidationConstants
		{
			public const int NameMaxLength = 50;
			public const int NameMinLength = 5;
		}
	}
}
