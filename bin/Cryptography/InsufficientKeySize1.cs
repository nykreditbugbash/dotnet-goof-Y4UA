
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NETStandaloneBlot.Cryptography
{
    class InsufficientKeySize1
    {
        public void Run()
        {
            // Fixed: Use a secure key size (2048 bits or higher)
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
            rsa.Encrypt(new byte[] { }, false);
        }
    }
}
