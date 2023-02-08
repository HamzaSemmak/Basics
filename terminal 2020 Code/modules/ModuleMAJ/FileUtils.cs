using CS_CLIB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace sorec_gamma.modules.ModuleMAJ
{
    public class FileUtils
    {

        private static Tracing logger = new Tracing();
        /**
         * param name="sourceFileName": fullpath of source file
         * param name="destinationFileName": fullpath of destination file
         */
        public static void Extract(string sourceFileName, string destinationFileName)
        {
            // TODO
        }

        /**
         * param name="filename": fullpath of file
         */
        public static bool FileExists(String filename)
        {
            bool exists = false;
            try
            {
                exists = File.Exists(filename);
            }
            catch
            {
            }
            return exists;
        }

        /**
         * param name="dirName": fullpath of directory
         */
        public static bool DirectoryExists(String dirName)
        {
            bool exists = false;
            try
            {
                exists = Directory.Exists(dirName);
            }
            catch
            {
            }
            return exists;
        }

        /**
         * param name="sourceDirName": fullpath of source dir
         * param name="destinationDirName": fullpath of destination dir
         */
        public static bool RenameOrMoveDirectory(string sourceDirName, string destinationDirName)
        {
            bool success = false;
            try
            {
                Directory.Move(sourceDirName, destinationDirName);
                success = true;
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("FileUtils Exception : " + ex.Message);
            }
            return success;
        }

        /**
         * param name="sourceFileName": fullpath of source file
         * param name="destinationFileName": fullpath of destination file
         */
        public static bool RenameOrMoveFile(string sourceFileName, string destinationFileName)
        {
            bool success = false;
            try
            {
                if (File.Exists(sourceFileName))
                {
                    File.Move(sourceFileName, destinationFileName);
                    success = true;
                }
            }
            catch
            {
            }
            return success;
        }

        /**
         * param name="sourceFileName": fullpath of source file
         * param name="destinationFileName": fullpath of destination file
         * param name="overwriteIfExist": overwrite if file already exists
         */
        public static bool CopyFile(string sourceFileName, string destinationFileName, bool overwriteIfExist)
        {
            bool success = false;
            try
            {
                File.Copy(sourceFileName, destinationFileName, overwriteIfExist);
                success = true;
            }
            catch
            {
            }
            return success;
        }

        /**
         * param name="dirName": fullpath of directory
         */
        public static bool DeleteDirectory(string dirName)
        {
            bool success = false;
            try
            {
                Directory.Delete(dirName);
                success = true;
            }
            catch
            {
            }
            return success;
        }
        /**
         * param name="dirName": fullpath of directory
         */
        public static bool CreateDirectory(string dirName)
        {
            bool success = false;
            try
            {
                Directory.CreateDirectory(dirName);
                success = true;
            }
            catch
            {
            }
            return success;
        }

        /**
         * param name="filename": fullpath of file
         */
        public static bool DeleteFile(string filename)
        {
            bool success = false;
            try
            {
                File.Delete(filename);
                success = true;
            }
            catch
            {
            }
            return success;
        }

        /**
         * param name="filename": fullpath of file
         * param name="oldValue": old value
         * param name="newValue": new value
         */
        public static bool EditFile(string filename, string oldValue, string newValue)
        {
            try
            {
                string text = File.ReadAllText(filename);
                text = text.Replace(oldValue, newValue);
                File.WriteAllText(filename, text);
                return true;
            }
            catch
            {
                return false;
            }
        } 
        
        public static string ReadFileContent(string filepath)
        {
            string content = null;
            try
            {
                content = File.ReadAllText(filepath);
            }
            catch (Exception ex)
            {
                logger.addLog("Read File Error: " + ex.Message, 0);
            }
            return content;
        }
        
        public static string[] ReadFileLines(string filepath)
        {
            string[] lignes = null;
            try
            {
                lignes = File.ReadAllLines(filepath);
            }
            catch (Exception ex)
            {
                logger.addLog("Read File Error: " + ex.Message, 0);
            }
            return lignes;
        }

    }
}
