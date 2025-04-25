using System.Text;

using XSystem.Security.Cryptography;

namespace Starlight.TestProject.Helper
{
    public class HelperTest
    {
        [Fact]
        public async Task ContainsSpecialCharacter_False()
        {
            var helper = new Standard.Helper();
            bool result = await helper.ContainsSpecialCharacter("qwertyuiopasdfghjklzxcvbnm1234567890");

            Assert.False(result, "The sequence of characters cant have a special character");
        }

        [Fact]
        public async Task ContainsSpecialCharacter_True()
        {
            var helper = new Standard.Helper();
            bool result = await helper.ContainsSpecialCharacter("qwertyuiopasdfghjklzxcvbnm1234567890!@#$%$%¨&*()_[]}{;<>");

            Assert.True(result, "The sequence of characters must have a special character");
        }

        [Fact]
        public async Task EncryptPass256_True()
        {
            var helper = new Standard.Helper();
            var pass = "passW0rd123";
            var salt = "salt";
            var pass256 = await EncryptPass256(pass, salt);
            var passHelper256 = await helper.EncryptPass256(pass, salt);

            bool passEncryptedEquals = Equals(pass256, passHelper256);

            Assert.True(passEncryptedEquals);
        }

        [Fact]
        public async Task EncryptPass256_False()
        {
            var helper = new Standard.Helper();
            var pass = "passW0rd123";
            var salt = "salt";
            var passHelper256 = await helper.EncryptPass256(pass, salt);

            bool passEncryptedEquals = Equals(pass, passHelper256);

            Assert.False(passEncryptedEquals);
        }

        [Fact]
        public async Task IsValidString_True()
        {
            var helper = new Standard.Helper();
            var isString = "this is a valid string";
            var stringWithReq = "this string contais 34 characters.";

            var isStringValid = await helper.IsValidString(isString);
            var isStringValid2 = await helper.IsValidString(stringWithReq, 34, 34);

            Assert.True(isStringValid, "the string must be valid!");
            Assert.True(isStringValid2, "the string must be valid! The max and min must be the same!");
        }

        [Fact]
        public async Task IsValidString_False()
        {
            var helper = new Standard.Helper();
            var isString = " ";
            var stringWithReq = "this string contais 34 characters.";

            var isStringValid = await helper.IsValidString(isString);
            var isStringValid2 = await helper.IsValidString(stringWithReq, 1, 100);

            Assert.False(isStringValid, "the string must be invalid!");
            Assert.False(isStringValid2, "the string must be invalid! The min and Max characters must be invalid.");
        }

        [Fact]
        public async Task IsValidEmail_True()
        {
            var helper = new Standard.Helper();
            var email = "email@teste.com";
            var isValidEmail = await helper.IsValidEmail(email);

            Assert.True(isValidEmail, "Email must be valid.");
        }

        [Fact]
        public async Task IsValidEmail_False()
        {
            var helper = new Standard.Helper();
            var email = "emailtestecom";
            var isValidEmail = await helper.IsValidEmail(email);

            Assert.False(isValidEmail, "Email must be invalid.");
        }

        private async Task<string> EncryptPass256(string pass, string salt = null)
        {
            pass = pass.Trim();

            byte[] s = Encoding.UTF8.GetBytes(salt);
            byte[] senhaByte = Encoding.UTF8.GetBytes(pass);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(s).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
