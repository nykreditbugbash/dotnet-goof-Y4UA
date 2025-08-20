
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NETStandaloneBlot.Cryptography
{
    class InsecureECB1
    {
        public void Run()
        {
            // Use secure CBC mode instead of insecure ECB mode
            AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };

            AesCryptoServiceProvider aesCryptoServiceProvider2 = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };

            AesManaged aesManaged = new AesManaged
            {
                Mode = CipherMode.CBC
            };

            AesManaged aesManaged2 = new AesManaged
            {
                Mode = CipherMode.CBC
            };

            RijndaelManaged rm = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

            RijndaelManaged rm2 = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };

            TripleDESCryptoServiceProvider tdes2 = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC
            };


        }
    }
}
