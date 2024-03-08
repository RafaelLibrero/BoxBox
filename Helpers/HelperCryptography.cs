using System.Security.Cryptography;
using System.Text;

namespace BoxBox.Helpers
{
    public class HelperCryptography
    { 
        public static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(1, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static string EncriptarContenido
            (string contenido, string salt)
        {
            string contenidoSalt = contenido + salt;
            
            SHA256 sHA256 = SHA256.Create();
            byte[] salida;
            UnicodeEncoding encoding = new UnicodeEncoding();
            
            salida = encoding.GetBytes(contenidoSalt);
            
            for (int i = 1; i <= 33; i++)
            {
                //REALIZAMOS CIFRADO SOBRE CIFRADO
                salida = sHA256.ComputeHash(salida);
            }
            sHA256.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}