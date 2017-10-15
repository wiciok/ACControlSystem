using System;

namespace ACCSApi.Services.Utils
{
    internal static class UniqueStringGenerator
    {
        public static string GenerateUniqueToken()
        {
            var g = Guid.NewGuid();
            var guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            guidString = guidString.Replace("/", "");
            return guidString;
        }
    }
}
