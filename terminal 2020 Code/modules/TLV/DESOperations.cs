using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using CS_CLIB;

namespace sorec_gamma.modules.TLV
{
    class DESOperations
    {

        private static string key1 = "0102030405060708090A0B0C0D0E0F10";
        private static string key2 = "0102030405060708";
        public static string Encrypt(string toEncrypt)
        {
            Tracing logger = new Tracing();
           
            byte[] keyArray = UtilsYP.redHexa(key1);
            byte[] toEncryptArray = UtilsYP.redHexa(toEncrypt);
           
           TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
           tdes.Key = keyArray;
          
           tdes.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.None;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
       
            return Utils.bytesToHex(resultArray);

        }
        public static string EncryptSimple(string toEncrypt)
        {
            Tracing logger = new Tracing();
           
            byte[] keyArray = UtilsYP.redHexa(key2);
            byte[] toEncryptArray = UtilsYP.redHexa(toEncrypt);

            DESCryptoServiceProvider tdes = new DESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.None;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format

            return Utils.bytesToHex(resultArray);

        }
        public static string Decrypt(string cipherString)
        {
            
            //get the byte code of the string

           byte[] toEncryptArray = UtilsYP.redHexa(cipherString);
           byte[] keyArray = UtilsYP.redHexa(key1);
            
           TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
           tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

           tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

           tdes.Padding = PaddingMode.None;

           ICryptoTransform cTransform = tdes.CreateDecryptor();
           byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
           tdes.Clear();
            //return the Clear decrypted TEXT
          return  Utils.bytesToHex(resultArray);
        }

         
    }
}
