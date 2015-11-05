using System;
using System.Configuration;
using System.IO;
using DropNet;

namespace PhotoContest.Web.Helpers
{
    public class Dropbox
    {
        private static DropNetClient client;

        static Dropbox()
        {
            client = new DropNetClient("ve5tzn6oprq756p", "nv0tyxgnm3gvoia", "cdkbD3NysrsAAAAAAAAEcewzrvd0WIFwG4wyXQ2IDqBbtSv2Y08zfCQuhny8MmTj");
        }

        public static string Upload(string fileName, string authorName, Stream fileStream)
        {
            string fullFileName = authorName + "_" + DateTime.Now.ToString("o") + "_" + fileName;
            client.UploadFile("/", fullFileName, fileStream);
            return fullFileName;
        }

        public static string Download(string path)
        {
            return client.GetMedia(path).Url;
        }
    }
}