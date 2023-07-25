namespace SportsNet.Common
{
    public class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 100;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Post
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 80;

            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 200;
        }

        public static class Comment
        {
            public const int MinLength = 2;
            public const int MaxLength = 100;
        }

        public static class Image
        {
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 30;

            public const int UrlMaxLength = 2048;
        }
    }
}
