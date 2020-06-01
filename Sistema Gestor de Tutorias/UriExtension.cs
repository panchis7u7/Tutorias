using System;

namespace Sistema_Gestor_de_Tutorias
{
    public static class UriExtension
    {
        public static bool IsWebUri(this Uri uri)
        {
            if (uri != null)
            {
                var str = uri.ToString().ToLower();
                return str.StartsWith("http://") || str.StartsWith("https://");
            }
            return false;
        }
    }
}
