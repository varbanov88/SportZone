namespace SportZone.Common
{
    public class GlobalConstants
    {
        public const int ArticleListingPageSize = 5;
        public const int NewsPageSize = 12;
        public const int CommentPageSize = 20;

        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int ImageMaxSize = 50 * 1024;
        public const int ImageMinSize = 5 * 1024;

        public const int VideoUrlLength = 11;

        public const int ContentMinLength = 50;
        public const int ContentMaxLength = 2000;

        public const int CommentMinLength = 5;
        public const int CommentMaxLength = 200;

        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 100;

        public const string CommentTextLengthErrorText = "Comment must be between 5 and 200 symbols!";

        public const string VideoUrlPrefix = "https://www.youtube.com/embed/";
    }
}
