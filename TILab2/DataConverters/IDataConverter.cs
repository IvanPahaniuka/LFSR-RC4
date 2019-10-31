using Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TILab2.DataConverters
{
    public interface IDataConverter
    {
        string ToText(byte[] data);
        string ToText(byte[] data, int byteLength);
        byte[] ToByteArray(string line);
    }
}
