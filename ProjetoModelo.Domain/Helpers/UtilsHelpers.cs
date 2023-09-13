using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ProjetoModelo.Domain.Helpers
{
    public static class UtilsHelper
    {

        public static string GetFirstName(string name)
        {
            return name.Split(' ')[0];
        }

        public static string AdicionarPrefixoTelefone(string numeroTelefone)
        {
            if (!numeroTelefone.StartsWith("55"))
            {
                return "55" + numeroTelefone;
            }

            return numeroTelefone;
        }

        public static string FirstCharToUpper(this string input)
        {
            if (!String.IsNullOrEmpty(input))
                return input.First().ToString().ToUpper() + input.Substring(1);
            else
                return input;
        }

        public static string CreateMD5(string input)
        {
            try
            {
                // Use input string to calculate MD5 hash
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool IsValid(this string str)
        {
            return ((!String.IsNullOrEmpty(str)) && (!String.IsNullOrWhiteSpace(str)));
        }

        public static bool ValidarUrlImagem(string url)
        {
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public static int RandomNumber()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public static string GetSha256Hash(string value)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result) sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static int ValidInt(this string str)
        {
            try
            {
                int retorno = 0;
                if (str.IsValid())
                {
                    int.TryParse(str, out retorno);
                }
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static bool CpfIsValid(string cpf)
        {
            try
            {

                cpf = cpf.Replace(".", "").Replace("-", "");

                if (new string(cpf[0], cpf.Length) == cpf)
                {
                    return false;
                }

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DataNascimentoIsValid(DateTime? data)
        {
            if (data == null) return false;

            DateTime validateData;

            try
            {
                validateData = Convert.ToDateTime(data);
            }
            catch
            {
                return false;
            }

            if (validateData.Year <= 1900 || validateData >= DateTime.Today) return false;

            return true;
        }

        public static string MountURL(string folder, string imageName)
        {
            var url = "";
            return $"{url}/{folder}/{imageName}";
        }

        public static bool IdadeIsValid(DateTime? dataNascimento, int? idadeMinima)
        {
            if (dataNascimento == null) return false;
            if (idadeMinima == null) return true;

            return dataNascimento.Value.AddYears(idadeMinima.Value) <= DateTime.Now;
        }

        public static string GetMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "JAN";
                case 2:
                    return "FEV";
                case 3:
                    return "MAR";
                case 4:
                    return "ABR";
                case 5:
                    return "MAI";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AGO";
                case 9:
                    return "SET";
                case 10:
                    return "OUT";
                case 11:
                    return "NOV";
                case 12:
                    return "DEZ";
                default:
                    return month.ToString();
            }
        }

        public static string GetHostName(HttpContext httpContext)
        {
            string hostName = "";

            try
            {

                IPAddress address = httpContext.Request.HttpContext.Connection.RemoteIpAddress;
                IPHostEntry _host = Dns.GetHostEntry(address);

                hostName = (_host != null ? _host.HostName : " IPHostEntry não identificado");
            }
            catch (Exception ex)
            {
                hostName = ex.Message;
            }

            return hostName;
        }

        public static string GenerateRandomNumbers(int quantity)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < quantity; i++)
            {
                int randomNumber = random.Next(0, 10);
                sb.Append(randomNumber);
            }

            return sb.ToString();
        }

        public static string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
