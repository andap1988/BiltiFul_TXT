using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Fornecedor { get; set; }
        public decimal ValorTotal { get; set; }
        public Arquivos Arquivos { get; set; }

        public Compra() { Arquivos = new Arquivos(); }

        public Compra(int id, DateTime dataCompra, decimal fornecedor, decimal valorTotal)
        {
            Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + DataCompra.ToString("dd/MM/yyyy").Replace("/", "")
                + Fornecedor.ToString("00000000000000")
                + ValorTotal.ToString("0,000.00").Replace(",", "").Replace(".", "");
        }

        public void Menu()
        {
            int option = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Menu Compra de Materias-primas ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Compra");
                Console.WriteLine(" 2 - Excluir Compra");
                Console.WriteLine(" 3 - Mostrar Compras");
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
            string msgInicial, msgSaida, cnpj, razaoSocial, dataAbertura, linha, escolha;
            bool flagInterna = true, flagPrincipal = true;

            msgInicial = "\n ...:: Cadastrar Compra ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.MateriaPrima))
            {
                Console.WriteLine("\n xxxx Nao ha materia-prima cadastrada.");
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
                        Console.Write(" Para comecar, digite o CNPJ do fornecedor: ");
                        cnpj = Console.ReadLine();
                        if (cnpj == "9")
                            return;
                        else
                        {
                            if (string.IsNullOrEmpty(cnpj))
                            {
                                Console.WriteLine("\n xxxx CNPJ nao pode ser vazio.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else if (!string.IsNullOrEmpty(
                                Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14)))
                            {
                                Console.WriteLine("\n xxxx Fornecedor bloqueado para novas compras.");
                                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                Console.ReadKey();
                                return;
                            }
                            else if (string.IsNullOrEmpty(
                                Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14)))
                            {
                                Console.WriteLine("\n xxxx CNPJ nao cadastrado.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }
                            else
                                flagInterna = false;
                        }
                    } while (flagInterna);

                    if (!cnpj.Contains("."))
                        cnpj = cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");

                    flagInterna = true;

                    linha = Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14);
                    razaoSocial = linha.Substring(14, 50).Trim();
                    dataAbertura = linha.Substring(64, 8).Insert(2, "/").Insert(5, "/");

                    do
                    {
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine($" CNPJ: {cnpj}");
                        Console.WriteLine($" Razao social: {razaoSocial}");
                        Console.WriteLine($" Data de Abertura: {dataAbertura}");

                        Console.WriteLine("\n Confirma os dados do fornecedor? 1 - SIM / 2 - NAO");
                        Console.Write("\n Escolha: ");
                        escolha = Console.ReadLine();

                        if (escolha != "1" && escolha != "2" && escolha != "9")
                        {
                            Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else if (escolha == "9")
                            return;
                        else if (escolha == "2")
                        {
                            Console.WriteLine("\n xxxx A compra foi cancelada.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                            return;
                        }
                        else if (escolha == "1")
                            flagInterna = false;
                    } while (flagInterna);

                    ItemCompra novaCompra = new();
                    novaCompra.IniciarCompra(cnpj, razaoSocial, dataAbertura);
                    flagPrincipal = false;

                } while (flagPrincipal);
            }
        }

        public void Excluir()
        {
            string msgInicial, msgSaida, escolha, compraEscolhida = null;
            bool flagPrincipal = true, flagInterna = true;
            List<string> itensArquivo = null;


            msgInicial = "\n ...:: Excluir Compra ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Compra))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma compra cadastrada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                do
                {
                    flagInterna = true;
                    do
                    {
                        escolha = null;
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");

                        Console.WriteLine(" Desejas: \n");
                        Console.WriteLine(" 1 - Digitar o codigo da compra pra exclusao.");
                        Console.WriteLine(" 2 - Listar todas as compras disponiveis para exclusao.");
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
                            Console.Write(" Digite o codigo da compra para exclusao: ");
                            escolha = Console.ReadLine().PadLeft(5, '0');
                            compraEscolhida = Arquivos.RecuperaLinhaInteira(Arquivos.Compra, escolha, 0, 5);
                            flagInterna = false;
                        }
                        else if (escolha == "2")
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine("                        ..:: Compras realizadas ::..");

                            itensArquivo = Arquivos.MontarLista(Arquivos.Compra);

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

                    flagInterna = true;

                    if (string.IsNullOrEmpty(compraEscolhida))
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

                            MontarCompra(compraEscolhida);

                            Console.WriteLine(" Deseja excluir essa compra? 1 - SIM / 2 - NAO.");
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
                                Arquivos.AlterarDocumento(Arquivos.Compra, compraEscolhida, compraEscolhida.Substring(0, 5), 0, 5, true);
                                Arquivos.AlterarDocumento(Arquivos.ItemCompra, compraEscolhida, compraEscolhida.Substring(0, 5), 0, 5, true);

                                Console.WriteLine("\n A compra foi excluida.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                                flagInterna = false;
                                flagPrincipal = false;
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

        public void MontarCompra(string item)
        {
            List<string> itensCompra = null;

            string codCompra = item.Substring(0, 5);
            string dataCompra = item.Substring(5, 8).Insert(2, "/").Insert(5, "/");
            string cnpj = item.Substring(13, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            string valorCompra = item.Substring(27, 6);

            Console.WriteLine($"\n Compra nº:         {codCompra}");
            Console.WriteLine($" Data Compra:       {dataCompra}");
            Console.WriteLine($" CNPJ:              {cnpj}");
            Console.WriteLine($" Valor da Compra:   {valorCompra.Insert(1, ".").Insert(5, ","):#,##0.#0}");

            itensCompra = Arquivos.MontarLista(Arquivos.ItemCompra, item.Substring(0, 5), 0, 5, true);

            Console.WriteLine("\n Cod.       Materia-prima           V. Unitario         Qt.         Total");
            Console.WriteLine(" ---------------------------------------------------------------------------\n");
            itensCompra.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.Substring(13, 6), 0, 6);
                string nome = linha.Substring(6, 20).Trim();
                Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')}     {item.Substring(21, 5).Insert(3, ","),8:##0.#0}      {item.Substring(18, 3),8:##0.#0}       {item.Substring(26, 6).Insert(1, ".").Insert(5, ","),8:#,##0.#0}");
            });
            Console.WriteLine("\n\n                               --/-------/--\n");
        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Compra))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma compra cadastrada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Compra");
            }
        }
    }
}
