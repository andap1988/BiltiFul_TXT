using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Pesquisa
    {
        public Arquivos Arquivos { get; set; }

        public Pesquisa() { Arquivos = new Arquivos(); }

        public void Menu()
        {
            int option = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Menu Pesquisa ::::.....\n");
                Console.WriteLine(" 1 - Pesquisar Cliente");
                Console.WriteLine(" 2 - Pesquisar Fornecedor");
                Console.WriteLine(" 3 - Pesquisar Produto");
                Console.WriteLine(" 4 - Pesquisar Materia-prima");
                Console.WriteLine(" 5 - Pesquisar Producao");
                Console.WriteLine(" 6 - Pesquisar Compras");
                Console.WriteLine(" 7 - Pesquisar Vendas");
                Console.WriteLine(" ---------------------------");
                Console.WriteLine($" 9 - Sair\n");
                Console.Write("\n Escolha: ");
                int.TryParse(Console.ReadLine(), out option);

                if (option == 9)
                {
                    flag = false;
                }
                else if ((option < 1) || (option > 3))
                {
                    Console.WriteLine("\n Opcao invalida.");
                    Console.WriteLine("\n Pressione ENTER para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    switch (option)
                    {
                        case 1:
                            PesquisarCliente();
                            break;
                        case 2:
                            PesquisarFornecedor();
                            break;
                        case 3:
                            PesquisarProduto();
                            break;
                        case 4:
                            PesquisarMateriaPrima();
                            break;
                        case 5:
                            PesquisarProducao();
                            break;
                        case 6:
                            PesquisarCompras();
                            break;
                        case 7:
                            PesquisarVendas();
                            break;
                    }
                }
            } while (flag);
        }

        public void PesquisarCliente()
        {
            string msgInicial, msgSaida, escolha, linhaEncontrada, nome, cpf;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Cliente))
            {
                Console.WriteLine("\n xxxx Nao ha clientes para pesquisar.");
                Console.WriteLine("\n Pressione ENTER para voltar...");
                Console.ReadKey();
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine(" Pesquisar por:\n");
                    Console.WriteLine(" 1 - Nome");
                    Console.WriteLine(" 2 - CPF");
                    Console.Write("\n Escolha: ");
                    escolha = Console.ReadLine();

                    if (escolha != "1" && escolha != "2" && escolha != "9")
                    {
                        Console.WriteLine("\n xxxx Digite apenas '1' ou '2'.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                        Console.ReadKey();
                    }
                    else if (escolha == "9")
                        return;
                    else if (escolha == "1")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.Write(" Digite o Nome: ");
                            nome = Console.ReadLine().PadLeft(50, ' ');

                            if (nome == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, nome, 11, 50)))
                                {
                                    Console.WriteLine("\n xxxx O nome nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, nome, 11, 50);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine($" CPF:              {linhaEncontrada.Substring(0, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-")}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(11, 50).Trim()}");
                                    Console.WriteLine($" Data de Nasc:     {linhaEncontrada.Substring(61, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Sexo:             {(linhaEncontrada.Substring(69, 1) == "M" ? "Masculino" : "Feminino")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(70, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(78, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(86, 1) == "A" ? "Ativo" : "Inadimplente")}");

                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadLine();
                                    flagInterna = false;
                                    flagPrincipal = false;
                                }
                            }
                        } while (flagInterna);
                    }
                    else if (escolha == "2")
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.Write(" Digite o CPF: ");
                            cpf = Console.ReadLine();

                            if (cpf == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                                {
                                    Console.WriteLine("\n xxxx O CPF nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else 
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine($" CPF:              {linhaEncontrada.Substring(0, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-")}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(11, 50).Trim()}");
                                    Console.WriteLine($" Data de Nasc:     {linhaEncontrada.Substring(61, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Sexo:             {(linhaEncontrada.Substring(69, 1) == "M" ? "Masculino" : "Feminino")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(70, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(78, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(86, 1) == "A" ? "Ativo" : "Inadimplente")}");

                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadLine();
                                    flagInterna = false;
                                    flagPrincipal = false;
                                }
                            }
                        } while (flagInterna);
                    }
                } while (flagPrincipal);
            }
        }
        public void PesquisarFornecedor()
        {

        }
        public void PesquisarProduto()
        {

        }
        public void PesquisarMateriaPrima()
        {

        }
        public void PesquisarProducao()
        {

        }
        public void PesquisarCompras()
        {

        }
        public void PesquisarVendas()
        {

        }
    }
}
