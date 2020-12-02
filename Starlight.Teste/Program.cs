using Starlight.Standard;

using System;

namespace Starlight.Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();

            program.IniciarTeste();
        }

        public async void IniciarTeste()
        {
            Helper helper = new Helper();
            string teste = "";

            Console.WriteLine("teste helper");

            Console.WriteLine("teste special character");
            bool special = await helper.ContainsSpecialCharacter(teste);
            Console.WriteLine(special);

            Console.WriteLine("teste encriptar senha");
            var senha = await helper.EncryptPass256(teste, null);
            Console.Write(senha);
        }
    }
}
