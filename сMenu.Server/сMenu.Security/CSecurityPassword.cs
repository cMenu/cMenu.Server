using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace cMenu.Security
{
    public class CSecurityPassword
    {
        private byte[] _salt;
        private byte[] _hash;

        public string Salt
        {
            get { return Convert.ToBase64String(_salt); }
        }
        public string Hash
        {
            get { return Convert.ToBase64String(_hash); }
        }
        public byte[] RawSalt
        {
            get { return (byte[])_salt.Clone(); }
        }
        public byte[] RawHash
        {
            get { return (byte[])_hash.Clone(); }
        }

        public CSecurityPassword(string salt, string hash)
        {
          _salt = Convert.FromBase64String(salt);
          _hash = Convert.FromBase64String(hash);
        }
        public CSecurityPassword(byte[] salt, byte[] hash)
        {
            _salt = (byte[])salt.Clone();
            _hash = (byte[])hash.Clone();
        }
        public CSecurityPassword(char[] clearText)
        {
            _salt = GenerateRandom(6);
            _hash = HashPassword(clearText);
        }

        private byte[] HashPassword(char[] clearText)
        {
            Encoding utf8 = Encoding.UTF8;
            byte[] hash;

            // создаем рабочий массив достаточного размера, чтобы вместить
            byte[] data = new byte[_salt.Length
                        + utf8.GetMaxByteCount(clearText.Length)];

            try
            {
                // копируем синхропосылку в рабочий массив
                Array.Copy(_salt, 0, data, 0, _salt.Length);

                // копируем пароль в рабочий массив, преобразуя его в UTF-8
                int byteCount = utf8.GetBytes(clearText, 0, clearText.Length,
                  data, _salt.Length);

                // хэшируем данные массива
                using (HashAlgorithm alg = new SHA256Managed())
                    hash = alg.ComputeHash(data, 0, _salt.Length + byteCount);
            }
            finally
            {
                // очищаем рабочий массив в конце работы, чтобы избежать
                // утечки открытого пароля
                Array.Clear(data, 0, data.Length);
            }

            return hash;
        }
        private static char[] Generate()
        {
            char[] random = new char[12];

            // генерируем 9 случайных байтов; этого достаточно, чтобы
            // получить 12 случайных символов из набора base64
            byte[] rnd = GenerateRandom(9);

            // конвертируем случайные байты в base64
            Convert.ToBase64CharArray(rnd, 0, rnd.Length, random, 0);

            // очищаем рабочий массив
            Array.Clear(rnd, 0, rnd.Length);

            return random;
        }
        private static byte[] GenerateRandom(int size)
        {
            byte[] random = new byte[size];
            RandomNumberGenerator.Create().GetBytes(random);
            return random;
        }

        public bool Verify(char[] clearText)
        {
            byte[] hash = HashPassword(clearText);

            if (hash.Length == _hash.Length)
            {
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != _hash[i])
                        return false;
                }

                return true;
            }

            return false;
        }

    }
}
