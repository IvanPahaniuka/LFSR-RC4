using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TILab2
{
    //Расширения для массива byte
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Конвертирует массив байт в строку двоичных чисел
        /// </summary>
        /// <param name="arr">Массив для перевода в строку</param>
        /// <returns>Строка из чисел</returns>
        public static string ToBitString(this byte[] arr)
        {
            return arr.ToBitString(arr.Length);
        }

        /// <summary>
        /// Конвертирует массив байт в строку двоичных чисел
        /// </summary>
        /// <param name="arr">Массив для перевода в строку</param>
        /// <param name="bytesLength">Число байт для перевода, начиная с 0-го элемента массива</param>
        /// <returns>Строка из чисел</returns>
        public static string ToBitString(this byte[] arr, int bytesLength)
        {
            if (arr == null)
                return "";

            int length = arr.Length >= bytesLength ? bytesLength : arr.Length;

            StringBuilder sbuilder = new StringBuilder(length * 9);
            int p = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 7; j >= 0; j--)
                    sbuilder.Insert(p++, ((arr[i] >> j) & 1) == 0 ? '0' : '1');

                sbuilder.Insert(p++, ' ');
            }

            if (length > 0)
                sbuilder.Remove(length*9-1, 1);

            return sbuilder.ToString();
        }

        /// <summary>
        /// Конвертирует массив байт в строку десятичных чисел
        /// </summary>
        /// <param name="arr">Массив для перевода в строку</param>
        /// <returns>Строка из чисел</returns>
        public static string ToDigString(this byte[] arr)
        {
            return arr.ToDigString(arr.Length);
        }

        /// <summary>
        /// Конвертирует массив байт в строку десятичных чисел
        /// </summary>
        /// <param name="arr">Массив для перевода в строку</param>
        /// <param name="bytesLength">Число байт для перевода, начиная с 0-го элемента массива</param>
        /// <returns>Строка из чисел</returns>
        public static string ToDigString(this byte[] arr, int bytesLength)
        {
            if (arr == null)
                return "";

            int length = arr.Length >= bytesLength ? bytesLength : arr.Length;

            StringBuilder sbuilder = new StringBuilder(length * 4);
            int p = 0;
            string t;
            for (int i = 0; i < length; i++)
            {
                t = arr[i].ToString();
                sbuilder.Insert(p, arr[i].ToString());
                p += t.Length;
                sbuilder.Insert(p++, ' ');
            }

            if (length > 0)
                sbuilder.Remove(sbuilder.Length-1, 1);

            return sbuilder.ToString();
        }
    }

    //Расширения для string
    public static class StringExtensions
    {
        /// <summary>
        /// Конвертирует последовательность десятичных чисел в массив byte
        /// </summary>
        /// <param name="line">Строка чисел (разделены любыми символами, кроме цифр)</param>
        /// <returns>Массив чисел (большие числа пропускаются)</returns>
        public static byte[] DigToByteArray(this string line)
        {
            string t = "";
            List<byte> arr = new List<byte>();
            for (int i = 0; i < line.Length; i++)
                if (char.IsDigit(line[i]))
                    t += line[i];
                else
                {
                    if (t.Length > 0)
                        if (byte.TryParse(t, out byte bt))
                            arr.Add(bt);
                    t = "";
                }

            if (t.Length > 0)
                if (byte.TryParse(t, out byte bt))
                    arr.Add(bt);

            return arr.ToArray();
        }

        /// <summary>
        /// Конвертирует последовательность двоичных чисел в массив byte
        /// </summary>
        /// <param name="line">Строка чисел (разделены любыми символами, кроме цифр)</param>
        /// <returns>Массив чисел (большие числа пропускаются)</returns>
        public static byte[] BitToByteArray(this string lineSelf)
        {
            string line = new string(lineSelf.Where(x => x == '0' || x == '1').ToArray());

            int t = 0;
            int p = 0;
            List<byte> arr = new List<byte>();
            for (int i = line.Length - 1; i >= 0; i--)
            {
                t ^= (line[i] - '0') << p++;
                
                if (p >= 8)
                {
                    arr.Insert(0, (byte)t);
                    t = 0;
                    p = 0;
                }
            }

            if (p > 0)
                arr.Insert(0, (byte)t);

            return arr.ToArray();
        }
    }
}
