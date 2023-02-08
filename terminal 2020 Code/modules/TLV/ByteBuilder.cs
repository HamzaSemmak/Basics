using System;
using System.Collections.Generic;
using System.Text;
using CS_CLIB;

namespace sorec_gamma.modules.TLV
{
    class ByteBuilder
    {
        Tracing logger = new Tracing();
        public byte[] data;
        private int size;
        static byte[] digits = new byte[] {
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    97,
    98,
    99,
    100,
    101,
    102,
    103,
    104,
    105,
    106,
    107,
    108,
    109,
    110,
    111,
    112,
    113,
    114,
    115,
    116,
    117,
    118,
    119,
    120,
    121,
    122
  };
        static byte[] digitTens = new byte[] {
    48,
    48,
    48,
    48,
    48,
    48,
    48,
    48,
    48,
    48,
    49,
    49,
    49,
    49,
    49,
    49,
    49,
    49,
    49,
    49,
    50,
    50,
    50,
    50,
    50,
    50,
    50,
    50,
    50,
    50,
    51,
    51,
    51,
    51,
    51,
    51,
    51,
    51,
    51,
    51,
    52,
    52,
    52,
    52,
    52,
    52,
    52,
    52,
    52,
    52,
    53,
    53,
    53,
    53,
    53,
    53,
    53,
    53,
    53,
    53,
    54,
    54,
    54,
    54,
    54,
    54,
    54,
    54,
    54,
    54,
    55,
    55,
    55,
    55,
    55,
    55,
    55,
    55,
    55,
    55,
    56,
    56,
    56,
    56,
    56,
    56,
    56,
    56,
    56,
    56,
    57,
    57,
    57,
    57,
    57,
    57,
    57,
    57,
    57,
    57
  };
        static byte[] digitOnes = new byte[] {
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57,
    48,
    49,
    50,
    51,
    52,
    53,
    54,
    55,
    56,
    57
  };
        static int[] sizeTable = new int[] {
    9,
    99,
    999,
    9999,
    99999,
    999999,
    9999999,
    99999999,
    999999999,
    0x7fffffff
  };

        public ByteBuilder()
        {
            //this(256);
        }

        public ByteBuilder(int n)
        {
            this.data = new byte[n];
            this.size = 0;
        }

        public int sizeB()
        {
            return this.size;
        }

        public ByteBuilder append(byte[] array)
        {
            return this.append(array, 0, array.Length);
        }

        public ByteBuilder append(byte[] array, int n, int n2)
        {
            if (n + n2 > array.Length)
            {
                //throw new ArrayIndexOutOfBoundsException();
            }
            this.testAddition(n2);
            for (int i = 0; i < n2; ++i)
            {
                this.data[this.size + i] = array[i + n];
            }
            this.size += n2;
            return this;
        }

        private void testAddition(int n)
        {
            if (this.size + n >= this.data.Length)
            {
                int n2;
                for (n2 = 2; this.size + n >= this.data.Length * n2; n2 *= 2) {
                    
                }

                // this.data = Arrays.CopyOf(this.data, this.data.Length * n2);
                
                byte[] data2 = new byte[this.data.Length * n2];
                
                this.data.CopyTo(data2, 0);
                this.data = data2;
            }
        }

        public ByteBuilder append(byte b)
        {
            this.testAddition(1);
            this.data[this.size++] = b;
            return this;
        }

        public ByteBuilder append(string s)
        {
            int length = s.Length;
            this.testAddition(length);
            for (int i = 0; i < length; ++i)
            {
                this.data[this.size + i] = Convert.ToByte(s[i]);
            }
            this.size += length;
            return this;
        }

        public String toHexString()
        {
            //return UtilsYP.devHexa(this.data, 0, this.size);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.size; i++)
            {
                builder.Append(this.data[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public int sizeTlv()
        {
            return this.size;
        }
    }
}
