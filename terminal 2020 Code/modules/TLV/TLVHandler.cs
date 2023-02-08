using System;
using System.Collections.Generic;
using System.Text;
using sorec_gamma.modules.ModuleAuthentification;

namespace sorec_gamma.modules.TLV
{
    class TLVHandler
    {
       ByteBuilder tlvBuffer = new ByteBuilder(256);

    public TLVHandler() {}

    public TLVHandler(String tlv) {
      byte[] bufferReduced = UtilsYP.redHexa(tlv);
      if (bufferReduced == null) {
        /*
      try {
        // Maybe it is in Base 64...
        bufferReduced = UtilsYP.base64Decode(tlv);
      }
      catch (Exception e) {
        if (bufferReduced == null) {
          throw new Exception("Bad TLV format");
        }
      }
		
		*/
      }
      tlvBuffer.append(bufferReduced);
    }

        public static int Response(String response)
        {
            int coderesponse = 0;
            String TLV = getTLVChamps(response);
            TLVHandler appTagsHandlerResponse = new TLVHandler(TLV);
            TLVTags TLVCodesresponse = appTagsHandlerResponse.getTLV(TLVTags.SOREC_CODE_REPONSE);
            byte[] DataCorrespondance;

            if (TLVCodesresponse != null)
            {
                DataCorrespondance = TLVCodesresponse.value;
                try
                {
                    coderesponse = Int32.Parse(Utils.bytesToHex(DataCorrespondance));
                }
                catch (Exception ex)
                {
                    ApplicationContext.Logger.Error("Exception: " + ex.Message + ex.StackTrace);
                }
            }
            return coderesponse;
        }

    public static String getTLVChamps(String response)
    {
        char[] delimiterChars = { ':', ',', '{', '}', '"' };
        string[] tlvChamps = response.Split(delimiterChars);
        String TLV = "";
        foreach (String s in tlvChamps)
        {
            if (s.StartsWith("DF"))
            {
                    TLV = s;
                    break;
            }
        }
        
        return TLV;

    }

    public int add(int tag, long value) {
      return add(tag.ToString("X"), value);
    }
    public int add(int tag, byte[] value)
    {
        return add(tag.ToString("X"), value);
    }
        public int add(String tag, long value) 
        {
            try {
                string strValue = value.ToString();
                if (strValue.Length % 2 == 0) {
                    return add(tag, strValue);
                }
                else {
                    return add(tag, "0" + strValue);
                }
            }
            catch(Exception ex) 
            {
                ApplicationContext.Logger.Error("TLVHandler Exception : " + ex.Message + "\n" + ex.StackTrace);
                return - 1;
            }
        }

    public int add(String tag, String value) {

      if (value == null || value.Length % 2 == 1) {
        //Error
        return - 1;
      }

      if (tag == null || tag.Length % 2 == 1) {
        //Error
        return - 1;
      }

      byte[] valueToAppend = UtilsYP.redHexa(value);
      if (valueToAppend == null) {
        //Error
        return - 1;
      }

      return add(tag, valueToAppend);
    }

    public int add(String tag, byte[] valueToAppend) {

      if (valueToAppend == null) {
        //Error
        return - 1;
      }

      if (tag == null || tag.Length % 2 == 1) {
        //Error
        return - 1;
      }

      tlvBuffer.append(UtilsYP.redHexa(tag));

      int tagLength = valueToAppend.Length;

      if (tagLength < 128) {
        tlvBuffer.append((byte) tagLength);
      }
      else if (tagLength < 256) {
        tlvBuffer.append((byte) 0x81);
        tlvBuffer.append((byte) tagLength);
      }
      else if (tagLength < (256 * 256)) {
        tlvBuffer.append((byte) 0x82);
        tlvBuffer.append((byte)(tagLength >> 8));
        tlvBuffer.append((byte)(tagLength));
      }
      else if (tagLength < (256 * 256 * 256)) {
        tlvBuffer.append((byte) 0x83);
        tlvBuffer.append((byte)(tagLength >> 16));
        tlvBuffer.append((byte)(tagLength >> 8));
        tlvBuffer.append((byte)(tagLength));
      }
      else {
        //Error
        return - 1;
      }

      tlvBuffer.append(valueToAppend);
      return 1;
    }

    public int addASCII(int tag, string asciiValue) {
      return addASCII(tag.ToString("X"), asciiValue);
    }

    public int addASCII(String tag, string asciiValue) {

      if (asciiValue == null) {
        //Error
        return - 1;
      }

      if (tag == null || tag.Length % 2 == 1) {
        //Error
        return - 1;
      }

      tlvBuffer.append(UtilsYP.redHexa(tag));

      int tagLength = asciiValue.Length;

      if (tagLength < 128) {
        tlvBuffer.append((byte) tagLength);
      }
      else if (tagLength < 256) {
        tlvBuffer.append((byte) 0x81);
        tlvBuffer.append((byte) tagLength);
      }
      else if (tagLength < (256 * 256)) {
        tlvBuffer.append((byte) 0x82);
        tlvBuffer.append((byte)(tagLength >> 8));
        tlvBuffer.append((byte)(tagLength));
      }
      else if (tagLength < (256 * 256 * 256)) {
        tlvBuffer.append((byte) 0x83);
        tlvBuffer.append((byte)(tagLength >> 16));
        tlvBuffer.append((byte)(tagLength >> 8));
        tlvBuffer.append((byte)(tagLength));
      }
      else {
        //Error
        return - 1;
      }

      tlvBuffer.append(asciiValue);
      return 1;
    }

    public TLVTags getTLV(int tag) {

      int index = 0;
      int tagToTest;
      int tagToTestLength;
      int bufferLength = tlvBuffer.sizeB();
      while (index < bufferLength) {

        tagToTest = (tlvBuffer.data[index] & 0xFF);
        index++;
        if ((tagToTest & 0x1F) == 0x1F) {
          // Au moins 2 Octets
          tagToTest = tagToTest << 8;
          tagToTest += (tlvBuffer.data[index] & 0xFF);
          index++;
          if ((tagToTest & 0x80) == 0x80) {
            // Au moins 3 Octets
            tagToTest = tagToTest << 8;
            tagToTest += (tlvBuffer.data[index] & 0xFF);
            index++;
            if ((tagToTest & 0x80) == 0x80) {
              // 4 Octets
              tagToTest = tagToTest << 8;
              tagToTest += (tlvBuffer.data[index] & 0xFF);
              index++;
              if ((tagToTest & 0x80) == 0x80) {
                //throw new NoSuchElementException("Bad format");
              }

            }
          }
        }

        // Get length
        tagToTestLength = tlvBuffer.data[index++] & 0xFF;
        if (tagToTestLength > 127) {
          // get number of bytes coding the length
          int length = tagToTestLength & 0x7F;
          if (length > 4) {
            //throw new NoSuchElementException("Bad Length size > 4");
          }

          tagToTestLength = 0;
          for (int loop = 0; loop < length; loop++) {
            tagToTestLength |= (tlvBuffer.data[index++] & 0xFF) << (8 * ((length - (loop + 1))));
          }

          if (tagToTestLength > (256 * 256 * 256)) {
            //throw new NoSuchElementException("Bad Length > " + (256 * 256 * 256));
          }

          if (tagToTestLength < 0) {
            //throw new NoSuchElementException("Bad Length < 0 !!!");
          }
        }
        else if (tagToTestLength < 0) {
          // throw new NoSuchElementException("Bad Length < 0 !!!");
        }

        if (tag == tagToTest) {
            TLVTags tlv = new TLVTags();
          tlv.tag = tag;
          tlv.length = tagToTestLength;
          tlv.value = new byte[tlv.length];

          Array.Copy(tlvBuffer.data, index, tlv.value, 0, tlv.length);

          return tlv;

        }

        index += tagToTestLength;

      }

      return null;

    }
    public string toString() {
      return tlvBuffer.toHexString();
    }

    public byte[] toByte() {
        return tlvBuffer.data;
    }


    public List<TLVTags> getTLVList()
    {

        List<TLVTags> tlvList = new List<TLVTags>();

    int index = 0;
    int tagToTest;
    int tagToTestLength;
    int bufferLength = tlvBuffer.sizeTlv();
    while (index < bufferLength) {

      tagToTest = (tlvBuffer.data[index] & 0xFF);
      index++;
      if ((tagToTest & 0x1F) == 0x1F) {
        // Au moins 2 Octets
        tagToTest = tagToTest << 8;
        tagToTest += (tlvBuffer.data[index] & 0xFF);
        index++;
        if ((tagToTest & 0x80) == 0x80) {
          // Au moins 3 Octets
          tagToTest = tagToTest << 8;
          tagToTest += (tlvBuffer.data[index] & 0xFF);
          index++;
          if ((tagToTest & 0x80) == 0x80) {
            // 4 Octets
            tagToTest = tagToTest << 8;
            tagToTest += (tlvBuffer.data[index] & 0xFF);
            index++;
            if ((tagToTest & 0x80) == 0x80) {
              //throw new NoSuchElementException("Bad format");
            }

          }
        }
      }

      // Get length
      tagToTestLength = tlvBuffer.data[index++] & 0xFF;
      if (tagToTestLength > 127) {
        // get number of bytes coding the length
        int length = tagToTestLength & 0x7F;
        if (length > 4) {
          //throw new NoSuchElementException("Bad Length size > 4");
        }

        tagToTestLength = 0;
        for (int loop = 0; loop < length; loop++) {
          tagToTestLength |= (tlvBuffer.data[index++] & 0xFF) << (8 * ((length - (loop + 1))));
        }

        if (tagToTestLength > (256 * 256 * 256)) {
          //throw new NoSuchElementException("Bad Length > " + (256 * 256 * 256));
        }

        if (tagToTestLength < 0) {
          //throw new NoSuchElementException("Bad Length < 0 !!!");
        }
      }
      else if (tagToTestLength < 0) {
        //throw new NoSuchElementException("Bad Length < 0 !!!");
      }

     // if (tag == tagToTest) {
      TLVTags tlv = new TLVTags();
        tlv.tag = tagToTest;
        tlv.length = tagToTestLength;
        tlv.value = new byte[tlv.length];
        Array.Copy(tlvBuffer.data, index, tlv.value, 0, tlv.length);
        tlvList.Add(tlv);
      //}

      index += tagToTestLength;

    }

    return tlvList;

  }


    public List<TLVTags> getTLVList(int tag)
    {

      List<TLVTags> tlvList = new List<TLVTags>();

    int index = 0;
    int tagToTest;
    int tagToTestLength;
    int bufferLength = tlvBuffer.sizeTlv();
    while (index < bufferLength) {

      tagToTest = (tlvBuffer.data[index] & 0xFF);
      index++;
      if ((tagToTest & 0x1F) == 0x1F) {
        // Au moins 2 Octets
        tagToTest = tagToTest << 8;
        tagToTest += (tlvBuffer.data[index] & 0xFF);
        index++;
        if ((tagToTest & 0x80) == 0x80) {
          // Au moins 3 Octets
          tagToTest = tagToTest << 8;
          tagToTest += (tlvBuffer.data[index] & 0xFF);
          index++;
          if ((tagToTest & 0x80) == 0x80) {
            // 4 Octets
            tagToTest = tagToTest << 8;
            tagToTest += (tlvBuffer.data[index] & 0xFF);
            index++;
            if ((tagToTest & 0x80) == 0x80) {
              //throw new NoSuchElementException("Bad format");
            }

          }
        }
      }

      // Get length
      tagToTestLength = tlvBuffer.data[index++] & 0xFF;
      if (tagToTestLength > 127) {
        // get number of bytes coding the length
        int length = tagToTestLength & 0x7F;
        if (length > 4) {
          //throw new NoSuchElementException("Bad Length size > 4");
        }

        tagToTestLength = 0;
        for (int loop = 0; loop < length; loop++) {
          tagToTestLength |= (tlvBuffer.data[index++] & 0xFF) << (8 * ((length - (loop + 1))));
        }

        if (tagToTestLength > (256 * 256 * 256)) {
          //throw new NoSuchElementException("Bad Length > " + (256 * 256 * 256));
        }

        if (tagToTestLength < 0) {
          //throw new NoSuchElementException("Bad Length < 0 !!!");
        }
      }
      else if (tagToTestLength < 0) {
        //throw new NoSuchElementException("Bad Length < 0 !!!");
      }

     if (tag == tagToTest) {
         TLVTags tlv = new TLVTags();
        tlv.tag = tagToTest;
        tlv.length = tagToTestLength;
        tlv.value = new byte[tlv.length];
        Array.Copy(tlvBuffer.data, index, tlv.value, 0, tlv.length);
        tlvList.Add(tlv);
      }

      index += tagToTestLength;

    }

    return tlvList;

  }
    public int add(int tag, TLVHandler tlvHandler)
    {
        return add(tag, tlvHandler.toString());
    }

    private int add(int tag, string value)
    {
        return add(tag.ToString("X"), value);
    }



    
    }
}
