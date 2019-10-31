using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encoders
{
    public interface IEncoder
    {
        /// <summary>
        /// Расшифровывает массив байтов 
        /// </summary>
        /// <param name="arr">Зашифрованный массив байтов</param>
        /// <param name="reg">Ключ</param>
        /// <returns>Исходный текст</returns>
        byte[] Decrypt(byte[] arr, byte[] key);

        /// <summary>
        /// Шифрует массив байтов
        /// </summary>
        /// <param name="arr">Массив байтов</param>
        /// <param name="reg">Ключ</param>
        /// <returns>Зашифрованный массив байтов</returns>
        byte[] Encrypt(byte[] arr, byte[] key);
    }
}
