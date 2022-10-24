namespace Library.Data
{
    public static class DataConstants
    {
        public class Book
        {
            public const int BookTitleMaxLength = 50;

            public const int BookTitleMinLength = 10;

            public const int BookAuthorMaxLength = 50;

            public const int BookAuthorMinLength = 5;

            public const int BookDescriptionMaxLength = 5000;

            public const int BookDescriptionMinLength = 5;
        }

        public class ApplicationUser
        {
            public const int UserUserNameMaxLength = 20;

            public const int UserUserNameMinLength = 5;

            public const int UserEmailMaxLength = 60;

            public const int UserEmailMinLength = 10;

            public const int UserPasswordMaxLength = 20;

            public const int UserPasswordMinLength = 5;
        }

        public class Category
        {
            public const int CategoryNameMaxLength = 50;

            public const int CategoryNameMinLength = 5;
        }
    }
}
