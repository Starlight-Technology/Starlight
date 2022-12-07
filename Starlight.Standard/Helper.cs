using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Starlight.Standard
{
    public interface IHelper
    {
        Task<bool> ContainsSpecialCharacter(string value);
        Task<string> EncryptPass256(string pass);
        Task<string> EncryptPass256(string pass, string salt = null);
        Task<string> EncryptPass256(string pass, string salt = null, bool isCaseSensitive = false, int? maxLength = null, int? minLength = null);
        Task<bool> IsValidEmail(string value);
        Task<bool> IsValidString(string value);
        Task<bool> IsValidString(string value, int? maxLength = null, int? minLength = null);
    }

    public class Helper : IHelper
    {
        /// <summary>
        /// Verify if a string contains special characters like "./;,#@$$%¨&*
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> ContainsSpecialCharacter(string value)
        {
            if (value.AsParallel().Any(c => !char.IsLetterOrDigit(c)))
                return Task.FromResult(true);

            return Task.FromResult(false);
        }

        /// <summary>
        /// Encrypt a password using SHA256
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public async Task<string> EncryptPass256(string pass) => await EncryptPass256(pass, null);

        /// <summary>
        /// Encrypt a password using SHA256 with a salt to avoid two or more passwords with the same hash
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> EncryptPass256(string pass, string salt = null)
        {
            pass = pass.Trim();

            if (!await IsValidString(pass))
                throw new Exception("Password can't be null or empty.");

            if (await IsValidString(salt))
            {
                byte[] s = Encoding.UTF8.GetBytes(salt);
                byte[] senhaByte = Encoding.UTF8.GetBytes(pass);
                byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(s).ToArray());
                return Convert.ToBase64String(sha256);
            }

            else
            {
                byte[] senhaByte = Encoding.UTF8.GetBytes(pass);
                byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.ToArray());
                return Convert.ToBase64String(sha256);
            }
        }

        /// <summary>
        /// Encrypt a password using SHA256 with a salt to avoid two or more passwords with the same hash
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="salt"></param>
        /// <param name="isCaseSensitive"></param>
        /// <param name="maxLength"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> EncryptPass256(string pass, string salt = null, bool isCaseSensitive = false, int? maxLength = null, int? minLength = null)
        {
            if (!isCaseSensitive)
                pass = pass.ToLower();
            if (await IsValidString(pass, maxLength, minLength))
                await EncryptPass256(pass, salt);

            throw new Exception("Password can't be null or empty.");
        }

        /// <summary>
        /// Verify if a string is null, empty or a white space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> IsValidString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        /// <summary>
        /// Verify if a string is null, empty or a white space, and have the max and min length especified
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="minLength"></param>
        /// <returns></returns>
        public Task<bool> IsValidString(string value, int? maxLength = null, int? minLength = null)
        {
            if (minLength.HasValue && value.Length >= minLength && maxLength.HasValue && value.Length <= maxLength)
                return IsValidString(value);
            else if (minLength.HasValue && value.Length >= minLength)
                return IsValidString(value);
            else if (maxLength.HasValue && value.Length <= maxLength)
                return IsValidString(value);

            return Task.FromResult(false);
        }


        /// <summary>
        /// Verify if the string is an email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> IsValidEmail(string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return Task.FromResult(addr.Address == value);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

    }
}
