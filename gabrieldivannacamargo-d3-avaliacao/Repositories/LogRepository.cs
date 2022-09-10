using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaliacaoD3.Interfaces;
using AvaliacaoD3.Models;

namespace AvaliacaoD3.Repositories
{
    internal class LogRepository : ILog
    {
        private const string path = "database/log.txt";
        private const string connectionString = "Server=labsoft.pcs.usp.br; Initial Catalog=db_5; User id=usuario_5; pwd=44323442882;";
        //private FileStream _fileStream = File.OpenWrite(path);
        
        public LogRepository(){
            CreateFolderFile(path);
        }

        public Users SearchUser(string email, string password)
        {
            using (SqlConnection connection = new(connectionString))
            {
                //string querySelectUser = "SELECT UserID, Name, Email, Password FROM Users WHERE @Email = Email";
                string queryOpenDecryption = "OPEN SYMMETRIC KEY MinhaCHave DECRYPTION BY CERTIFICATE MeuCertificado WITH PASSWORD = 'Certificado'";
                string querySelectUser = "SELECT *, SenhaDescriptografada = CAST (DECRYPTBYKEY([Password]) AS VARCHAR(255)) FROM Users WHERE @Email = Email";
                string queryCloseDecryption = "CLOSE SYMMETRIC KEY MinhaChave";

                SqlCommand OpenCommand = new(queryOpenDecryption, connection);
                SqlCommand SelectCommand = new(querySelectUser, connection);
                SqlCommand CloseCommand = new(queryCloseDecryption, connection);

                SqlDataReader reader;

                SelectCommand.Parameters.AddWithValue("@Email", email);
                connection.Open();
                OpenCommand.ExecuteNonQuery();

                Users findedUser = new Users();
                reader = SelectCommand.ExecuteReader();

                if (reader.Read())
                {
                    if(reader[5].ToString() != password){
                          return findedUser;
                    }
                    findedUser.UserId = reader[0].ToString();
                    findedUser.UserName = reader[1].ToString();
                    findedUser.UserJob = reader[2].ToString();
                    findedUser.Email = reader[3].ToString();
                    //Senha descriptografada
                    findedUser.UserPassword = reader[5].ToString();
                }
                return findedUser;
            };
        }


        public Users LoginUser(string email, string password)
        {
            Users user = SearchUser(email, password);

            if(user.UserId != string.Empty)
            {
                RegisterAcess(user);
            }
            return user;
        }

        private static string PrepareLoginLine(Users user)
        {
            return $"O usuário {user.UserName} ({user.UserId}) - {user.UserJob} - acessou o sistema as {DateTimeOffset.Now}\n";
        }

        private static string PrepareLogoutLine(Users user){
            return $"O usuário {user.UserName} ({user.UserId}) - {user.UserJob} - deslogou do sistema as {DateTimeOffset.Now}\n";
        }

        public static void CreateFolderFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public void RegisterAcess(Users user)
        {
            //Console.WriteLine(user.UserName);
            string line = PrepareLoginLine(user);
            byte[] info = new UTF8Encoding(true).GetBytes(line);
            using(FileStream file = File.OpenWrite(path)){
                file.Seek(0, SeekOrigin.End);
                file.Write(info);
            };
        }

        public void RegisterLogout(Users user){
            string line = PrepareLogoutLine(user);
            byte[] info = new UTF8Encoding(true).GetBytes(line);
            using(FileStream file = File.OpenWrite(path)){
                file.Seek(0, SeekOrigin.End);
                file.Write(info);
            };
        }

    }
}
