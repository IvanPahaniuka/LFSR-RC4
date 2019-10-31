using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherTester
{
    public static class ByteArrayExtension
    {
        public static string ToBitString(this byte[] arr)
        {
            return arr.ToBitString(arr.Length);
        }

        public static string ToBitString(this byte[] arr, int bytesLength)
        {
            int length = arr.Length >= bytesLength ? bytesLength : arr.Length;

            StringBuilder sbuilder = new StringBuilder(length * 9);
            int p = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 7; j >= 0; j--)
                    sbuilder.Insert(p++, ((arr[i] >> j) & 1) == 0 ? '0' : '1');

                sbuilder.Insert(p++, ' ');
            }

            sbuilder.Remove(length*9-1, 1);

            return sbuilder.ToString();
        }
    }
}
