using AvaliacaoD3.Models;
using AvaliacaoD3.Repositories;
using AvaliacaoD3;
using System.Runtime.CompilerServices;

namespace AvaliacaoD3
{
    internal class Program
    {
        private const string path = "database/log.txt";
        static void Main(string[] args)
        {
            string option = string.Empty;

            //FileStream file = File.OpenWrite(path);
            LogRepository _log = new();
            Users _user = new();

            do
            {
                Screens.MainScreen();
                Console.Write("Sua escolha: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        string[] UserData = Screens.LoginScreen();
                        //Email e senha:
                        _user = _log.LoginUser(UserData[0], UserData[1]);
                        
                        if (_user.UserName != string.Empty)
                        {
                            Console.WriteLine("Logado com sucesso!");
                            Screens.LoggedScreen();
                            option = Console.ReadLine();
                            _log.RegisterLogout(_user);
                            if(option == "0"){
                                Console.WriteLine("\nEncerrando aplicação...");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Falha ao logar, dados inválidos!");
                        }
                        break;
                    case "0":
                        Console.WriteLine("\nEncerrando aplicação....");
                        break;
                }
            } while (option != "0");
        }
    }
}