using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.TLV
{
    class UtilsYP
    {

        public static byte[] redHexa(string s)
        {
            if (s.Length % 2 == 1)
            {
                return null;
            }
            int n = s.Length / 2;
            byte[] array = new byte[n];
            if (redHexa(array, 0, s, n) == 1)
            {
                return array;
            }
            return null;
        }

        public static int redHexa(byte[] array, int n, String s, int n2)
        {
            return redHexa(array, n, s, 0, n2);
        }

        public static int redHexa(byte[] array, int n, String s, int n2, int n3)
        {
            for (int i = 0; i < n3; ++i)
            {
                int intValue1 = intValue(s[n2 + i * 2]);
                int intValue2 = intValue(s[n2 + (i * 2 + 1)]);
                if (intValue1 == -1 || intValue2 == -1)
                {
                    return -1;
                }
                array[n + i] = (byte)((intValue1 << 4) + intValue2);
            }
            return 1;
        }

        public static int intValue(char c)
        {
            switch (c)
            {
                case '0':
                    {
                        return 0;
                    }
                case '1':
                    {
                        return 1;
                    }
                case '2':
                    {
                        return 2;
                    }
                case '3':
                    {
                        return 3;
                    }
                case '4':
                    {
                        return 4;
                    }
                case '5':
                    {
                        return 5;
                    }
                case '6':
                    {
                        return 6;
                    }
                case '7':
                    {
                        return 7;
                    }
                case '8':
                    {
                        return 8;
                    }
                case '9':
                    {
                        return 9;
                    }
                case 'a':
                    {
                        return 10;
                    }
                case 'b':
                    {
                        return 11;
                    }
                case 'c':
                    {
                        return 12;
                    }
                case 'd':
                    {
                        return 13;
                    }
                case 'e':
                    {
                        return 14;
                    }
                case 'f':
                    {
                        return 15;
                    }
                case 'A':
                    {
                        return 10;
                    }
                case 'B':
                    {
                        return 11;
                    }
                case 'C':
                    {
                        return 12;
                    }
                case 'D':
                    {
                        return 13;
                    }
                case 'E':
                    {
                        return 14;
                    }
                case 'F':
                    {
                        return 15;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }
    }
}
