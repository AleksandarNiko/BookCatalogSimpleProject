namespace BookCatalog.Common
{
    public static class EntityValidationConstants
    {
        public class Book
        {
            public const int TitleMaxLength = 100;
            public const int DescriptionMaxLength = 500;
            public const int ISBNMaxLength = 13;
            public const int LanguageMaxLength = 50;
        }

        public class Author
        {
            public const int NameMaxLength = 100;
            public const int BiographyMaxLength = 500;
        }

        public class Genre
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 200;
        }

    }
}
