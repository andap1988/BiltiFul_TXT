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
            string msgInicial, msgSaida, escolha, produtoEscolhido;
            bool flagInterna = true, flagPrincipal = true;

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
                        Console.WriteLine(" 1 - Digitar o codigo do produto para iniciar a producao.");
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
                            Console.Write(" Digite o codigo do produto: ");
                            escolha = Console.ReadLine();
                            produtoEscolhido = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, escolha, 0, 13);
                            flagInterna = false;
                        }
                        else if (escolha == "2")
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine("                        ..:: Produtos ::..");

                            itensArquivo = Arquivos.MontarLista(Arquivos.Produto);

                            itensArquivo.ForEach(item =>
                            {
                                MontarCompra(item);
                            });

                            Console.Write(" Digite o codigo da compra para exclusao: ");
                            escolha = Console.ReadLine().PadLeft(5, '0');
                            compraEscolhida = Arquivos.RecuperaLinhaInteira(Arquivos.Compra, escolha, 0, 5);
                            flagInterna = false;
                        }
                    } while (flagInterna);

                    ItemVenda novaVenda = new();
                    novaVenda.IniciarVenda(cpf, nome, dataNasc);
                    flagPrincipal = false;

                } while (flagPrincipal);
            }
        }

        public void Excluir()
        {

        }

        public void Imprimir()
        {

        }
    }
}
