using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NancyBlog.Domain
{    
    public class UserAccount : Entity
    {
        public string Username { get; set; }
        //TODO: OrmLite saving byte[] didn't work, testing with string first.
        //public byte[] Password { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        //-------------------------------------------------------------------------------- 

        public virtual void SetPassword(string plainTextPassword)
        {
            Password = plainTextPassword; //Encrypt(plainTextPassword);
        }

        /// <summary>
        /// Check whether attemptedPassword is the same as stored password for the user.
        /// </summary>
        /// <param name="attemptedPassword"></param>
        /// <returns>Returns true if attemptedPassword is same as stored password</returns>
        public virtual bool CheckPassword(string attemptedPassword)
        {
            return attemptedPassword == Password; // Decrypt(this.Password);
        }

        #region "Encryption stuff"
        //-------------------------------------------------------------------------------- 

        protected static SymmetricAlgorithm algorithm = BuildAlgorithm();

        private static SymmetricAlgorithm BuildAlgorithm()
        {
            var algo = RijndaelManaged.Create();

            algo.Key = Encoding.UTF8.GetBytes("sampleSAMPLEsamp");
            algo.IV = Encoding.UTF8.GetBytes("testTESTtestTEST");

            Array.Reverse(algo.Key);
            Array.Reverse(algo.IV);

            return algo;
        }

        protected byte[] Encrypt(string clearText)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms,
                                                    algorithm.CreateEncryptor(),
                                                    CryptoStreamMode.Write);

                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(clearText);
                    sw.Close();
                    return ms.ToArray();
                }
            }
        }

        protected string Decrypt(byte[] cipherText)
        {
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                CryptoStream cs = new CryptoStream(ms,
                                            algorithm.CreateDecryptor(),
                                            CryptoStreamMode.Read);
                using (StreamReader sr = new StreamReader(cs))
                {
                    string val = sr.ReadToEnd();
                    sr.Close();
                    return val;
                }
            }
        }

        //-------------------------------------------------------------------------------- 
        #endregion
    }
}
