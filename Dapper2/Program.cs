﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper2
{
    class Program
    {
        static string strConexao = ConfigurationManager.ConnectionStrings["AcaoWeb"].ConnectionString;
        static void Main(string[] args)
        {

            Acao();
            Console.ReadKey();
        }

        private static void Acao()
        {
            using (var conn = new SqlConnection(strConexao))
            {

                var add = new Insert();
                {

                    Console.Write("Digite seu Nome: ");
                    add.Nome = Console.ReadLine();
                    Console.Write("Seu CPF: ");
                    add.Cpf = Console.ReadLine();
                    Console.Write("Seu Numero do Celular: ");
                    add.Numero = Console.ReadLine();
                    Console.Write("Sexo (M por Homem e F por Mulher): ");
                    add.Sexo = char.Parse(Console.ReadLine());
                    Console.Write("Voce é Adulto S por sim e N por não: ");
                    string adt = Console.ReadLine();

                    if (adt == "s" || adt == "S")
                    {
                        add.Adulto = true;
                    }
                    else if (adt == "n" || adt == "N")
                    {
                        add.Adulto = false;
                    }
                    else
                    {
                        Console.WriteLine("404");
                    }
                };
                conn.Execute(@"insert into AcaoWeb(Nome,Cpf,Numero,Sexo,Adulto)values(@Nome, @Cpf, @Numero,@Sexo,@Adulto)", add);
                Console.WriteLine("Cadastro realizado com sucesso\n*******************************");
                var resultado = conn.Query("select * from AcaoWeb");

                Console.WriteLine("{0}, {1},{2},{3}", " Id " , " Nome " , " Cpf " , " Numero ", " Sexo ", " Adulto ");
                foreach (var pessoa in resultado)
                {
                    Console.WriteLine("{0} - {1} - {2} - {3}", pessoa.Id, pessoa.Nome, pessoa.Cpf, pessoa.Numero, pessoa.sexo, pessoa.Adulto);
                }
                var atualizarBD = @"Update BancoAno..AcaoWeb Set Nome = @Nome
                                   Where Id = @Id";
                Console.Write("Digite o Nome que queria mudar: ");
                conn.Execute(atualizarBD, new
                {

                    Nome = Console.ReadLine(),
                    id = 21
                }) ;
                var alteracao = conn.Query("select * from AcaoWeb");

                Console.WriteLine("{0}, {1},{2},{3}", " Id ", " Nome ", " Cpf ", " Numero ", " Sexo ", " Adulto ");
                foreach (var pessoa in alteracao)
                {
                    Console.WriteLine("{0} - {1} - {2} - {3}", pessoa.Id, pessoa.Nome, pessoa.Cpf, pessoa.Numero, pessoa.Sexo, pessoa.Adulto);
                }
            }
        }

    }

}
