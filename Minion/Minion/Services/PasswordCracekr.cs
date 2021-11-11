using Minion.Entities;
using Minion.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minion.Services
{
    public class PasswordCracekr : IPasswordCracekr
    {
        public PasswordCracekr()
        {
            //todo : dependency Injection
            var r = PhoneBook.Instance;
        }
        public PhoneNumber CrackPassword(string hashPassword, int startRange, int endRange)
        {
            PhoneNumber phoneNumber = new() { HashedNumber = hashPassword, FullNumber = string.Empty };

            var result = CrackWithRanges(hashPassword, startRange, endRange);
            if(result != string.Empty)
            {
                phoneNumber.FullNumber = result;
            }
            return phoneNumber;
        }
        public string CrackWithRanges(string hashPassword, int startRange, int endRange)
        {
            var md5Password = string.Empty;
            if (PhoneBook.Passwords.ContainsKey(hashPassword))
            {
                return PhoneBook.Passwords[hashPassword];
            }
            int divition = (int)Math.Pow(10, 7);
            for (int i = startRange; i < endRange; i++)
            {
                var profix = i / divition;
                var num = i % divition;
                var phone = "05" + profix + '-' + string.Format("{0:D7}", num);
                md5Password = MD5Hash(phone);
                PhoneBook.Passwords.Add(md5Password, phone);
                if (md5Password == hashPassword)
                    return phone;
            }

            return string.Empty;
        }
        public string MD5Hash(string number)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(number));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
