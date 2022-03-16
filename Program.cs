using System;
using System.Globalization;

namespace BiltiFulBD
{
    internal class Program
    {
        public static int Menus(string name, string[] options)
        {
            int option = 0, i = 1;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine($"\n .....:::: {name} ::::.....\n");
                foreach (string item in options)
                {
                    Console.WriteLine($" {i} - {item}");
                    i++;
                }

                Console.WriteLine("------------------------");
                Console.WriteLine($" 9 - Sair\n");
                Console.Write(" Escolha: ");
                int.TryParse(Console.ReadLine(), out option);

                if (option == 9)
                {
                    flag = false;
                }
                else if ((option < 1) || (option > 4))
                {
                    Console.WriteLine("\n Opcao invalida.\n");
                    Console.WriteLine(" Pressione ENTER para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    flag = false;
                }

            } while (flag);

            return option;
        }
        /*{
            int option = 0;
            string choice;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Biltiful Menu ::::.....\n");
                Console.WriteLine(" 1 - Cadastros");
                Console.WriteLine(" 2 - Producao");
                Console.WriteLine(" 3 - Compras");
                Console.WriteLine(" 4 - Vendas");
                Console.WriteLine("------------------------");
                Console.WriteLine(" 9 - Sair\n");
                Console.Write(" Escolha: ");
                int.TryParse(Console.ReadLine(), out option);

                if (option == 9)
                {
                    flag = false;
                }
                else if ((option < 1) || (option > 4))
                {
                    Console.WriteLine("\n Opcao invalida.\n");
                    Console.WriteLine(" Pressione ENTER para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    flag = false;
                }

            } while (flag);
            return option;*/

        static void Main(string[] args)
        {
            var cultureInformation = new CultureInfo("pt-BR");
            cultureInformation.NumberFormat.CurrencySymbol = "R$";
            CultureInfo.DefaultThreadCurrentCulture = cultureInformation;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInformation;

            Arquivos Arquivos = new();
            Arquivos.GerarArquivos();

            bool flag = true, subFlag = true;
            int option = Menus("Menu Biltiful", new string[] { "Cadastros", "Producao", "Compras", "Vendas", "Pesquisas" });
            int subOption;

            do
            {
                switch (option)
                {
                    case 1:
                        subOption = Menus("Menu Cadastros", new string[] { "Clientes", "Fornecedores", "Produtos", "Materias-primas" });
                        do
                        {
                            switch (subOption)
                            {
                                case 1:
                                    //Cliente
                                    Cliente cliente = new();
                                    cliente.Menu();
                                    subOption = Menus("Menu Cadastros", new string[] { "Clientes", "Fornecedores", "Produtos", "Materias-primas" });
                                    break;
                                case 2:
                                    // Fornecedor
                                    Fornecedor fornecedor = new();
                                    fornecedor.Menu();
                                    subOption = Menus("Menu Cadastros", new string[] { "Clientes", "Fornecedores", "Produtos", "Materias-primas" });
                                    break;
                                case 3:
                                    //Produtos
                                    Produto produto = new();
                                    produto.Menu();
                                    subOption = Menus("Menu Cadastros", new string[] { "Clientes", "Fornecedores", "Produtos", "Materias-primas" });
                                    break;
                                case 4:
                                    // Materia-prima
                                    MPrima mpprima = new();
                                    mpprima.Menu();
                                    subOption = Menus("Menu Cadastros", new string[] { "Clientes", "Fornecedores", "Produtos", "Materias-primas" });
                                    break;
                                case 9:
                                    subFlag = false;
                                    break;
                            }
                        } while (subFlag);
                        option = Menus("Biltiful", new string[] { "Cadastros", "Producao", "Compras", "Vendas" });
                        break;
                    case 2:
                        Producao producao = new();
                        producao.Menu();
                        option = Menus("Biltiful", new string[] { "Cadastros", "Producao", "Compras", "Vendas" });
                        break;
                    case 3:
                        Compra compra = new();
                        compra.Menu();
                        option = Menus("Biltiful", new string[] { "Cadastros", "Producao", "Compras", "Vendas" });
                        break;
                    case 4:
                        Venda venda = new();
                        venda.Menu();
                        option = Menus("Biltiful", new string[] { "Cadastros", "Producao", "Compras", "Vendas" });
                        break;
                    case 9:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\n Opcao invalida.\n");
                        Console.WriteLine(" Pressione ENTER para voltar...");
                        Console.ReadKey();
                        //option = Menu();
                        break;
                }
            } while (flag);

            Console.Clear();
            Console.WriteLine("\n\n\t\t\t BILTIFUL agradece a preferencia.");
            Console.WriteLine("\n\n\t\t\t\t Volte sempre!");
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");

        }
    }
}
