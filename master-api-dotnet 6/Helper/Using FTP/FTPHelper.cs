using System.Net;

namespace master_api_dotnet_6.Helper.Using_FTP
{
    public class FTPHelper
    {
        private readonly string ftpServer;
        private readonly string ftpUsername;
        private readonly string ftpPassword;

        public FTPHelper(string server, string username, string password)
        {
            ftpServer = "ftp://192.168.2.000/";
            ftpUsername = "id";
            ftpPassword = "pass";
        }

        public void UploadImage(Stream imageStream, string remoteFileName)
        {
            try
            {
                // Create the FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://192.168.2.225/MANAGERIUM/HR_Files/{remoteFileName}");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Copy the contents of the image to the request stream
                using (Stream requestStream = request.GetRequestStream())
                {
                    imageStream.CopyTo(requestStream);
                }

                // Get the response
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload complete. Status: {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during FTP upload: {ex.Message}");
                throw;
            }
        }

        public Stream DownloadImage(string remoteFileName)
        {
            try
            {
                // Create the FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://192.168.2.225/MANAGERIUM/HR_Files/{remoteFileName}");
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

                // Get the response
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                // Get the response stream and return it
                return response.GetResponseStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during FTP download: {ex.Message}");
                throw;
            }
        }

    }
}
