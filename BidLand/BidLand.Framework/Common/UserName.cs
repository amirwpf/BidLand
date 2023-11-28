using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidLand.Framework.Common
{
	public static class UserName
	{

		public static string Email2UserName(this string email)
		{
			int indx = email.IndexOf('@');
			string res = email.Substring(0 ,indx);
			return res;
		}
	}
}
