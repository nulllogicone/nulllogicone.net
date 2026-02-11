public static class OliUtils
{
            // MakeImageSrc()
        public static string MakeImageSrc(string datei)
        {
            var src = "";

            if (!string.IsNullOrEmpty(datei))
            {
                if (datei.StartsWith("http"))
                {
                    src = datei;
                }
                else
                {
                    if (!datei.StartsWith("/"))
                    {
                        datei = "/" + datei;
                    }
                    datei = datei.Replace("//", "/");
                    var bilderOrdner = "https://oliit.blob.core.windows.net/oliupload";
                    src = bilderOrdner + datei;
                }
            }
            return src;
        }

}