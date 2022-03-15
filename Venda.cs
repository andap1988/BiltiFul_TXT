using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public Arquivos Arquivos { get; set; }

        public Venda() { Arquivos = new Arquivos(); }

        public Venda(int id, DateTime dataVenda, decimal cliente, decimal valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            ValorTotal = valorTotal;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + DataVenda.ToString("dd/MM/yyyy").Replace("/", "")
                + Cliente.ToString("00000000000")
                + ValorTotal.ToString("0,000.00").Replace(",", "").Replace(".", "");
        }

        public void Menu()
        {
            int option = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Menu Venda de Produtos ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Venda");
                Console.WriteLine(" 2 - Excluir Venda");
                Console.WriteLine(" 3 - Mostrar Vendas");
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
            string msgInicial, msgSaida, cpf, nome, dataNasc, linha, escolha;
            bool flagInterna = true, flagPrincipal = true, vendaFeita;

            msgInicial = "\n ...:: Cadastrar Venda ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha produto cadastrado.");
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
                        Console.Write(" Para comecar, digite o CPF do cliente: ");
                        cpf = Console.ReadLine();
                        if (cpf == "9")
                            return;
                        else
                        {
                            if (string.IsNullOrEmpty(cpf))
                            {
                                Console.WriteLine("\n xxxx CPF nao pode ser vazio.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else if (!string.IsNullOrEmpty(
                                Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                            {
                                Console.WriteLine("\n xxxx Cadastro desatualizado. Solicite ao cliente procurar a gerencia para atualizacao cadastral.");
                                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                Console.ReadKey();
                                return;
                            }
                            else if (string.IsNullOrEmpty(
                                Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                            {
                                Console.WriteLine("\n xxxx CPF nao cadastrado.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }
                            else
                                flagInterna = false;
                        }
                    } while (flagInterna);

                    if (!cpf.Contains("."))
                        cpf = cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");

                    flagInterna = true;

                    linha = Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11);
                    nome = linha.Substring(11, 50).Trim();
                    dataNasc = linha.Substring(61, 8).Insert(2, "/").Insert(5, "/");
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine($" CPF: {cpf}");
                        Console.WriteLine($" Nome: {nome}");
                        Console.WriteLine($" Data Nasc.: {dataNasc}");

                        Console.WriteLine("\n Confirma os dados do cliente? 1 - SIM / 2 - NAO");
                        Console.Write("\n Escolha: ");
                        escolha = Console.ReadLine();

                        if (escolha != "1" && escolha != "2")
                        {
                            Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else if (escolha == "2")
                        {
                            Console.WriteLine("\n xxxx A venda foi cancelada.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                            return;
                        }
                        else if (escolha == "1")
                            flagInterna = false;
                    } while (flagInterna);

                    ItemVenda novaVenda = new();
                    novaVenda.IniciarVenda(cpf, nome, dataNasc);
                    flagPrincipal = false;

                } while (flagPrincipal);
            }
        }

        public void Excluir()
        {
            string msgInicial, msgSaida, escolha, vendaEscolhida = null;
            bool flagPrincipal = true, flagInterna = true;
            List<string> itensArquivo = null;


            msgInicial = "\n ...:: Excluir Venda ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Venda))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma venda cadastrada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                do
                {
                    escolha = null;
                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");

                    Console.WriteLine(" Desejas: \n");
                    Console.WriteLine(" 1 - Digitar o codigo da venda pra exclusao.");
                    Console.WriteLine(" 2 - Listar todas as vendas disponiveis para exclusao.");
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
                        Console.Write(" Digite o codigo da venda para exclusao: ");
                        escolha = Console.ReadLine().PadLeft(5, '0');
                        vendaEscolhida = Arquivos.RecuperaLinhaInteira(Arquivos.Venda, escolha, 0, 5);
                    }
                    else if (escolha == "2")
                    {
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine("                        ..:: Vendas realizadas ::..");

                        itensArquivo = Arquivos.MontarLista(Arquivos.Venda);

                        itensArquivo.ForEach(item =>
                        {
                            MontarVenda(item);
                        });

                        Console.Write(" Digite o codigo da venda para exclusao: ");
                        escolha = Console.ReadLine().PadLeft(5, '0');
                        vendaEscolhida = Arquivos.RecuperaLinhaInteira(Arquivos.Venda, escolha, 0, 5);
                    }                    

                    if (string.IsNullOrEmpty(vendaEscolhida))
                    {
                        Console.WriteLine("\n xxxx Codigo invalido.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
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

                            MontarVenda(vendaEscolhida);

                            Console.WriteLine(" Deseja excluir essa venda? 1 - SIM / 2 - NAO.");
                            Console.Write("\n Escolha: ");
                            escolha = Console.ReadLine();

                            if (escolha != "1" && escolha != "2")
                            {
                                Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                Console.ReadKey();
                            }
                            else if (escolha == "1")
                            {
                                // fazer a exclusao da venda e dos itens da venda
                            }
                            else if (escolha == "2")
                            {
                                Console.WriteLine("\n xxxx Exclusao cancelada.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }

                        } while (flagInterna);
                    }


                } while (flagPrincipal);
            }
        }

        public void MontarVenda(string item)
        {
            List<string> itensVenda = null;

            string codPedido = item.Substring(0, 5);
            string dataVenda = item.Substring(5, 8).Insert(2, "/").Insert(5, "/");
            string cpf = item.Substring(13, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-");
            string valorVenda = item.Substring(24, 6);

            Console.WriteLine($"\n Pedido nº:        {codPedido}");
            Console.WriteLine($" Data Venda:       {dataVenda}");
            Console.WriteLine($" CPF:              {cpf}");
            Console.WriteLine($" Valor da Venda:   {valorVenda.Insert(1, ".").Insert(5, ","):#,###.#0}");

            itensVenda = Arquivos.MontarLista(Arquivos.ItemVenda, item.Substring(0, 5), 0, 5, true);

            Console.WriteLine("\n Cod.          Produto              V. Unitario       Qt.         Total");
            Console.WriteLine(" -------------------------------------------------------------------------\n");
            itensVenda.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, item.Substring(5, 13), 0, 13);
                string nome = linha.Substring(13, 20).Trim();
                Console.WriteLine($" {linha.Substring(0, 13)} {nome.PadRight(20, ' ')} {item.Substring(21, 5).Insert(3, ","),8:###.#0}     {item.Substring(18, 3),8:000}        {item.Substring(26, 6).Insert(1, ".").Insert(5, ","),8:#,###.#0}");
            });
            Console.WriteLine("\n\n                               --/-------/--\n");
        }

        public void MontarCupom(List<ItemVenda> itens)
        {

            Console.WriteLine("\n Cod.          Produto              V. Unitario      Qt.         Total");
            Console.WriteLine(" -------------------------------------------------------------------------\n");
            itens.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, item.Produto, 0, 13);
                string nome = linha.Substring(13, 20).Trim();
                Console.WriteLine($" {item.Produto} {nome.PadRight(20, ' ')} {item.ValorUnitario,8:###.#0}     {item.Quantidade,8:000}        {item.TotalItem,8:#,###.#0}");
            });
            Console.WriteLine("\n -------------------------------------------------------------------------\n");
        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Venda))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma venda cadastrada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Venda");
            }
        }
    }
}
