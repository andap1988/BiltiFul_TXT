using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class ItemProducao
    {
        public int Id { get; set; }
        public DateTime DataProducao { get; set; }
        public string MateriaPrima { get; set; }
        public decimal QuantidadeMateriaPrima { get; set; }
        public Arquivos Arquivos { get; set; }

        public ItemProducao() { Arquivos = new Arquivos(); }

        public ItemProducao(int id, DateTime dataProducao, string materiaPrima, decimal quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id.ToString("00000")
                + DataProducao.ToString("dd/MM/yyyy").Replace("/", "")
                + MateriaPrima
                + QuantidadeMateriaPrima.ToString("000.00").Replace(",", ""); ;
        }

        public void IniciarProducao(string produto)
        {
            string msgInicial, msgSaida, valorVenda, ultimaVenda, codProd, nomeProd, qtProd, idLinha, escolha, nomeBusca, mprimaEncontrada = null, nomeMPrima, codMPrima, qtMPrima, codBusca;
            int idProducao;
            decimal qtMP = 0, qtPROD = 0;
            bool flag = true, flagInterna = true, flagPrincipal = true;
            List<string> mprimas = new();
            List<ItemProducao> listaItemProducao = new();
            ItemProducao itemProducao = null;

            codProd = produto.Substring(0, 13);
            nomeProd = produto.Substring(13, 20).Trim();
            ultimaVenda = produto.Substring(38, 8).Insert(2, "/").Insert(5, "/");
            valorVenda = produto.Substring(33, 5).Insert(3, ",");

            Console.WriteLine($"\n Codigo nº:      {codProd}");
            Console.WriteLine($" Nome:           {nomeProd}");
            Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
            Console.WriteLine($" Valor da Venda: {valorVenda}");

            if (Arquivos.VerificarArquivoVazio(Arquivos.IdProducao))
            {
                idLinha = Arquivos.RecuperaLinhaInteira(Arquivos.IdProducao, null, 0, 5);
                idProducao = int.Parse(idLinha);
                idProducao++;
            }
            else
            {
                idProducao = 1;
            }

            msgInicial = "\n ...:: Cadastrar Producao ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                flagInterna = true;

                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine("     ..:: Produto ::..\n");
                Console.WriteLine($" Codigo nº:      {codProd}");
                Console.WriteLine($" Nome:           {nomeProd}");
                Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
                Console.WriteLine($" Valor da Venda: {valorVenda}");
                Console.WriteLine(" -----------------------------\n");

                Console.WriteLine($" Producao nº {idProducao:00000}");
                Console.WriteLine($" Data de Producao: {DateTime.Now.Date:dd/MM/yyyy}");

                mprimas = Arquivos.MontarLista(Arquivos.MateriaPrima, "I", 42, 1);

                do
                {
                    Console.WriteLine(" -----------------------------\n");
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

                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");

                Console.WriteLine($" Codigo nº:      {codProd}");
                Console.WriteLine($" Nome:           {nomeProd}");
                Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
                Console.WriteLine($" Valor da Venda: {valorVenda}");
                Console.WriteLine(" -----------------------------\n");

                Console.WriteLine($" Producao nº {idProducao:00000}");
                Console.WriteLine($" Data de Producao: {DateTime.Now.Date:dd/MM/yyyy}");
                Console.WriteLine(" -----------------------------\n");

                codMPrima = mprimaEncontrada.Substring(0, 6);
                nomeMPrima = mprimaEncontrada.Substring(6, 20).Trim();

                Console.WriteLine($" Codigo: {codMPrima}");
                Console.WriteLine($" Nome:   {nomeMPrima}");

                do
                {
                    Console.Write(" Qtd. materia-prima (entre 1 e 999,99): ");
                    qtMPrima = Console.ReadLine();

                    if (string.IsNullOrEmpty(qtMPrima))
                    {
                        Console.WriteLine("\n xxxx Quantidade nao pode ser vazia.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else if (!decimal.TryParse(qtMPrima, out qtMP))
                    {
                        Console.WriteLine("\n xxxx Digite apenas numeros.");
                        Console.WriteLine("\n Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        decimal.TryParse(qtMPrima, out qtMP);

                        if (qtMP == 0)
                        {
                            Console.WriteLine("\n xxxx Quantidade nao pode ser 0.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else if (!(qtMP < 1000))
                        {
                            Console.WriteLine("\n xxxx Quantidade excedida. Maximo: 999,99.");
                            Console.WriteLine("\n Pressione ENTER para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");

                            Console.WriteLine($" Codigo nº:      {codProd}");
                            Console.WriteLine($" Nome:           {nomeProd}");
                            Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
                            Console.WriteLine($" Valor da Venda: {valorVenda}");
                            Console.WriteLine(" -----------------------------\n");

                            Console.WriteLine($" Producao nº {idProducao:00000}");
                            Console.WriteLine($" Data de Producao: {DateTime.Now.Date:dd/MM/yyyy}");
                            Console.WriteLine(" -----------------------------\n");

                            itemProducao = new(idProducao, DateTime.Now.Date, codMPrima, qtMP);
                            listaItemProducao.Add(itemProducao);

                            MontarLista(listaItemProducao);
                            flagInterna = false;
                        }
                    }
                } while (flagInterna);

                flagInterna = true;

                do
                {
                    Console.WriteLine(" Desejas: \n");
                    Console.WriteLine(" 1 - Adicionar mais materia-prima");
                    Console.WriteLine(" 2 - Finalizar adicao de materia-prima");
                    Console.Write("\n Escolha: ");
                    escolha = Console.ReadLine();

                    if (escolha == "9")
                        return;
                    else if (escolha != "1" && escolha != "2" && escolha != "9")
                    {
                        Console.WriteLine("\n xxxx Digite apenas '1' ou '2'.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...");
                        Console.ReadKey();
                    }
                    else if (escolha == "1")
                    {
                        flagInterna = false;
                    }
                    else if (escolha == "2")
                    {
                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");

                        Console.WriteLine($" Codigo nº:      {codProd}");
                        Console.WriteLine($" Nome:           {nomeProd}");
                        Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
                        Console.WriteLine($" Valor da Venda: {valorVenda}");
                        Console.WriteLine(" -----------------------------\n");

                        Console.WriteLine($" Producao nº {idProducao:00000}");
                        Console.WriteLine($" Data de Producao: {DateTime.Now.Date:dd/MM/yyyy}");
                        Console.WriteLine(" -----------------------------\n");

                        MontarLista(listaItemProducao);

                        do
                        {
                            Console.Write(" Qtd. de produto a ser produzido (entre 1 e 999,99): ");
                            qtProd = Console.ReadLine();

                            if (string.IsNullOrEmpty(qtProd))
                            {
                                Console.WriteLine("\n xxxx Quantidade nao pode ser vazia.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            else if (!decimal.TryParse(qtProd, out qtPROD))
                            {
                                Console.WriteLine("\n xxxx Digite apenas numeros.");
                                Console.WriteLine("\n Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                decimal.TryParse(qtProd, out qtPROD);

                                if (qtPROD == 0)
                                {
                                    Console.WriteLine("\n xxxx Quantidade nao pode ser 0.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else if (!(qtPROD < 1000))
                                {
                                    Console.WriteLine("\n xxxx Quantidade excedida. Maximo: 999,99.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(msgInicial);
                                    Console.WriteLine(msgSaida);
                                    Console.WriteLine(" -------------------------------------------------------------------------\n");

                                    Console.WriteLine($" Codigo nº:      {codProd}");
                                    Console.WriteLine($" Nome:           {nomeProd}");
                                    Console.WriteLine($" Ultima Venda:   {ultimaVenda}");
                                    Console.WriteLine($" Valor da Venda: {valorVenda}");
                                    Console.WriteLine(" -----------------------------\n");

                                    Console.WriteLine($" Producao nº {idProducao:00000}");
                                    Console.WriteLine($" Data de Producao: {DateTime.Now.Date:dd/MM/yyyy}");
                                    Console.WriteLine(" -----------------------------\n");

                                    Console.WriteLine($" Quantidade de produtos: {qtPROD:###.#0}");

                                    MontarLista(listaItemProducao);

                                    Console.WriteLine("\n Finalizar producao? 1 - SIM / 2 - NAO (ao escolher NAO, a producao sera cancelada)");
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
                                        Console.WriteLine("\n xxxx Producao cancelada.");
                                        Console.WriteLine("\n Pressione ENTER para voltar...");
                                        Console.ReadKey();
                                        return;
                                    }
                                    else if (escolha == "1")
                                    {
                                        escolha = "100";
                                        flag = false;
                                        flagInterna = false;
                                        flagPrincipal = false;
                                    }
                                }
                            }
                        } while (flag);
                    }
                } while (flagInterna);
            } while (flagPrincipal);

            if (escolha == "100")
            {
                Producao producaoFeita = new(idProducao, DateTime.Now.Date, codProd, qtPROD);

                Arquivos.DeletarArquivo(Arquivos.IdProducao);
                Arquivos.Gravar(idProducao.ToString("00000"), Arquivos.IdProducao);
                listaItemProducao.ForEach(item =>
                {
                    Arquivos.Gravar(item.ToString(), Arquivos.ItemProducao);
                });

                Arquivos.Gravar(producaoFeita.ToString(), Arquivos.Producao);

                Console.WriteLine("\n Producao concluida com sucesso!");
                Console.WriteLine("\n Pressione ENTER para voltar...");
                Console.ReadKey();
                flagPrincipal = false;
            }
        }

        public void MontarLista(List<ItemProducao> itensProducao)
        {
            Console.WriteLine("\n Cod.       Materia-prima            Qt.  ");
            Console.WriteLine(" -------------------------------------------\n");
            itensProducao.ForEach(item =>
            {
                string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.MateriaPrima, 0, 6);
                string nome = linha.Substring(6, 20).Trim();
                Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')} {item.QuantidadeMateriaPrima,8:##0.#0}");
            });
            Console.WriteLine("\n --------------------------------------------\n");
        }
    }
}
