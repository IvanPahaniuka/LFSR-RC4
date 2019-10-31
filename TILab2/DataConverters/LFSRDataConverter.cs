using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TILab2;

namespace TILab2.DataConverters
{
    public class LFSRDataConverter : IDataConverter
    {
        public byte[] ToByteArray(string line)
        {
            return line.BitToByteArray();
        }

        public string ToText(byte[] data)
        {
            return data.ToBitString();
        }

        /// <summary>
        /// Приводит первые byteLength байт в строку
        /// </summary>
        /// <param name="data">Массив для перевода в строку</param>
        /// <param name="byteLength">Число байт для перевода</param>
        /// <returns>Строка чисел</returns>
        public string ToText(byte[] data, int byteLength)
        {
            return data.ToBitString(byteLength);
        }
    }
}
