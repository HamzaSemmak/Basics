using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using CS_CLIB;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace sorec_gamma.modules.TLV
{

  public static class Utils {

    static byte[] key = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
    
    public static String getSha256(string input) {
      // Create a SHA256 
      using(SHA256 sha256Hash = SHA256.Create()) {
        // ComputeHash - returns byte array 
        // Encoding.UTF8.GetBytes(input)
        byte[] tabByte = Encoding.UTF8.GetBytes(input);
       
        byte[] bytes = sha256Hash.ComputeHash(tabByte);
      
        byte[] macBytes = new HMACSHA512(Utils.key).ComputeHash(bytes);
        // Convert byte array to a string hexa
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++) {
          builder.Append(bytes[i].ToString("x2"));
        }
        
        return builder.ToString();
      }
    }

    public static byte[] macSign(string tlvData) {
      Tracing logger = new Tracing();
      byte[] macBytes = new HMACSHA256(Utils.key).ComputeHash(Encoding.UTF8.GetBytes(tlvData));
     // byte[] macBytes = new HMACSHA512(Utils.key).ComputeHash(Encoding.UTF8.GetBytes(tlvData));
     // logger.addLog(" HMAAC 512 format hex avec conversion et UTF8 format HexString  : " + bytesToHex(macBytes), 1);
     // logger.addLog(" HMACSHA256 format hex avec conversion et UTF8 format HexString  : " + bytesToHex(macBytes), 1);

      return macBytes;
    }


    public static string bytesToHex(byte[] bytes) {
      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < bytes.Length; i++) {
        builder.Append(bytes[i].ToString("X2"));
      }
      return builder.ToString();
    }
    
      public static String HexToASCII(String hexString)
        {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hexString.Length; i += 2)
        {
            string hs = string.Empty;

            hs = hexString.Substring(i, 2);
            ulong decval = Convert.ToUInt64(hs, 16);
            long deccc = Convert.ToInt64(hs, 16);
            char character = Convert.ToChar(deccc);
            sb.Append(character);

        }

        String ascii = sb.ToString();
        return ascii;
        
    }
      public static int HexToInt(String hexString)
      {

          return Convert.ToInt32(hexString,16);

      }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

}