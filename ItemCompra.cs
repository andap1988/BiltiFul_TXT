using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class ItemCompra
    {
        public int Id { get; set; }
        public DateTime DataCompra { get; set; }
        public string MateriaPrima { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal TotalItem { get; set; }
        public Arquivos Arquivos { get; set; }

        public ItemCompra() { Arquivos = new Arquivos(); }

        public ItemCompra(int id, DateTime dataCompra, string materiaPrima, decimal quantidade, decimal valorUnitario, decimal totalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + DataCompra.ToString("dd/MM/yyyy").Replace("/", "")
                + MateriaPrima
                + Quantidade.ToString("000.00").Replace(",", "")
                + ValorUnitario.ToString("000.00").Replace(",", "")
                + TotalItem.ToString("0,000.00").Replace(",", "").Replace(".", "");
        }

        public void IniciarCompra(string cnpj, string razaoSocial, string dataAbertura)
        {
            string msgInicial, msgSaida, nomeBusca, codBusca, codMPrima, nomeMPrima, mprimaEncontrada = null, qtMPrima, idLinha, escolha, valorMPrimaDigitado;
            decimal valorMPrima, totalMPrima = 0, totalCompra = 0, qt = 0;
            int idCompra, controleQuantidade = 0;
            bool flagInterna = true, flagPrincipal = true;
            List<string> mprimas = new();
            List<ItemCompra> itens = new();
            ItemCompra itemComprado = null;

            if (Arquivos.VerificarArquivoVazio(Arquivos.IdCompra))
            {
                idLinha = Arquivos.RecuperaLinhaInteira(Arquivos.IdCompra, null, 0, 5);
                idCompra = int.Parse(idLinha);
                idCompra++;
            }
            else
            {
                idCompra = 1;
            }

            msgInicial = "\n ...:: Cadastrar Compra ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                flagInterna = true;

                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CNPJ: {cnpj}");
                Console.WriteLine($" Nome: {razaoSocial}");
                Console.WriteLine($" Data Nasc.: {dataAbertura}");
                Console.WriteLine(" -----------------------------\n");

                mprimas = Arquivos.MontarLista(Arquivos.MateriaPrima, "I", 42, 1);

                do
                {
                    flagInterna = true;

                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine(" ..:: Materias-primas Disponiveis ::..\n");
                    mprimas.ForEach(mprima =>
                    {
                        Console.WriteLine($" Codigo: {mprima.Substring(0, 6)}");
                        Console.WriteLine($" Nome: {mprima.Substring(6, 20).Trim()}");
                        Console.WriteLine(" -------------------------\n");
                    });
                    Console.WriteLine(" Incluir materia-prima por: 1 - Nome / 2 - Codigo.");
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
                        Console.Write("\n Digite o nome da materia-prima: ");
                        nomeBusca = Console.ReadLine();

                        if (string.IsNullOrEmpty(nomeBusca))
                        {
                            Console.WriteLine("\n xxxx O nome da materia-prima nao pode ser vazio.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (nomeBusca == "9")
                                return;
                            else
                            {
                                mprimaEncontrada = null;
                                nomeBusca = nomeBusca.Trim().ToUpper();

                                mprimas.ForEach(mprima =>
                                {
                                    nomeMPrima = mprima.Substring(6, 20).Trim().ToUpper();
                                    if (nomeMPrima == nomeBusca)
                                        mprimaEncontrada = mprima;
                                });

                                if (string.IsNullOrEmpty(mprimaEncontrada))
                                {
                                    Console.WriteLine("\n xxxx Materia-prima nao encontrada.");
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
                        Console.Write("\n Digite o codigo da materia-prima: ");
                        codBusca = Console.ReadLine();

                        if (string.IsNullOrEmpty(codBusca))
                        {
                            Console.WriteLine("\n xxxx O codigo da materia-prima nao pode ser vazio.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (codBusca == "9")
                                return;
                            else
                            {
                                if (codBusca.Contains("MP"))
                                    codBusca = codBusca.Remove(0, 2).PadLeft(4, '0');

                                codBusca = codBusca.PadLeft(4, '0');

                                mprimaEncontrada = null;
                                mprimas.ForEach(mprima =>
                                {
                                    codMPrima = mprima.Substring(2, 4);
                                    if (codMPrima == codBusca)
                                        mprimaEncontrada = mprima;
                                });

                                if (string.IsNullOrEmpty(mprimaEncontrada))
                                {
                                    Console.WriteLine("\n xxxx Materia-prima nao encontrada.");
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
                    Console.WriteLine("\n ..:: Cadastrar Compra ::..");
                    Console.WriteLine(" -----------------------------\n");
                    Console.WriteLine($" CNPJ: {cnpj}");
                    Console.WriteLine($" Razao social: {razaoSocial}");
                    Console.WriteLine($" Data de abertura: {dataAbertura}");
                    Console.WriteLine(" -----------------------------\n");

                    codMPrima = mprimaEncontrada.Substring(0, 6);
                    nomeMPrima = mprimaEncontrada.Substring(6, 20).Trim();

                    Console.WriteLine($" Codigo:        {codMPrima}");
                    Console.WriteLine($" Nome:          {nomeMPrima}\n");
                    Console.Write($" Valor Unitario: ");
                    valorMPrimaDigitado = Console.ReadLine();

                    if (string.IsNullOrEmpty(valorMPrimaDigitado))
                    {
                        Console.WriteLine("\n xxxx Valor unitario nao pode ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else if (!decimal.TryParse(valorMPrimaDigitado, out valorMPrima))
                    {
                        Console.WriteLine("\n xxxx Valor unitario invalido.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        decimal.TryParse(valorMPrimaDigitado, out valorMPrima);

                        if (valorMPrima == 0)
                        {
                            Console.WriteLine("\n xxxx Quantidade nao pode ser 0.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else if (!(valorMPrima < 1000))
                        {
                            Console.WriteLine("\n xxxx Quantidade excedida. Maximo: 999,99.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Write(" Quantidade (entre 1 e 999,99): ");
                            qtMPrima = Console.ReadLine();

                            if (string.IsNullOrEmpty(qtMPrima))
                            {
                                Console.WriteLine("\n xxxx Quantidade nao pode ser vazia.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            else if (!decimal.TryParse(qtMPrima, out qt))
                            {
                                Console.WriteLine("\n xxxx Digite apenas numeros.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                decimal.TryParse(qtMPrima, out qt);

                                if (qt == 0)
                                {
                                    Console.WriteLine("\n xxxx Quantidade nao pode ser 0.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else if (!(qt < 1000))
                                {
                                    Console.WriteLine("\n xxxx Quantidade excedida. Maximo: 999,99.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n ..:: Cadastrar Compra ::..");
                                    Console.WriteLine(" -----------------------------\n");
                                    Console.WriteLine($" CNPJ: {cnpj}");
                                    Console.WriteLine($" Razao social: {razaoSocial}");
                                    Console.WriteLine($" Data de Abertura: {dataAbertura}");
                                    Console.WriteLine(" -----------------------------\n");
                                    Console.WriteLine($" Compra nº: {idCompra:00000}\n");

                                    if (!(valorMPrima * qt < 10000))
                                    {
                                        Console.WriteLine("\n xxxx A materia-prima adicionada ultrapassa o valor maximo permitido por cada item (maximo: R$ 9.999,99).");
                                        Console.WriteLine("\n Pressione ENTER para voltar...");
                                        Console.ReadKey();
                                        flagInterna = false;
                                    }
                                    else
                                    {
                                        totalMPrima = valorMPrima * qt;

                                        if (!(totalCompra + totalMPrima < 100000))
                                        {
                                            Console.WriteLine("\n xxxx A materia-prima adicionada ultrapassa o valor maximo permitido por cada pedido (maximo: R$ 99.999,99).");
                                            Console.WriteLine("\n Pressione ENTER para voltar...");
                                            Console.ReadKey();
                                            flagInterna = false;
                                        }
                                        else
                                        {
                                            
                                            itemComprado = new(idCompra, DateTime.Now.Date, codMPrima, qt, valorMPrima, totalMPrima);
                                            itens.Add(itemComprado);                                            

                                            MontarCupom(itens);

                                            if (itens.Count < 3)
                                            {
                                                Console.WriteLine("\n Desejas: \n");
                                                Console.WriteLine(" 1 - Adicionar mais uma materia-prima");
                                                Console.WriteLine(" 2 - Finalizar compra");
                                                Console.WriteLine(" ------------------------------");
                                                Console.WriteLine(" 9 - Cancelar compra\n");
                                                Console.Write("\n Escolha: ");
                                                escolha = Console.ReadLine();

                                                if (string.IsNullOrEmpty(escolha))
                                                {
                                                    Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                                    Console.ReadKey();
                                                    itens.Remove(itemComprado);
                                                }
                                                else if (escolha != "1" && escolha != "2" && escolha != "9")
                                                {
                                                    Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                                                    Console.ReadKey();
                                                    itens.Remove(itemComprado);
                                                }
                                                else if (escolha == "9")
                                                {
                                                    Console.WriteLine("\n xxxx Compra cancelada.");
                                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                                    Console.ReadKey();
                                                    return;
                                                }
                                                else if (escolha == "1")
                                                {
                                                    totalCompra += totalMPrima;
                                                    flagInterna = false;
                                                }
                                                else if (escolha == "2")
                                                {
                                                    totalCompra += totalMPrima;

                                                    Console.Clear();
                                                    Console.WriteLine("\n ..:: Cadastrar Compra ::..");
                                                    Console.WriteLine(" -----------------------------\n");
                                                    Console.WriteLine($" CNPJ {cnpj}");
                                                    Console.WriteLine($" Razao social: {razaoSocial}");
                                                    Console.WriteLine($" Data de Abertura: {dataAbertura}");
                                                    Console.WriteLine(" -----------------------------\n");
                                                    Console.WriteLine($" Compra nº: {idCompra:00000}\n");

                                                    MontarCupom(itens);

                                                    Console.WriteLine($"                                          Total da Compra: R$ {totalCompra:#,###.#0}");

                                                    Console.WriteLine("\n Finalizar compra? 1 - SIM / 2 - NAO (ao escolher NAO, a compra sera cancelada)");
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
                                                        Console.WriteLine("\n xxxx Compra cancelada.");
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
                                                Console.WriteLine("\n ..:: Cadastrar Compra ::..");
                                                Console.WriteLine(" -----------------------------\n");
                                                Console.WriteLine($" CNPJ {cnpj}");
                                                Console.WriteLine($" Razao social: {razaoSocial}");
                                                Console.WriteLine($" Data de Abertura: {dataAbertura}");
                                                Console.WriteLine(" -----------------------------\n");
                                                Console.WriteLine($" Compra nº: {idCompra:00000}\n");

                                                MontarCupom(itens);

                                                totalCompra = 0;

                                                itens.ForEach(item =>
                                                {
                                                    totalCompra = totalCompra + item.TotalItem;
                                                });

                                                bool flag = true;
                                                do
                                                {
                                                    Console.WriteLine($"                                          Total da Compra: R$ {totalCompra:#,###.#0}\n");
                                                    Console.WriteLine("\n Finalizar compra? 1 - SIM / 2 - NAO (ao escolher NAO, a compra sera cancelada)");
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
                                                        Console.WriteLine("\n xxxx Compra cancelada.");
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
                        }
                    }
                } while (flagInterna);

                if (escolha == "100")
                {
                    cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                    Compra compraFeita = new(idCompra, DateTime.Now.Date, decimal.Parse(cnpj), totalCompra);
                    MPrima tempMPrima = new();

                    Arquivos.DeletarArquivo(Arquivos.IdCompra);
                    Arquivos.Gravar(idCompra.ToString("00000"), Arquivos.IdCompra);
                    itens.ForEach(item =>
                    {
                        tempMPrima.AlterarDataUltimaCompra(DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", ""), item.MateriaPrima);
                        Arquivos.Gravar(item.ToString(), Arquivos.ItemCompra);
                    });

                    Arquivos.Gravar(compraFeita.ToString(), Arquivos.Compra);

                    Console.WriteLine("\n Compra concluida com sucesso!");
                    Console.WriteLine("\n Pressione ENTER para voltar...");
                    Console.ReadKey();
                    flagPrincipal = false;
                }

            } while (flagPrincipal);
        }

        public void MontarCupom(List<ItemCompra> itens)
        {
            Console.WriteLine("\n Cod.       Materia-prima           V. Unitario         Qt.         Total");
            Console.WriteLine(" ---------------------------------------------------------------------------\n");
            itens.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.MateriaPrima, 0, 6);
                string nome = linha.Substring(6, 20).Trim();
                Console.WriteLine($" {item.MateriaPrima}     {nome.PadRight(20, ' ')}     {item.ValorUnitario,8:##0.#0}      {item.Quantidade,8:##0.#0}       {item.TotalItem,8:#,##0.#0}");
            });
            Console.WriteLine("\n ---------------------------------------------------------------------------\n");
        }
    }
}
