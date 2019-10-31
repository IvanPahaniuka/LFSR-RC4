using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encoders
{
    /// <summary>
    /// Класс ключевых исключений для шифраторов
    /// </summary>
    public class KeyException : Exception
    {
        public KeyException(): base(){}
        public KeyException(string message): base(message) { }
    }
}
