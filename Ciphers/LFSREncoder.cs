using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encoders
{
    public class LFSREncoder: IEncoder
    {
        private readonly int[] xorRegs;
        private long reg = 0;

        public LFSREncoder(byte[] xorRegs)
        {
            this.xorRegs = xorRegs.Select(x => (int)x).ToArray();
        }

        /// <summary>
        /// Шифрует массив байтов
        /// </summary>
        /// <param name="arr">Массив байтов</param>
        /// <param name="reg">Начальный регистр</param>
        /// <returns>Зашифрованный массив байтов</returns>
        public byte[] Encrypt(byte[] arr, byte[] reg)
        {
            return Decrypt(arr, reg);
        }

        /// <summary>
        /// Расшифровывает массив байтов
        /// </summary>
        /// <param name="arr">Зашифрованный массив байтов</param>
        /// <param name="reg">Начальный регистр</param>
        /// <returns>Исходный текст</returns>
        public byte[] Decrypt(byte[] arr, byte[] reg)
        {
            if (arr == null)
                return null;

            byte[] res = new byte[arr.Length];

            InitializeReg(reg);
            int regLength = xorRegs.Max();

            for (int i = 0; i < arr.Length; i++)
                res[i] = (byte)(arr[i] ^ GetKeyItem(regLength));

            return res;
        }

        public byte[] GetKey(byte[] reg, long size)
        {
            byte[] res = new byte[size];
            int regLength = xorRegs.Max();

            InitializeReg(reg);
            for (int i = 0; i < size; i++)
                res[i] = GetKeyItem(regLength);

            return res;
        }

        private byte GetKeyItem(int regLength)
        {
            long bt = 0, newBit;

            for (int j = 8; j > 0; j--)
            {
                bt = (bt << 1) ^ ((reg >> regLength) & 1);

                newBit = 0;
                foreach (int x in xorRegs)
                    newBit ^= reg >> x;
                newBit &= 1;

                reg = (reg << 1) ^ newBit;
            }

            return (byte)bt;
        }

        private void InitializeReg(byte[] reg)
        {
            this.reg = 0;
            for (int i = 0; i < reg.Length; i++)
            {
                for (int j = 7; j >= 0; j--)
                    this.reg = (this.reg << 1) ^ (((long)reg[i] >> j) & 1);
            }
        }
    }
}
