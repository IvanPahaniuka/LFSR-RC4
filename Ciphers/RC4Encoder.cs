using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encoders
{
    public class RC4Encoder: IEncoder
    {
        private byte[] keyBase = new byte[256];
        private int x = 0;
        private int y = 0;

        /// <summary>
        /// Шифрует массив байтов
        /// </summary>
        /// <param name="arr">Зашифрованный массив байтов</param>
        /// <param name="rndKey">Случайная последовательность байтов (ключ для инициализации)</param>
        /// <returns>Исходный текст</returns>
        public byte[] Encrypt(byte[] arr, byte[] rndKey)
        {
            return Decrypt(arr, rndKey);
        }

        /// <summary>
        /// Расшифровывает массив байтов
        /// </summary>
        /// <param name="arr">Зашифрованный массив байтов</param>
        /// <param name="rndKey">Случайная последовательность байтов (ключ для инициализации)</param>
        /// <returns>Исходный текст</returns>
        public byte[] Decrypt(byte[] arr, byte[] rndKey)
        {
            if (arr == null)
                return null;

            InitializeKey(rndKey);

            int len = arr.Length;
            byte[] data = new byte[len];
            for (int i = 0; i < len; i++)
                data[i] = (byte)(arr[i] ^ GetKeyItem());

            return data;
        }

        public byte[] GetKey(byte[] rndKey, long size)
        {
            byte[] res = new byte[size];
            InitializeKey(rndKey);

            for (int i = 0; i < size; i++)
                res[i] = GetKeyItem();

            return res;
        }

        private void InitializeKey(byte[] rndKey)
        {
            for (int i = 0; i < 256; i++)
            {
                keyBase[i] = (byte)i;
            }

            if (rndKey.Length == 0)
            {
                Array.Resize(ref rndKey, 1);
                rndKey[0] = 0;
            }

            int keyLength = rndKey.Length;
            int j = 0;
            x = 0;
            y = 0;
            byte t;
            for (int i = 0; i < 256; i++)
            {
                j = (j + keyBase[i] + rndKey[i % keyLength]) % 256;

                t = keyBase[i];
                keyBase[i] = keyBase[j];
                keyBase[j] = t;
            }
        }

        private byte GetKeyItem()
        {
            x = (x + 1) % 256;
            y = (y + keyBase[x]) % 256;

            byte t = keyBase[x];
            keyBase[x] = keyBase[y];
            keyBase[y] = t;

            return keyBase[(keyBase[x] + keyBase[y]) % 256];
        }
    }
}
