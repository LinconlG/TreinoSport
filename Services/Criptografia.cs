using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Services {
    public static class Criptografia {

        public static string sha256_hash(string value) {
            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create()) {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public static bool ValidarEmail(string email) {
            try {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException) {
                return false;
            }
        }
    }
}
