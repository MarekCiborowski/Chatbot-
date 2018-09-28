using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AdminPanel.ViewModels.AuthenticationViewModels
{
    public class UserVM
    {
        [Display(Name = "Użytkownik")]
        [Required(ErrorMessage = "Wymagana nazwa użytkownika")]
        public string userName { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Wymagane hasło")]
        public string password { get; set; }

        public string hashPassword()
        {
            MD5 hash = MD5.Create();

            return GetMd5Hash(hash, password);
        }

        static string GetMd5Hash(MD5 hash, string input)
        {
            // Konwertowanie stringa do tablicy bajtów i wyliczanie hashowania
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Stworzenie StringBuildera do przechowywania bajtów i stworzenie stringa
            StringBuilder sBuilder = new StringBuilder();

            // Każdy bajt jest haszowany i formatowany do postaci ciągu szesnastkowego
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Zwracanie zahaszowanego stringa
            return sBuilder.ToString();
        }
    }
}