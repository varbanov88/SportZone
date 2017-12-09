using System;
using System.Collections.Generic;
using System.Text;

namespace SportZone.Data
{
    public class DataConstants
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;

        public const int ImageMaxSize = 50 * 1024;
        public const int ImageMinSize = 5 * 1024;

        public const int VideoUrlLength = 11;

        public const int ContentMinLength = 50;
        public const int ContentMaxLength = 2000;

        public const int CommentMinLength = 5;
        public const int CommentMaxLength = 200;

        public const int TitleMinLength = 15;
        public const int TitleMaxLength = 200;
    }
}
