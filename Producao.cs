using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Producao
    {
        public int Id { get; set; }
        public DateTime DataProducao { get; set; }
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public Arquivos Arquivos { get; set; }

        public Producao() { Arquivos = new Arquivos(); }

        public Producao(int id, DateTime dataProducao, string produto, decimal quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            Produto = produto;
            Quantidade = quantidade;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + DataProducao.ToString("dd/MM/yyyy").Replace("/", "")
                + Produto
                + Quantidade.ToString("000.00").Replace(",", "");
        }

        public void Menu()
        {
            int option = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Menu Producao ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Producao");
                Console.WriteLine(" 2 - Excluir Producao");
                Console.WriteLine(" 3 - Mostrar Producões");
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
                            Cadastrar();
                            break;
                        case 2:
                            Excluir();
                            break;
                        case 3:
                            Imprimir();
                            break;
                    }
                }
            } while (flag);
        }

        public void Cadastrar()
        {
            string msgInicial, msgSaida, escolha, produtoEscolhido = null;
            bool flagInterna = true, flagPrincipal = true;
            List<string> itensArquivo = new();

            msgInicial = "\n ...:: Cadastrar Producao ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha produto cadastrado para iniciar uma producao.");
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
                    do
                    {
                        escolha = null;
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");

                        Console.WriteLine(" Desejas: \n");
                        Console.WriteLine(" 1 - Digitar o codigo ou nome do produto para iniciar a producao.");
                        Console.WriteLine(" 2 - Listar todos os produtos disponiveis para producao.");
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
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");

                            Console.WriteLine(" Porcurar por: 1 - Nome / 2 - Codigo.");
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
                                Console.Write("\n Digite o nome do produto: ");
                                escolha = Console.ReadLine();

                                if (string.IsNullOrEmpty(escolha))
                                {
                                    Console.WriteLine("\n xxxx O nome nao pode ser vazio.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    escolha = escolha.PadLeft(20, ' ');
                                    produtoEscolhido = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, escolha, 13, 20);

                                    if (string.IsNullOrEmpty(produtoEscolhido))
                                    {
                                        Console.WriteLine("\n xxxx Produto nao escontrado");
                                        Console.WriteLine("\n Pressione ENTER para voltar...");
                                        Console.ReadKey();
                                    }
                                    else if (char.Parse(produtoEscolhido.Substring(54, 1)) == 'I')
                                    {
                                        Console.WriteLine("\n xxxx Produto esta indisponivel para producao (inativo)");
                                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                        Console.ReadKey();
                                    }
                                    else
                                        flagInterna = false;
                                }
                            }
                            else if (escolha == "2")
                            {
                                Console.Write("\n Digite o codigo do produto: ");
                                escolha = Console.ReadLine();
                                produtoEscolhido = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, escolha, 0, 13);

                                if (string.IsNullOrEmpty(produtoEscolhido))
                                {
                                    Console.WriteLine("\n xxxx Produto nao escontrado");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                    Console.ReadKey();
                                }
                                else if (char.Parse(produtoEscolhido.Substring(54, 1)) == 'I')
                                {
                                    Console.WriteLine("\n xxxx Produto esta indisponivel para producao (inativo)");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }
                        }
                        else if (escolha == "2")
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine("                        ..:: Produtos ::..");

                            itensArquivo = Arquivos.MontarLista(Arquivos.Produto, "I", 54, 1);

                            itensArquivo.ForEach(item =>
                            {
                                MontarProduto(item);
                            });

                            Console.WriteLine(" -------------------------------------------------------------------------\n");

                            Console.WriteLine(" Porcurar por: 1 - Nome / 2 - Codigo.");
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
                                Console.Write("\n Digite o nome do produto: ");
                                escolha = Console.ReadLine();

                                if (string.IsNullOrEmpty(escolha))
                                {
                                    Console.WriteLine("\n xxxx O nome nao pode ser vazio.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    escolha = escolha.PadLeft(20, ' ');
                                    produtoEscolhido = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, escolha, 13, 20);

                                    if (string.IsNullOrEmpty(produtoEscolhido))
                                    {
                                        Console.WriteLine("\n xxxx Produto nao escontrado");
                                        Console.WriteLine("\n Pressione ENTER para voltar...");
                                        Console.ReadKey();
                                    }
                                    else if (char.Parse(produtoEscolhido.Substring(54, 1)) == 'I')
                                    {
                                        Console.WriteLine("\n xxxx Produto esta indisponivel para producao (inativo)");
                                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                        Console.ReadKey();
                                    }
                                    else
                                        flagInterna = false;
                                }
                            }
                            else if (escolha == "2")
                            {
                                Console.Write("\n Digite o codigo do produto: ");
                                escolha = Console.ReadLine().PadLeft(5, '0');
                                produtoEscolhido = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, escolha, 0, 13);

                                if (string.IsNullOrEmpty(produtoEscolhido))
                                {
                                    Console.WriteLine("\n xxxx Produto nao escontrado");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                    Console.ReadKey();
                                }
                                else if (char.Parse(produtoEscolhido.Substring(54, 1)) == 'I')
                                {
                                    Console.WriteLine("\n xxxx Produto esta indisponivel para producao (inativo)");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }
                        }
                    } while (flagInterna);

                    flagInterna = true;

                    do
                    {
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine($" Codigo nº:      {produtoEscolhido.Substring(0, 13)}");
                        Console.WriteLine($" Nome:           {produtoEscolhido.Substring(13, 20).Trim()}");
                        Console.WriteLine($" Ultima Venda:   {produtoEscolhido.Substring(38, 8).Insert(2, "/").Insert(5, "/")}");
                        Console.WriteLine($" Valor da Venda: {produtoEscolhido.Substring(33, 5).Insert(3, ",")}");

                        Console.WriteLine("\n Confirma produto escolhido? 1 - SIM / 2 - NAO: ");
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
                            ItemProducao novaProducao = new();
                            novaProducao.IniciarProducao(produtoEscolhido);
                            Console.WriteLine(" Acabou producao");
                            Console.ReadKey();
                            flagInterna = false;
                            flagPrincipal = false;
                        }
                        else if (escolha == "2")
                        {
                            Console.WriteLine("\n xxxx Producao cancelada.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                            flagInterna = false;
                            flagPrincipal = false;
                        }
                    } while (flagInterna);
                } while (flagPrincipal);
            }
        }

        public void MontarProduto(string item)
        {
            string codProduto = item.Substring(0, 13);
            string nomeProd = item.Substring(13, 20).Trim();
            string ultimaVenda = item.Substring(38, 8).Insert(2, "/").Insert(5, "/");
            string valorVenda = item.Substring(33, 5);

            Console.WriteLine($"\n Codigo nº:      {codProduto}");
            Console.WriteLine($" Nome:           {nomeProd}");
            Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
            Console.WriteLine($" Valor da Venda: {valorVenda.Insert(3, ","):###.#0}");
        }

        public void Excluir()
        {

        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Producao))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma producao realizada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Producao");
            }
        }
    }
}
