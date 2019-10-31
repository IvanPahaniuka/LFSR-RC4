using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TILab2;

namespace TILab2.DataConverters
{
    public class RC4DataConverter : IDataConverter
    {
        public byte[] ToByteArray(string line)
        {
            return line.DigToByteArray();
        }

        public string ToText(byte[] data)
        {
            return data.ToDigString(); 
        }

        /// <summary>
        /// Приводит первые byteLength байт в строку
        /// </summary>
        /// <param name="data">Массив для перевода в строку</param>
        /// <param name="byteLength">Число байт для перевода</param>
        /// <returns>Строка чисел</returns>
        public string ToText(byte[] data, int byteLength)
        {
            return data.ToDigString(byteLength);
        }
    }
}
