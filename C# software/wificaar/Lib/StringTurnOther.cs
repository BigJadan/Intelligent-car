using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wificaar.Lib
{
    class StringTurnOther
    {
        public byte CharTurnUint(char str)
        {
            byte temp =0;
            switch (str)
            {
                case 'f':
                    temp = 15;
                    break;
                case 'e':
                    temp = 14;
                    break;
                case 'd':
                    temp = 13;
                    break;
                case 'c':
                    temp = 12;
                    break;
                case 'b':
                    temp = 11;
                    break;
                case 'a':
                    temp = 10;
                    break;
                case '9':
                    temp = 9;
                    break;
                case '8':
                    temp = 8;
                    break;
                case '7':
                    temp = 7;
                    break;
                case '6':
                    temp = 6;
                    break;
                case '5':
                    temp = 5;
                    break;
                case '4':
                    temp = 4;
                    break;
                case '3':
                    temp = 3;
                    break;
                case '2':
                    temp = 2;
                    break;
                case '1':
                    temp = 1;
                    break;
                case '0':
                    temp = 0;
                    break;
            }
            return temp;
        }
        public byte StringTurnUint(string str)
        {
            byte temp =0;

            /* for (int i=0; i < str.Length; i++)
             {
                temp=temp+ CharTurnUint(str[i])*(uint)Math.Pow(16,str.Length-i-1);
                
             }*/
            // temp = (uint)(CharTurnUint(str[0]) + CharTurnUint(str[1]) * 16 + CharTurnUint(str[2]) * 256 + CharTurnUint(str[3]) * 4096);
            temp = (byte)(CharTurnUint(str[0])*16 + CharTurnUint(str[1]));
            return temp;
        }
    }
}
