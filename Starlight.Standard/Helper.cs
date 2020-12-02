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
        Task<bool> IsValidString(string value, int? maxLength = null, int? MinLength = null);
    }

    public class Helper : IHelper
    {
        public async Task<bool> ContainsSpecialCharacter(string value)
        {
            if (value.AsParallel().Any(c => !char.IsLetterOrDigit(c)))
                return true;

            return false;
        }

        public async Task<string> EncryptPass256(string pass) => await EncryptPass256(pass, null);

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

        public async Task<string> EncryptPass256(string pass, string salt = null, bool isCaseSensitive = false, int? maxLength = null, int? minLength = null)
        {
            if (!isCaseSensitive)
                pass = pass.ToLower();
            if (await IsValidString(pass, maxLength, minLength))
                await EncryptPass256(pass, salt);

            throw new Exception("Password can't be null or empty.");
        }

        public Task<bool> IsValidString(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public Task<bool> IsValidString(string value, int? maxLength = null, int? MinLength = null) => MinLength.HasValue
                                                                                     && maxLength.HasValue
                                                                                     && maxLength <= value.Length
                                                                                     && MinLength >= value.Length
                                                                                     || (maxLength.HasValue && maxLength <= value.Length)
                                                                                     || (MinLength.HasValue && MinLength >= value.Length)
                ? IsValidString(value)
                : Task.FromResult(false);

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
