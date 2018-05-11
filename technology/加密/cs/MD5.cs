using System;
using System.Security.Cryptography;
using System.Text;

namespace test1
{
    class Program
    {

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            byte[] source = Encoding.UTF8.GetBytes("\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u0010" +
                                                   "\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u0010" +
                                                   "\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u0010" +
                                                   "\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u0010" +
                                                   "\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u0010" +
                                                   "\u0001\u0002\u0003\u0004");

            //c# 自带的api
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(source);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            Console.WriteLine(sb + "\n");
            md5.Clear();


            //使用自己实现的api
            sb.Length = 0;
            byte[] ret = new byte[16];
            MD5Impliment(source, (uint)source.Length, ret);
            for (int i = 0; i < ret.Length; i++)
            {
                sb.Append(ret[i].ToString("x2"));
            }
            Console.WriteLine(sb + "\n");

        }

        private static void MD5Impliment(byte[] initial_msg, UInt64 initial_len, byte[] digest)
        {

            //Note: All variables are unsigned 32 bits and wrap modulo 2^32 when calculating
            uint[] r = new uint[64];
            uint[] k = new uint[64];
            r = new uint[]
            {
                7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
                5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
                4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
                6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21
            };
            for (int j = 0; j < 64; j++)
            {
                k[j] = Convert.ToUInt32(Math.Floor(Math.Abs(Math.Sin(j + 1))*Math.Pow(2, 32)));
//                Console.WriteLine("k["+j+"]="+k[j].ToString("x2"));
            }
            //四个标准常数，其实就是0123456789ABCDEF
            uint h0 = 0x67452301;
            uint h1 = 0xEFCDAB89;
            uint h2 = 0x98BADCFE;
            uint h3 = 0x10325476;



            uint[] w = new uint[16];
            UInt64 new_len, offset;
            //Pre-processing:
            //append "1" bit to message    
            //append "0" bits until message length in bits ≡ 448 (mod 512)
            //append length mod (2^64) to message
            for (new_len = initial_len + 1; new_len % (512 / 8) != 448 / 8; new_len++);
            Console.WriteLine("加密信息原长度= " + initial_len+"Byte");
            Console.WriteLine("需要填充至    = " + new_len + "Byte");

            byte[] msg = new byte[new_len + 8];
            initial_msg.CopyTo(msg, 0);
            msg[initial_len] = 0x80; // append the "1" bit; most significant bit is "first"
            Console.Write("填充\"1\"后msg    =");
            for (int x = 0; x < msg.Length; x++)
            {
                if(x%8==0)
                    Console.Write("\n");
                Console.Write(msg[x].ToString("x2")+" ");
            }


            Console.WriteLine("\n添加\"1\"之后，后面全部填充0\n");
            for (offset = initial_len + 1; offset < new_len; offset++)
                msg[offset] = 0; // append "0" bits



            // append the len in bits at the end of the buffer.
            to_bytes(initial_len * 8, msg, new_len);
            Console.Write("添加源信息长度(按bit计算)至msg(先放低32位)=");
            for (int x = 0; x < msg.Length; x++)
            {
                if (x % 8 == 0)
                    Console.Write("\n");
                Console.Write(msg[x].ToString("x2") + " ");
            }
            Console.WriteLine("\n");

            // initial_len>>29 == initial_len*8>>32, but avoids overflow.
            to_bytes(initial_len >> 29, msg, new_len + 4);

            Console.Write("添加源信息长度(bit)至msg(再放高32位)=");
            for (int x = 0; x < msg.Length; x++)
            {
                if (x % 8 == 0)
                    Console.Write("\n");
                Console.Write(msg[x].ToString("x2") + " ");
            }
            Console.WriteLine("\n");


            uint a, b, c, d, i, f, g, temp;
            //main loop
            //每次加64位偏移
            for (offset = 0; offset < new_len; offset += (512/8))
            {

                // break chunk into sixteen 32-bit words w[j], 0 ≤ j ≤ 15
                //取16个32位 即 16 * 4byte = 64byte
                for (i = 0; i < 16; i++)
                    w[i] = to_int32(msg, offset + i*4);

                // Initialize hash value for this chunk:
                a = h0;
                b = h1;
                c = h2;
                d = h3;

                // Main loop:
                //对取出的16*32 = 512位  进行64次骚操作
                for (i = 0; i < 64; i++)
                {
                    
                    if (i < 16)
                    {
                        f = (b & c) | ((~b) & d);
                        g = i;
                    }
                    else if (i < 32)
                    {
                        f = (d & b) | ((~d) & c);
                        g = (5*i + 1)%16;
                    }
                    else if (i < 48)
                    {
                        f = b ^ c ^ d;
                        g = (3*i + 5)%16;
                    }
                    else
                    {
                        f = c ^ (b | (~d));
                        g = (7*i)%16;
                    }

                    temp = d;
                    d = c;
                    c = b;
                    b = b + ROTATE_LEFT((a + f + k[i] + w[g]), (int)r[i]);
                    a = temp;

                }
                // Add this chunk's hash to result so far:
                // 对于(5*i + 1)%16  (3*i + 5)%16  (7*i)%16  i递增16次，计算结果可以覆盖0-15
                //对16个32位的w 进行4轮(64/16)的反复处理
                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;

            }
            // cleanup
            

            //var char digest[16] := h0 append h1 append h2 append h3 //(Output is in little-endian)
            to_bytes(h0, digest,0);
            to_bytes(h1, digest,4);
            to_bytes(h2, digest,8);
            to_bytes(h3, digest,12);
        }

        private static uint ROTATE_LEFT(uint x, int n)
        {
            return (((x) << (n)) | ((x) >> (32 - (n))));
        }
        private static  uint to_int32(byte[] bytes,UInt64 index)
        {
            return (uint)bytes[0 + index]
                | ((uint)bytes[1 + index] << 8)
                | ((uint)bytes[2 + index] << 16)
                | ((uint)bytes[3 + index] << 24);
        }
        private static void to_bytes(UInt64 val, byte[] bytes,UInt64 index)
        {
            bytes[0 + index] = (byte)val;
            bytes[1 + index] = (byte)(val >> 8);
            bytes[2 + index] = (byte)(val >> 16);
            bytes[3 + index] = (byte)(val >> 24);
        }
    }
}