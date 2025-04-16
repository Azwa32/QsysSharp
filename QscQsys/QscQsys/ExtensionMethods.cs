using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QscQsys
{
    /// <summary>
    /// Extension methods for string manipulation
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// Break a string up into maximum size chunks
        /// </summary>
        /// <param name="source">String to chunk</param>
        /// <param name="maxChunkSize">Maximum size of chunks</param>
        /// <returns></returns>
        public static IEnumerable<string> Chunk(this string source, int maxChunkSize)
        {
            for (int i = 0; i < source.Length; i += maxChunkSize)
                yield return source.Substring(i, Math.Min(maxChunkSize, source.Length - i));
        }
    }

    /// <summary>
    /// Helper methods for creating XSig byte sequences compatible with the Intersystem Communications (ISC) symbol.
    /// </summary>
    /// <remarks>Indexing is not from the start of each signal type but rather from the beginning of the first
    /// analog signal for digital, analog, and serial outputs.</remarks>
    public static class XSig
    {
        /// <summary>
        /// Forces all outputs to 0.
        /// </summary>
        /// <returns>Bytes in XSig format for clear outputs trigger.</returns>
        public static byte[] ClearOutputs()
        {
            return new byte[] { 0xFC };
        }

        /// <summary>
        /// Evaluate all inputs and re-transmit any digital, analog, and permanent serail signals not set to 0.
        /// </summary>
        /// <returns>Bytes in XSig format for send status trigger.</returns>
        public static byte[] SendStatus()
        {
            return new byte[] { 0xFD };
        }

        /// <summary>
        /// Get bytes for a single digital signal.
        /// </summary>
        /// <param name="index">0-based digital index</param>
        /// <param name="value">Digital data to be encoded</param>
        /// <returns>Bytes in XSig format for digtial information.</returns>
        /// <exception cref="ArgumentException">Index out of range for digital information encoded in XSig format.</exception>
        public static byte[] GetBytes(int index, bool value)
        {
            // 12-bits available for digital encoded data
            if (index >= 4096 || index < 0)
                throw new ArgumentException("index");

            return new[] {
                (byte)(0x80 | (value ? 0 : 0x20) | (index >> 7)),
                (byte)((index - 1) & 0x7F)
            };
        }

        /// <summary>
        /// Get byte sequence for multiple digital signals.
        /// </summary>
        /// <param name="startIndex">Starting index of the sequence.</param>
        /// <param name="values">Digital signal value array.</param>
        /// <returns>Byte sequence in XSig format for digital signal information.</returns>
        public static byte[] GetBytes(int startIndex, bool[] values)
        {
            // Digital XSig data is 2 bytes per value
            const int fixedLength = 2;
            byte[] bytes = new byte[values.Length * fixedLength];
            for (int i = 0; i < values.Length; i++)
            {
                Buffer.BlockCopy(GetBytes(startIndex++, values[i]), 0, bytes, i * fixedLength, fixedLength);
            }

            return bytes;
        }

        /// <summary>
        /// Get bytes for a single analog signal.
        /// </summary>
        /// <param name="index">0-based analog index</param>
        /// <param name="value">Analog data to be encoded</param>
        /// <returns>Bytes in XSig format for analog signal information.</returns>
        /// <exception cref="ArgumentException">Index out of range for analog information encoded in XSig format.</exception>
        public static byte[] GetBytes(int index, ushort value)
        {
            // 10-bits available for analog encoded data
            if (index >= 1024 || index < 0)
                throw new ArgumentException("index");

            return new[] {
                (byte)(0xC0 | ((value & 0xC000) >> 10) | (index >> 7)),
                (byte)((index - 1) & 0x7F),
                (byte)((value & 0x3F80) >> 7),
                (byte)(value & 0x7F)
            };
        }

        /// <summary>
        /// Get byte sequence for multiple analog signals.
        /// </summary>
        /// <param name="startIndex">Starting index of the sequence.</param>
        /// <param name="values">Analog signal value array.</param>
        /// <returns>Byte sequence in XSig format for analog signal information.</returns>
        public static byte[] GetBytes(int startIndex, ushort[] values)
        {
            // Analog XSig data is 4 bytes per value
            const int fixedLength = 4;
            byte[] bytes = new byte[values.Length * fixedLength];
            for (int i = 0; i < values.Length; i++)
            {
                Buffer.BlockCopy(GetBytes(startIndex++, values[i]), 0, bytes, i * fixedLength, fixedLength);
            }

            return bytes;
        }

        /// <summary>
        /// Get bytes for a single serial signal.
        /// </summary>
        /// <param name="index">0-based serial index</param>
        /// <param name="value">Serial data to be encoded</param>
        /// <returns>Bytes in XSig format for serial signal information.</returns>
        /// <exception cref="ArgumentException">Index out of range for serial information encoded in XSig format.</exception>
        public static byte[] GetBytes(int index, string value)
        {
            // 10-bits available for serial encoded data
            if (index >= 1024 || index < 0)
                throw new ArgumentException("index");

            var serialBytes = Encoding.GetEncoding(28591).GetBytes(value);
            byte[] xsig = new byte[serialBytes.Length + 3];
            xsig[0] = (byte)(0xC8 | (index >> 7));
            xsig[1] = (byte)((index - 1) & 0x7F);
            xsig[xsig.Length - 1] = 0xFF;

            Buffer.BlockCopy(serialBytes, 0, xsig, 2, serialBytes.Length);
            return xsig;
        }

        /// <summary>
        /// Get byte sequence for multiple serial signals.
        /// </summary>
        /// <param name="startIndex">Starting index of the sequence.</param>
        /// <param name="values">Serial signal value array.</param>
        /// <returns>Byte sequence in XSig format for serial signal information.</returns>
        public static byte[] GetBytes(int startIndex, string[] values)
        {
            // Serial XSig data is not fixed-length like the other formats
            int offset = 0;
            byte[] bytes = new byte[values.Sum(v => v.Length + 3)];
            for (int i = 0; i < values.Length; i++)
            {
                var data = GetBytes(startIndex++, values[i]);
                Buffer.BlockCopy(data, 0, bytes, offset, data.Length);
                offset += data.Length;
            }

            return bytes;
        }
    }

    public static class EnumerableExtensionMethods
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T item in source)
            {
                action(item);
            }
        }
    }
}