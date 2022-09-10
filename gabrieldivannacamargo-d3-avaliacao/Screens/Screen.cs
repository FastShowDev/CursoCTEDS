namespace AvaliacaoD3{
    //Classe para imprimir as interfaces no console
    public static class Screens{
        public static void MainScreen(){
            Console.WriteLine("\n=========================================================");
            Console.WriteLine("\t\tEscolha uma das opções: \n");
            Console.WriteLine("1 - Acessar servidor");
            Console.WriteLine("0 - Encerrar aplicação");
            Console.WriteLine("\n=========================================================\n");
        }

        public static string[] LoginScreen(){
            string[] userData = new string[2];

            Console.WriteLine("\n=========================================================");
            Console.WriteLine("\t\t\tLogando: \n");
            Console.Write("Informe seu email: ");
            userData[0] = Console.ReadLine();
            Console.Write("Informe sua senha: ");
            userData[1] = Console.ReadLine();
            Console.WriteLine("\n=========================================================\n");

            return userData;
        }

        public static void LoggedScreen(){
            Console.WriteLine("\n=========================================================\n");
            Console.WriteLine("\t\tEscolha uma das opções: \n");
            Console.WriteLine("1 - Deslogar");
            Console.WriteLine("0 - Encerrar aplicação");
            Console.WriteLine("\n=========================================================\n");
            Console.Write("Sua escolha: ");
        }

    }
}