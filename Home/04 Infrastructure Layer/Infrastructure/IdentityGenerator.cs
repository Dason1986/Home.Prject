using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Infrastructure
{
    public class IdentityGenerator
    {
		[DllImport("rpcrt4.dll", SetLastError = true)]
		static extern int UuidCreateSequential(out Guid guid);
		public static Guid Next(Guid id)
		{
			var arry = id.ToByteArray();
			const byte sub = 1;
			var b = arry[3];
			if (b != byte.MaxValue)
			{
				arry[3] = (byte) (b + sub);
				return new Guid(arry);
			}
			arry[3] = 0;
			b = arry[2];
			if (b != byte.MaxValue)
			{
				arry[2] = (byte) (b + sub);
				return new Guid(arry);
			}
			arry[2] = 0;
			b = arry[1];
			if (b != byte.MaxValue)
			{
				arry[1] = (byte) (b + sub);
				return new Guid(arry);
			}
			arry[1] = 0;
			b = arry[0];
			arry[0] = (byte) (b + sub);
			return new Guid(arry);
		}
		public static Guid NewSequentialGuid()
		{
			Guid guid;
			UuidCreateSequential(out guid);
			var s = guid.ToByteArray();
			var t = new byte[16];
			t[3] = s[0];
			t[2] = s[1];
			t[1] = s[2];
			t[0] = s[3];
			t[5] = s[4];
			t[4] = s[5];
			t[7] = s[6];
			t[6] = s[7];
			t[8] = s[8];
			t[9] = s[9];
			t[10] = s[10];
			t[11] = s[11];
			t[12] = s[12];
			t[13] = s[13];
			t[14] = s[14];
			t[15] = s[15];
			return new Guid(t);
		}
    }
}
