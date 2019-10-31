using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoders;
using TILab2.DataConverters;

namespace TILab2
{

    /// <summary>
    /// Параметри шифратора для WPF
    /// </summary>
    public class EncoderParams
    {
        /// <summary>
        /// Шифратор
        /// </summary>
        public IEncoder encoder;
        /// <summary>
        /// Конвертер из byte[] в string и обратно 
        /// </summary>
        public IDataConverter converter;
        /// <summary>
        /// Функция получения ключа
        /// </summary>
        public Func<byte[], long, byte[]> keyGetter;
    }
}
