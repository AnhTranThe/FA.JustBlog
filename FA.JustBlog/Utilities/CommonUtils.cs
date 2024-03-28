using FA.JustBlog.Common;
using System.Globalization;
using System.Text;

namespace FA.JustBlog.Utilities
{
    public static class CommonUtils
    {
        public static string UrlFriendly(string text, int maxLength = 0)
        { // Return empty value if text is null
            if (text == null)
            {
                return "";
            }

            string normalizedString = text
                // Make lowercase
                .ToLowerInvariant()
                // Normalize the text
                .Normalize(NormalizationForm.FormD);

            StringBuilder stringBuilder = new();
            bool prevdash = false;
            int trueLength = 0;

            char c;

            for (int i = 0; i < normalizedString.Length; i++)
            {
                c = normalizedString[i];

                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    // Check if the character is a letter or a digit if the character is a
                    // international character remap it to an ascii valid character
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        _ = c < 128 ? stringBuilder.Append(c) : stringBuilder.Append(ConstantSystem.RemapInternationalCharToAscii(c));

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        break;

                    // Check if the character is to be replaced by a hyphen but only if the last character wasn't
                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            _ = stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        break;
                }

                // If we are at max length, stop parsing
                if (maxLength > 0 && trueLength >= maxLength)
                {
                    break;
                }
            }

            // Trim excess hyphens
            string result = stringBuilder.ToString().Trim('-');

            // Remove any excess character to meet maxlength criteria
            return maxLength <= 0 || result.Length <= maxLength ? result : result[..maxLength];
        }

        public static byte[] ConvertFileToByteArray(IFormFile file)
        {
            byte[] fileBytes = new byte[0];
            if (file is not null)
            {
                using MemoryStream memoryStream = new();
                file.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
            return fileBytes;

        }


    }
}
