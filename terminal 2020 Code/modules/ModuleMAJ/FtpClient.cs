using sorec_gamma.IHMs.ComposantsGraphique;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace sorec_gamma.modules.ModuleMAJ
{
    class FtpClient
    {
        private string hostAddress;
        private string port;
        private string username;
        private string password;
        private MAJForm form;

        private FtpClient(string hostAddress, string username, string password, string port, MAJForm form)
        {
            this.hostAddress = hostAddress;
            this.port = port;
            this.username = username;
            this.password = password;
            this.form = form;
        }

        public static FtpClient GetInstance(string hostAddress, string username, string password, string port = "21", MAJForm form = null)
        {
            return new FtpClient(hostAddress, username, password, port, form);
        }

        public bool DownloadFile(string filename, string sourceDir, string destDir)
        {
            bool success = true;
            long downloadedSize = 0;
            long size = 0;
            try
            {
                string ftpfullpath = hostAddress + ":" + port + sourceDir + filename;
                ApplicationContext.Logger.Info(string.Format("Download file {0} ... ", ftpfullpath));

                size = GetFileSize(filename, sourceDir);
                if (size == 0)
                    return false;
                if (form != null)
                {
                    form.ResetProgressBar((int)size);
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpfullpath);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Timeout = 3 * 60 * 1000;
                request.KeepAlive = false;
                // request.UsePassive = false;
                request.Credentials = new NetworkCredential(username, password);

                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (Stream fileStream = File.Create(destDir + filename))
                {
                    ftpStream.ReadTimeout = 3 * 60 * 1000;
                    byte[] buffer = new byte[10240];
                    int read;
                    while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, read);
                        if (form != null)
                        {
                            form.UpdateProgress((int)fileStream.Position);
                        }
                    }
                }
                downloadedSize = new FileInfo(destDir + filename).Length;
                success = downloadedSize == size;
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error(string.Format("DownloadFile {0} Error: {1}", sourceDir + filename, ex.Message));
                success = false;
            }
            finally
            {
                if (!success && Directory.Exists(destDir))
                {
                    Directory.Delete(destDir, true);
                }
                ApplicationContext.Logger.Info(string.Format("Download complete {0}: {1}/{2}", destDir + filename, downloadedSize, size));
            }

            return success;
        }

        public int GetFileSizeOld(string filename, string sourceDir)
        {
            int size = 0;
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(hostAddress + sourceDir + filename);
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

                Trace.TraceInformation(string.Format("GetFileSize Complete {0} : {1}", sourceDir + filename, size));

            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("GetFileSize {0} Error: {1}", sourceDir + filename, ex.Message));
            }
            return size;
        }
        public long GetFileSize(string filename, string sourceDir)
        {
            long size = 0;
            try
            {
                string ftpfullpath = hostAddress + ":" + port + sourceDir + filename;
                ApplicationContext.Logger.Info(string.Format("Get size file {0} ... ", ftpfullpath));

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpfullpath);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Timeout = 3 * 60 * 1000;
                // request.UsePassive = false;
                request.Credentials = new NetworkCredential(username, password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                size = response.ContentLength;
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error(string.Format("GetFileSize {0} Error: {1}", sourceDir + filename, ex.Message));
            }
            return size;
        }


        public bool DownloadDirectory(string sourceDir, string destDir)
        {
            var dirName = sourceDir.Substring(sourceDir.LastIndexOf('/') + 1);

            bool success = true;
            List<string> files = GetAllFilenames(sourceDir);

            int position = 0;
            foreach (string filename in files)
            {
                position++;
                success = DownloadFile(filename, sourceDir, destDir + "/" + dirName);
                if (!success)
                {
                    Trace.TraceError(string.Format("DownloadDirectory {0}, Error downloading file: ", sourceDir, filename));
                    break;
                }
            }
            Trace.TraceInformation(string.Format("DownloadDirectory {0}, Success: {1}", sourceDir, success));

            return success;
        }

        public bool DoesDirectoryExist(string dirPath)
        {
            bool isexist = false;

            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(hostAddress + dirPath);
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    isexist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return isexist;
        }

        public List<string> GetAllFilenames(string parentFolderpath)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(hostAddress + parentFolderpath);
                ftpRequest.Credentials = new NetworkCredential(username, password);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                List<string> directories = new List<string>();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var lineArr = line.Split('/');
                    line = lineArr[lineArr.Length - 1];
                    directories.Add(line);
                    line = streamReader.ReadLine();
                }

                streamReader.Close();

                return directories;
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error(string.Format("GetAllFiles from {0}, Error: {1}", parentFolderpath, ex.Message));
                return null;
            }
        }

    }
}
