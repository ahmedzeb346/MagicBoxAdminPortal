using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagicBoxAdminPortal.generalrepositoryinterface.CreateHashByKey
{
    public class CreateHashByKey : ICreateHashByKey
    {
        public string CreateByKeyHash(string valuetoHash, string keyValue)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                Encoding enc = Encoding.UTF8;
                byte[] keyByte = enc.GetBytes(keyValue);
                byte[] messageBytes = enc.GetBytes(valuetoHash);
                using (HMACSHA256 hmacsha256 = new HMACSHA256(keyByte))
                {
                    byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                    //return Convert.ToBase64String(hashmessage);

                    foreach (Byte b in hashmessage)
                        sb.Append(b.ToString("x2"));

                    return sb.ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
