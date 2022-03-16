using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class ItemVenda
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal TotalItem { get; set; }
        public Arquivos Arquivos { get; set; }

        public ItemVenda() { Arquivos = new Arquivos(); }

        public ItemVenda(int id, string produto, int quantidade, decimal valorUnitario, decimal totalItem)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + Produto
                + Quantidade.ToString("000")
                + ValorUnitario.ToString("000.00").Replace(",", "")
                + TotalItem.ToString("0,000.00").Replace(",", "").Replace(".", "");
        }

        public void IniciarVenda(string cpf, string nome, string dataNasc)
        {
            string msgInicial, msgSaida, nomeBusca, codBusca, codProd, nomeProd, produtoEncontrado = null, qtProd, idLinha, escolha;
            decimal valorProd, totalProd = 0, totalVenda = 0;
            int qt = 0, idVenda;
            bool flagInterna = true, flagPrincipal = true;
            List<string> produtos = new();
            List<ItemVenda> itens = new();
            ItemVenda itemVendido = null;

            if (Arquivos.VerificarArquivoVazio(Arquivos.IdVenda))
            {
                idLinha = Arquivos.RecuperaLinhaInteira(Arquivos.IdVenda, null, 0, 5);
                idVenda = int.Parse(idLinha);
                idVenda++;
            }
            else
            {
                idVenda = 1;
            }

            msgInicial = "\n ...:: Cadastrar Venda ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                flagInterna = true;

                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CPF: {cpf}");
                Console.WriteLine($" Nome: {nome}");
                Console.WriteLine($" Data Nasc.: {dataNasc}");
                Console.WriteLine(" -----------------------------\n");

                produtos = Arquivos.MontarLista(Arquivos.Produto, "I", 54, 1);

                do
                {
                    flagInterna = true;

                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine(" ..:: Produtos Disponiveis ::..\n");
                    produtos.ForEach(produto =>
                    {
                        Console.WriteLine($" Codigo: {produto.Substring(0, 13)}");
                        Console.WriteLine($" Nome: {produto.Substring(13, 20).Trim()}");
                        Console.WriteLine($" Valor: {produto.Substring(33, 5).Insert(3, ",")}");
                        Console.WriteLine(" -------------------------\n");
                    });
                    Console.WriteLine(" Incluir produto por: 1 - Nome / 2 - Codigo.");
                    Console.Write("\n Escolha: ");
                    escolha = Console.ReadLine();

                    if (escolha == "9")
                        return;
                    else if (escolha != "1" && escolha != "2" && escolha != "9")
                    {
                        Console.WriteLine("\n xxxx Digite apenas '1' para NOME ou '2' para CODIGO.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                        Console.ReadKey();
                    }
                    else if (escolha == "1")
                    {
                        Console.Write("\n Digite o nome do produto: ");
                        nomeBusca = Console.ReadLine();

                        if (string.IsNullOrEmpty(nomeBusca))
                        {
                            Console.WriteLine("\n xxxx O nome do produto nao pode ser vazio.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (nomeBusca == "9")
                                return;
                            else
                            {
                                produtoEncontrado = null;
                                nomeBusca = nomeBusca.Trim().ToUpper();

                                produtos.ForEach(produto =>
                                {
                                    nomeProd = produto.Substring(13, 20).Trim().ToUpper();
                                    if (nomeProd == nomeBusca)
                                        produtoEncontrado = produto;
                                });

                                if (string.IsNullOrEmpty(produtoEncontrado))
                                {
                                    Console.WriteLine("\n xxxx Produto nao encontrado.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }
                        }
                    }
                    else if (escolha == "2")
                    {
                        Console.Write("\n Digite o codigo do produto: ");
                        codBusca = Console.ReadLine();

                        if (string.IsNullOrEmpty(codBusca))
                        {
                            Console.WriteLine("\n xxxx O codigo do produto nao pode ser vazio.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (codBusca == "9")
                                return;
                            else
                            {
                                produtoEncontrado = null;
                                produtos.ForEach(produto =>
                                {
                                    codProd = produto.Substring(0, 13);
                                    if (codProd == codBusca)
                                        produtoEncontrado = produto;
                                });

                                if (string.IsNullOrEmpty(produtoEncontrado))
                                {
                                    Console.WriteLine("\n xxxx Produto nao encontrado.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }
                        }
                    }
                } while (flagInterna);

                flagInterna = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine("\n ..:: Cadastrar Venda ::..");
                    Console.WriteLine(" -----------------------------\n");
                    Console.WriteLine($" CPF: {cpf}");
                    Console.WriteLine($" Nome: {nome}");
                    Console.WriteLine($" Data Nasc.: {dataNasc}");
                    Console.WriteLine(" -----------------------------\n");

                    codProd = produtoEncontrado.Substring(0, 13);
                    nomeProd = produtoEncontrado.Substring(13, 20).Trim();
                    valorProd = decimal.Parse(produtoEncontrado.Substring(33, 5).Insert(3, ","));

                    Console.WriteLine($" Codigo:        {codProd}");
                    Console.WriteLine($" Nome:          {nomeProd}");
                    Console.WriteLine($" V. Unitario:   {valorProd}\n");
                    Console.Write(" Quantidade (entre 1 e 999): ");
                    qtProd = Console.ReadLine();

                    if (string.IsNullOrEmpty(qtProd))
                    {
                        Console.WriteLine("\n xxxx Quantidade nao pode ser vazia.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else if (!int.TryParse(qtProd, out qt))
                    {
                        Console.WriteLine("\n xxxx Digite apenas numeros.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        int.TryParse(qtProd, out qt);

                        if (qt == 0)
                        {
                            Console.WriteLine("\n xxxx Quantidade nao pode ser 0.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else if (qt > 999)
                        {
                            Console.WriteLine("\n xxxx Quantidade excedida. Maximo: 999.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\n ..:: Cadastrar Venda ::..");
                            Console.WriteLine(" -----------------------------\n");
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($" Nome: {nome}");
                            Console.WriteLine($" Data Nasc.: {dataNasc}");
                            Console.WriteLine(" -----------------------------\n");
                            Console.WriteLine($" Venda nº: {idVenda:00000}\n");

                            if (!(valorProd * qt < 10000))
                            {
                                Console.WriteLine("\n xxxx O produto adicionado ultrapassa o valor maximo permitido por cada item (maximo: R$ 9.999,99).");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                                flagInterna = false;
                            }
                            else
                            {
                                totalProd = valorProd * qt;

                                if (!(totalVenda + totalProd < 100000))
                                {
                                    Console.WriteLine("\n xxxx O produto adicionado ultrapassa o valor maximo permitido por cada pedido (maximo: R$ 99.999,99).");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                    flagInterna = false;
                                }
                                else
                                {
                                    totalVenda += totalProd;
                                    itemVendido = new(idVenda, codProd, qt, valorProd, totalProd);
                                    itens.Add(itemVendido);

                                    MontarCupom(itens);

                                    if (itens.Count < 3)
                                    {
                                        Console.WriteLine("\n Desejas: \n");
                                        Console.WriteLine(" 1 - Adicionar mais um produto");
                                        Console.WriteLine(" 2 - Finalizar venda");
                                        Console.WriteLine(" ------------------------------");
                                        Console.WriteLine(" 9 - Cancelar venda\n");
                                        Console.Write("\n Escolha: ");
                                        escolha = Console.ReadLine();

                                        if (escolha != "1" && escolha != "2" && escolha != "9")
                                        {
                                            Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                            Console.ReadKey();
                                        }
                                        else if (escolha == "9")
                                        {
                                            Console.WriteLine("\n xxxx Venda cancelada.");
                                            Console.WriteLine("\n Pressione ENTER para voltar...");
                                            Console.ReadKey();
                                            return;
                                        }
                                        else if (escolha == "1")
                                        {
                                            flagInterna = false;
                                        }
                                        else if (escolha == "2")
                                        {
                                            Console.Clear();
                                            Console.WriteLine("\n ..:: Cadastrar Venda ::..");
                                            Console.WriteLine(" -----------------------------\n");
                                            Console.WriteLine($" CPF: {cpf}");
                                            Console.WriteLine($" Nome: {nome}");
                                            Console.WriteLine($" Data Nasc.: {dataNasc}");
                                            Console.WriteLine(" -----------------------------\n");
                                            Console.WriteLine($" Venda nº: {idVenda:00000}\n");

                                            MontarCupom(itens);

                                            Console.WriteLine($"                                           Total da Venda: R$ {totalVenda:#,###.#0}");

                                            Console.WriteLine("\n Finalizar venda? 1 - SIM / 2 - NAO (ao escolher NAO, a venda sera cancelada)");
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
                                                Console.WriteLine("\n xxxx Venda cancelada.");
                                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                                Console.ReadKey();
                                                return;
                                            }
                                            else if (escolha == "1")
                                            {
                                                escolha = "100";
                                                flagInterna = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\n ..:: Cadastrar Venda ::..");
                                        Console.WriteLine(" -----------------------------\n");
                                        Console.WriteLine($" CPF: {cpf}");
                                        Console.WriteLine($" Nome: {nome}");
                                        Console.WriteLine($" Data Nasc.: {dataNasc}");
                                        Console.WriteLine(" -----------------------------\n");
                                        Console.WriteLine($" Venda nº: {idVenda:00000}\n");

                                        MontarCupom(itens);

                                        itens.ForEach(item =>
                                        {
                                            totalVenda = totalVenda + item.TotalItem;
                                        });

                                        bool flag = true;
                                        do
                                        {
                                            Console.WriteLine($"                                           Total da Venda: R$ {totalVenda:#,###.#0}\n");
                                            Console.WriteLine("\n Finalizar venda? 1 - SIM / 2 - NAO (ao escolher NAO, a venda sera cancelada)");
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
                                                Console.WriteLine("\n xxxx Venda cancelada.");
                                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                                Console.ReadKey();
                                                return;
                                            }
                                            else if (escolha == "1")
                                            {
                                                escolha = "100";
                                                flagInterna = false;
                                                flag = false;
                                            }
                                        } while (flag);
                                    }
                                }
                            }
                        }
                    }
                } while (flagInterna);

                if (escolha == "100")
                {
                    cpf = cpf.Replace(".", "").Replace("-", "");
                    Venda vendaFeita = new(idVenda, DateTime.Now.Date, decimal.Parse(cpf), totalVenda);
                    Produto tempProduto = new();

                    Arquivos.DeletarArquivo(Arquivos.IdVenda);
                    Arquivos.Gravar(idVenda.ToString("00000"), Arquivos.IdVenda);
                    itens.ForEach(item =>
                    {
                        tempProduto.AlterarDataUltimaVenda(DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", ""), item.Produto);
                        Arquivos.Gravar(item.ToString(), Arquivos.ItemVenda);
                    });

                    Arquivos.Gravar(vendaFeita.ToString(), Arquivos.Venda);

                    Console.WriteLine("\n Venda concluida com sucesso!");
                    Console.WriteLine("\n Pressione ENTER para voltar...");
                    Console.ReadKey();
                    flagPrincipal = false;
                }

            } while (flagPrincipal);
        }

        public void MontarCupom(List<ItemVenda> itens)
        {
            Console.WriteLine("\n Cod.          Produto              V. Unitario      Qt.         Total");
            Console.WriteLine(" -------------------------------------------------------------------------\n");
            itens.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, item.Produto, 0, 13);
                string nome = linha.Substring(13, 20).Trim();
                Console.WriteLine($" {item.Produto} {nome.PadRight(20, ' ')} {item.ValorUnitario,8:##0.#0}     {item.Quantidade,8:000}        {item.TotalItem,8:#,##0.#0}");
            });
            Console.WriteLine("\n -------------------------------------------------------------------------\n");
        }
    }
}
