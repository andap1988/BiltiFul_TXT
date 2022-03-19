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
                Console.WriteLine(" 0 - Pesquisar Cliente");
                Console.WriteLine(" 1 - Pesquisar Fornecedor");
                Console.WriteLine(" 2 - Pesquisar Produto");
                Console.WriteLine(" 3 - Pesquisar Materia-prima");
                Console.WriteLine(" 4 - Pesquisar Producao");
                Console.WriteLine(" 5 - Pesquisar Compras");
                Console.WriteLine(" 6 - Pesquisar Vendas");
                Console.WriteLine(" 7 - Pesquisar Fornecedor Bloqueado");
                Console.WriteLine(" 8 - Pesquisar Cliente Inadimplente");
                Console.WriteLine(" ---------------------------");
                Console.WriteLine($" 9 - Sair\n");
                Console.Write("\n Escolha: ");
                int.TryParse(Console.ReadLine(), out option);

                if (option == 9)
                {
                    flag = false;
                }
                else if ((option < 0) || (option > 8))
                {
                    Console.WriteLine("\n Opcao invalida.");
                    Console.WriteLine("\n Pressione ENTER para voltar...");
                    Console.ReadKey();
                }
                else
                {
                    switch (option)
                    {
                        case 0:
                            PesquisarCliente();
                            break;
                        case 1:
                            PesquisarFornecedor();
                            break;
                        case 2:
                            PesquisarProduto();
                            break;
                        case 3:
                            PesquisarMateriaPrima();
                            break;
                        case 4:
                            PesquisarProducao();
                            break;
                        case 5:
                            PesquisarCompra();
                            break;
                        case 6:
                            PesquisarVenda();
                            break;
                        case 7:
                            PesquisarBloqueado();
                            break;
                        case 8:
                            PesquisarInadimplente();
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
                    Console.WriteLine(" ..:: Pesquisa de Cliente ::..\n");
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
                            Console.WriteLine(" ..:: Pesquisa de Cliente ::..\n");
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
                                    Console.WriteLine(" ..:: Pesquisa de Cliente ::..\n");
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
                            Console.WriteLine(" ..:: Pesquisa de Cliente ::..\n");
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
                                    Console.WriteLine(" ..:: Pesquisa de Cliente ::..\n");
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
            string msgInicial, msgSaida, escolha, linhaEncontrada, razaoSocial, cnpj;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Cliente))
            {
                Console.WriteLine("\n xxxx Nao ha fornecedores para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Fornecedor ::..\n");
                    Console.WriteLine(" Pesquisar por:\n");
                    Console.WriteLine(" 1 - Razao Social");
                    Console.WriteLine(" 2 - CNPJ");
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
                            Console.WriteLine(" ..:: Pesquisa de Fornecedor ::..\n");
                            Console.Write(" Digite a razao social: ");
                            razaoSocial = Console.ReadLine().PadLeft(50, ' ');

                            if (razaoSocial == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, razaoSocial, 14, 50)))
                                {
                                    Console.WriteLine("\n xxxx A razao social nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, razaoSocial, 14, 50);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Fornecedor ::..\n");
                                    Console.WriteLine($" CNPJ:             {linhaEncontrada.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")}");
                                    Console.WriteLine($" Razao Social:     {linhaEncontrada.Substring(14, 50).Trim()}");
                                    Console.WriteLine($" Data de Abertura: {linhaEncontrada.Substring(64, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(72, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(80, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(88, 1) == "A" ? "Ativo" : "Bloqueado")}");

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
                            Console.WriteLine(" ..:: Pesquisa de Fornecedor ::..\n");
                            Console.Write(" Digite o CNPJ: ");
                            cnpj = Console.ReadLine();
                            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                            if (cnpj == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj, 0, 14)))
                                {
                                    Console.WriteLine("\n xxxx O CNPJ nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj, 0, 14);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Fornecedor ::..\n");
                                    Console.WriteLine($" CNPJ:             {linhaEncontrada.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")}");
                                    Console.WriteLine($" Razao Social:     {linhaEncontrada.Substring(14, 50).Trim()}");
                                    Console.WriteLine($" Data de Abertura: {linhaEncontrada.Substring(64, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(72, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(80, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(88, 1) == "A" ? "Ativo" : "Bloqueado")}");

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
        public void PesquisarProduto()
        {
            string msgInicial, msgSaida, escolha, linhaEncontrada, nomeProd, codProd;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha produtos para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Produto ::..\n");
                    Console.WriteLine(" Pesquisar por:\n");
                    Console.WriteLine(" 1 - Nome");
                    Console.WriteLine(" 2 - Codigo");
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
                            Console.WriteLine(" ..:: Pesquisa de Produto ::..\n");
                            Console.Write(" Digite a nome: ");
                            nomeProd = Console.ReadLine().PadLeft(20, ' ');

                            if (nomeProd == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Produto, nomeProd, 13, 20)))
                                {
                                    Console.WriteLine("\n xxxx O produto nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, nomeProd, 13, 20);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Produto ::..\n");
                                    Console.WriteLine($" Produto nº:       {linhaEncontrada.Substring(0, 13)}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(13, 20).Trim()}");
                                    Console.WriteLine($" Valor de Venda:   {linhaEncontrada.Substring(33, 5).Insert(3, ",")}");
                                    Console.WriteLine($" Utima Venda:      {linhaEncontrada.Substring(38, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(46, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(54, 1) == "A" ? "Ativo" : "Inativo")}");

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
                            Console.WriteLine(" ..:: Pesquisa de Produto ::..\n");
                            Console.Write(" Digite o codigo: ");
                            codProd = Console.ReadLine();

                            if (codProd == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Produto, codProd, 0, 13)))
                                {
                                    Console.WriteLine("\n xxxx O codigo nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, codProd, 0, 13);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Produto ::..\n");
                                    Console.WriteLine($" Produto nº:       {linhaEncontrada.Substring(0, 13)}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(13, 20).Trim()}");
                                    Console.WriteLine($" Valor de Venda:   {linhaEncontrada.Substring(33, 5).Insert(3, ",")}");
                                    Console.WriteLine($" Utima Venda:      {linhaEncontrada.Substring(38, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(46, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(54, 1) == "A" ? "Ativo" : "Inativo")}");

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
        public void PesquisarMateriaPrima()
        {
            string msgInicial, msgSaida, escolha, linhaEncontrada, nomeMPrima, codMPrima;
            bool flagInterna = true, flagPrincipal = true;
            int idMPrima = 0;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha materias-primas para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Materia-prima ::..\n");
                    Console.WriteLine(" Pesquisar por:\n");
                    Console.WriteLine(" 1 - Nome");
                    Console.WriteLine(" 2 - Codigo");
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
                            Console.WriteLine(" ..:: Pesquisa de Materia-prima ::..\n");
                            Console.Write(" Digite a nome: ");
                            nomeMPrima = Console.ReadLine().PadLeft(20, ' ');

                            if (nomeMPrima == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, nomeMPrima, 6, 20)))
                                {
                                    Console.WriteLine("\n xxxx A materia-prima nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, nomeMPrima, 6, 20);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Materia-prima ::..\n");
                                    Console.WriteLine($" Materia-prima nº: {linhaEncontrada.Substring(0, 6)}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(6, 20).Trim()}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(26, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(34, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(42, 1) == "A" ? "Ativo" : "Inativo")}");

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
                            Console.WriteLine(" ..:: Pesquisa de Materia-prima ::..\n");
                            Console.Write(" Digite o codigo: ");
                            codMPrima = Console.ReadLine();

                            if (codMPrima.Contains("MP"))
                                idMPrima = int.Parse(codMPrima.Remove(0, 2));

                            idMPrima = int.Parse(codMPrima);
                            flagInterna = true;

                            linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, "MP" + idMPrima.ToString("0000"), 0, 6);

                            if (codMPrima == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(linhaEncontrada))
                                {
                                    Console.WriteLine("\n xxxx O codigo nao existe.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Materia-prima ::..\n");
                                    Console.WriteLine($" Materia-prima nº: {linhaEncontrada.Substring(0, 6)}");
                                    Console.WriteLine($" Nome:             {linhaEncontrada.Substring(6, 20).Trim()}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(26, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(34, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(42, 1) == "A" ? "Ativo" : "Inativo")}");

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
        public void PesquisarProducao()
        {
            string msgInicial, msgSaida, linhaEncontrada, codProd;
            bool flagPrincipal = true;
            List<string> itensProducao = null;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha producao para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Producao ::..\n");
                    Console.Write(" Digite o codigo: ");
                    codProd = Console.ReadLine().PadLeft(5, '0');

                    if (codProd == "9")
                        return;
                    else
                    {
                        if (string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Producao, codProd, 0, 5)))
                        {
                            Console.WriteLine("\n xxxx A producao nao existe.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Producao, codProd, 0, 5);

                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine(" ..:: Pesquisa de Producao ::..\n");
                            Console.WriteLine($" Producao nº:       {linhaEncontrada.Substring(0, 5)}");
                            Console.WriteLine($" Data da Producao:  {linhaEncontrada.Substring(5, 8).Insert(2, "/").Insert(5, "/")}");
                            Console.WriteLine($" Codigo do Produto: {linhaEncontrada.Substring(13, 13)}");
                            Console.WriteLine($" Qt. do Produto:    {linhaEncontrada.Substring(26, 5).Insert(3, ","):###.#0}");

                            itensProducao = Arquivos.MontarLista(Arquivos.ItemProducao, linhaEncontrada.Substring(0, 5), 0, 5, true);

                            Console.WriteLine("\n Cod.       Materia-prima            Qt.  ");
                            Console.WriteLine(" -------------------------------------------\n");
                            itensProducao.ForEach(item =>
                            {
                                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.Substring(13, 6), 0, 6);
                                string nome = linha.Substring(6, 20).Trim();
                                Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')} {item.Substring(19, 5).Insert(3, ","),8:##0.#0}");
                            });

                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadLine();
                            flagPrincipal = false;
                        }
                    }
                } while (flagPrincipal);
            }
        }
        public void PesquisarCompra()
        {
            string msgInicial, msgSaida, linhaEncontrada, codCompra;
            bool flagPrincipal = true;
            List<string> itensCompra = null;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Compra))
            {
                Console.WriteLine("\n xxxx Nao ha compras para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Compra ::..\n");
                    Console.Write(" Digite o codigo: ");
                    codCompra = Console.ReadLine().PadLeft(5, '0');

                    if (codCompra == "9")
                        return;
                    else
                    {
                        if (string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Compra, codCompra, 0, 5)))
                        {
                            Console.WriteLine("\n xxxx A compra nao existe.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Compra, codCompra, 0, 5);

                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine(" ..:: Pesquisa de Compra ::..\n");
                            Console.WriteLine($" Compra nº:       {linhaEncontrada.Substring(0, 5)}");
                            Console.WriteLine($" Data da Compra:  {linhaEncontrada.Substring(5, 8).Insert(2, "/").Insert(5, "/")}");
                            Console.WriteLine($" CNPJ:            {linhaEncontrada.Substring(13, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")}");
                            Console.WriteLine($" Valor da Compra: {linhaEncontrada.Substring(27, 6).Insert(1, ".").Insert(5, ","):#,##0.#0}");

                            itensCompra = Arquivos.MontarLista(Arquivos.ItemCompra, linhaEncontrada.Substring(0, 5), 0, 5, true);

                            Console.WriteLine("\n Cod.       Materia-prima           V. Unitario         Qt.         Total");
                            Console.WriteLine(" ---------------------------------------------------------------------------\n");
                            itensCompra.ForEach(item =>
                            {
                                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.Substring(13, 6), 0, 6);
                                string nome = linha.Substring(6, 20).Trim();
                                Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')}     {item.Substring(24, 5).Insert(3, ","),8:##0.#0}      {item.Substring(19, 5).Insert(3, ","),8:##0.#0}       {item.Substring(29, 6).Insert(1, ".").Insert(5, ","),8:#,##0.#0}");
                            });

                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadLine();
                            flagPrincipal = false;
                        }
                    }
                } while (flagPrincipal);
            }
        }
        public void PesquisarVenda()
        {
            string msgInicial, msgSaida, linhaEncontrada, codVenda;
            bool flagPrincipal = true;
            List<string> itensVenda = null;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Compra))
            {
                Console.WriteLine("\n xxxx Nao ha vendas para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Venda ::..\n");
                    Console.Write(" Digite o codigo: ");
                    codVenda = Console.ReadLine().PadLeft(5, '0');

                    if (codVenda == "9")
                        return;
                    else
                    {
                        if (string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Venda, codVenda, 0, 5)))
                        {
                            Console.WriteLine("\n xxxx A venda nao existe.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Venda, codVenda, 0, 5);

                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine(" ..:: Pesquisa de Venda ::..\n");
                            Console.WriteLine($" Venda nº:       {linhaEncontrada.Substring(0, 5)}");
                            Console.WriteLine($" Data da Venda:  {linhaEncontrada.Substring(5, 8).Insert(2, "/").Insert(5, "/")}");
                            Console.WriteLine($" CPF:            {linhaEncontrada.Substring(13, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-")}");
                            Console.WriteLine($" Valor da Venda: {linhaEncontrada.Substring(24, 6).Insert(1, ".").Insert(5, ","):#,##0.#0}");

                            itensVenda = Arquivos.MontarLista(Arquivos.ItemVenda, linhaEncontrada.Substring(0, 5), 0, 5, true);

                            Console.WriteLine("\n Cod.          Produto              V. Unitario       Qt.         Total");
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            itensVenda.ForEach(item =>
                            {
                                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, item.Substring(5, 13), 0, 13);
                                string nome = linha.Substring(13, 20).Trim();
                                Console.WriteLine($" {linha.Substring(0, 13)} {nome.PadRight(20, ' ')} {item.Substring(21, 5).Insert(3, ","),8:###.#0}     {item.Substring(18, 3),8:000}        {item.Substring(26, 6).Insert(1, ".").Insert(5, ","),8:#,##0.#0}");
                            });

                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadLine();
                            flagPrincipal = false;
                        }
                    }
                } while (flagPrincipal);
            }
        }

        public void PesquisarBloqueado()
        {
            string msgInicial, msgSaida, escolha, linhaEncontrada, razaoSocial, cnpj;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Bloqueado))
            {
                Console.WriteLine("\n xxxx Nao ha fornecedores bloqueados para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Fornecedor Bloqueado ::..\n");
                    Console.WriteLine(" Pesquisar por:\n");
                    Console.WriteLine(" 1 - Razao Social");
                    Console.WriteLine(" 2 - CNPJ");
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
                            Console.WriteLine(" ..:: Pesquisa de Fornecedor Bloqueado ::..\n");
                            Console.Write(" Digite a razao social: ");
                            razaoSocial = Console.ReadLine().PadLeft(50, ' ');

                            if (razaoSocial == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, razaoSocial, 14, 50)))
                                {
                                    Console.WriteLine("\n xxxx A razao social nao existe no cadastro de bloqueados.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, razaoSocial, 14, 50);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Fornecedor Bloqueado ::..\n");
                                    Console.WriteLine($" CNPJ:             {linhaEncontrada.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")}");
                                    Console.WriteLine($" Razao Social:     {linhaEncontrada.Substring(14, 50).Trim()}");
                                    Console.WriteLine($" Data de Abertura: {linhaEncontrada.Substring(64, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(72, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(80, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(88, 1) == "A" ? "Ativo" : "Bloqueado")}");

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
                            Console.WriteLine(" ..:: Pesquisa de Fornecedor Bloqueado ::..\n");
                            Console.Write(" Digite o CNPJ: ");
                            cnpj = Console.ReadLine();
                            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                            if (cnpj == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, cnpj, 0, 14)))
                                {
                                    Console.WriteLine("\n xxxx O CNPJ nao existe no cadastro de bloqueados.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, cnpj, 0, 14);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Fornecedor Bloqueado ::..\n");
                                    Console.WriteLine($" CNPJ:             {linhaEncontrada.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-")}");
                                    Console.WriteLine($" Razao Social:     {linhaEncontrada.Substring(14, 50).Trim()}");
                                    Console.WriteLine($" Data de Abertura: {linhaEncontrada.Substring(64, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Utima Compra:     {linhaEncontrada.Substring(72, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Data de Cadastro: {linhaEncontrada.Substring(80, 8).Insert(2, "/").Insert(5, "/")}");
                                    Console.WriteLine($" Situacao:         {(linhaEncontrada.Substring(88, 1) == "A" ? "Ativo" : "Bloqueado")}");

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

        public void PesquisarInadimplente()
        {
            string msgInicial, msgSaida, escolha, linhaEncontrada, nome, cpf;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Pesquisas ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Inadimplente))
            {
                Console.WriteLine("\n xxxx Nao ha clientes inadimplentes para pesquisar.");
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
                    Console.WriteLine(" ..:: Pesquisa de Cliente Inadimplente ::..\n");
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
                            Console.WriteLine(" ..:: Pesquisa de Cliente Inadimplente ::..\n");
                            Console.Write(" Digite o Nome: ");
                            nome = Console.ReadLine().PadLeft(50, ' ');

                            if (nome == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, nome, 11, 50)))
                                {
                                    Console.WriteLine("\n xxxx O nome nao existe no cadastro de inadimplentes.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, nome, 11, 50);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Cliente Inadimplente ::..\n");
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
                            Console.WriteLine(" ..:: Pesquisa de Cliente Inadimplente ::..\n");
                            Console.Write(" Digite o CPF: ");
                            cpf = Console.ReadLine();

                            if (cpf == "9")
                                return;
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                                {
                                    Console.WriteLine("\n xxxx O CPF nao existe no cadastro de inadimplentes.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    linhaEncontrada = Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, cpf.Replace(".", "").Replace("-", ""), 0, 11);

                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                                    Console.WriteLine(" ..:: Pesquisa de Cliente Inadimplente ::..\n");
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
    }
}
