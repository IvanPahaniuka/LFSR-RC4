using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Encoders
{
    public class FileEncoder
    {
        public IEncoder Encoder { get; set; } = null;


        public FileEncoder(): this(null) { }

        public FileEncoder(IEncoder encoder)
        {
            Encoder = encoder;
        }

        /// <summary>
        /// Шифрует содержимое файла srcFileName и сохраняет его в destFileName
        /// </summary>
        /// <param name="srcFileName">Шифруемый файл</param>
        /// <param name="destFileName">Конечный файл</param>
        /// <param name="key">Ключ</param>
        public byte[] Encrypt(string srcFileName, string destFileName, byte[] key)
        {
            byte[] data = File.ReadAllBytes(srcFileName);
            data = Encoder.Encrypt(data, key);
            File.WriteAllBytes(destFileName, data);

            return data;
        }

        /// <summary>
        /// Расшифровывает содержимое файла srcFileName и сохраняет его в destFileName
        /// </summary>
        /// <param name="srcFileName">Расшифровываемый файл</param>
        /// <param name="destFileName">Конечный файл</param>
        /// <param name="key">Ключ</param>
        public byte[] Decrypt(string srcFileName, string destFileName, byte[] key)
        {
            byte[] data = File.ReadAllBytes(srcFileName);
            data = Encoder.Decrypt(data, key);
            File.WriteAllBytes(destFileName, data);

            return data;
        }
    }
}
