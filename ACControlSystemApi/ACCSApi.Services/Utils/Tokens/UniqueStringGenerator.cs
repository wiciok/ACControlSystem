using System;

namespace ACControlSystemApi.Utils.Tokens
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
