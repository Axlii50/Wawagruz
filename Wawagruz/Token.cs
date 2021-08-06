using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wawagruz
{
    public class Token
    {
        private DateTime Expire;
        public readonly string TokenString;

        public Token(string Token, DateTime ExpireTime)
        {
            this.Expire = ExpireTime;
            this.TokenString = Token;
        }

        /// <summary>
        /// If expired true else false
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            return Expire - DateTime.Now > TimeSpan.Zero ? false : true;
        }
    }
}
