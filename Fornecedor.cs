using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Fornecedor
    {

        public decimal CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public Arquivos Arquivos { get; set; }

        public Fornecedor() { Arquivos = new Arquivos(); }

        public Fornecedor(decimal cNPJ, string razaoSocial, DateTime dataAbertura, DateTime ultimaCompra, DateTime dataCadastro, char situacao)
        {
            CNPJ = cNPJ;
            RazaoSocial = razaoSocial;
            DataAbertura = dataAbertura;
            UltimaCompra = ultimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
            Arquivos = new Arquivos();
        }


        public override string ToString()
        {
            return CNPJ.ToString("00000000000000")
                + RazaoSocial.PadLeft(50, ' ')
                + DataAbertura.ToString("dd/MM/yyyy").Replace("/", "")
                + UltimaCompra.ToString("dd/MM/yyyy").Replace("/", "")
                + DataCadastro.ToString("dd/MM/yyyy").Replace("/", "")
                + Situacao;
        }

        public void Menu()
        {
            int option = 0;
            bool flag = true;

            do
            {
                Console.Clear();
                Console.WriteLine("\n .....:::: Menu Fornecedor ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Fornecedor");
                Console.WriteLine(" 2 - Editar Fornecedor");
                Console.WriteLine(" 3 - Mostrar Fornecedores");
                Console.WriteLine(" 4 - Fornecedores Bloqueados");
                Console.WriteLine(" ----------------------------");
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
                    switch (option)
                    {
                        case 1:
                            Cadastrar();
                            break;
                        case 2:
                            Editar();
                            break;
                        case 3:
                            Imprimir();
                            break;
                        case 4:
                            Bloqueado();
                            break;
                    }
                }
            } while (flag);
        }

        public void Cadastrar()
        {
            string msgInicial, msgSaida, razaoSocial, cnpj;
            bool flagPrincipal = true, flagInterna = true;
            DateTime dtAbertura;

            msgInicial = "\n ...:: Cadastrar Fornecedor ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                do
                {
                    Console.Write(" Para comecar, digite seu CNPJ: ");
                    cnpj = Console.ReadLine();
                    if (cnpj == "9")
                    {
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14)))
                        {
                            Console.WriteLine("\n xxxx CNPJ ja cadastrado.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else if (!Validacoes.ValidarCNPJ(cnpj))
                        {
                            Console.WriteLine("\n xxxx CNPJ invalido.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                            Console.ReadKey();
                        }
                        else
                            flagInterna = false;
                    }
                } while (flagInterna);

                flagInterna = true;
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CNPJ: {cnpj}");

                do
                {
                    Console.Write(" Razao Social (maximo: 50 caracteres): ");
                    razaoSocial = Console.ReadLine();

                    if (razaoSocial == "9")
                        return;
                    else if (razaoSocial.Length > 50)
                    {
                        Console.WriteLine("\n xxxx Tamanho da razao social excedeu o limite (maximo: 50 caracteres).");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (string.IsNullOrEmpty(razaoSocial))
                    {
                        Console.WriteLine("\n xxxx A razao social nao pode ser vazia.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                        flagInterna = false;
                } while (flagInterna);

                flagInterna = true;
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CNPJ: {cnpj}");
                Console.WriteLine($" Razao Social: {razaoSocial}");

                do
                {
                    Console.Write(" Data de Abertura: ");
                    string date = Console.ReadLine();

                    if (date == "9")
                    {
                        flagPrincipal = false;
                        return;
                    }
                    else
                    {
                        if (!date.Contains("/"))
                        {
                            date = date.Insert(2, "/").Insert(5, "/");
                        }

                        dtAbertura = Convert.ToDateTime(date);
                        int meses = Convert.ToInt32(Math.Floor(DateTime.Today.Subtract(dtAbertura).TotalDays / 30));

                        if (meses < 6)
                        {
                            Console.WriteLine("\n xxxx So podemos cadastrar fornecedores com mais 6 meses de abertura.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
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
                    Console.WriteLine($" CNPJ: {cnpj}");
                    Console.WriteLine($" Razao Social: {razaoSocial}");
                    Console.WriteLine($" Data de Abertura: {dtAbertura:dd/MM/yyyy}");

                    Console.Write("\n Confirmar cadastro? (1 - SIM / 2 - NAO): ");
                    string escolha = Console.ReadLine();

                    if (escolha != "1" && escolha != "2")
                    {
                        Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (escolha == "2")
                    {
                        Console.WriteLine("\n xxxx O cadastro foi cancelado pelo usuario.");
                        Console.WriteLine("\n Pressione ENTER para voltar...\n");
                        Console.ReadKey();
                        return;
                    }
                    else if (escolha == "1")
                    {
                        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                        Fornecedor fornecedor = new(decimal.Parse(cnpj), razaoSocial, dtAbertura, DateTime.Now.Date, DateTime.Now.Date, 'A');
                        Arquivos.Gravar(fornecedor.ToString(), Arquivos.Fornecedor);

                        Console.WriteLine("\n oooo Fornecedor cadastrado com sucesso.");
                        Console.WriteLine("\n Pressione ENTER para voltar...\n");
                        Console.ReadKey();

                        flagInterna = false;
                        flagPrincipal = false;
                    }

                } while (flagInterna);

            } while (flagPrincipal);
        }

        public void Editar()
        {
            bool flagPrincipal = true, flagInterna = true;
            string cnpj, msgInicial, msgSaida, linha, razaoSocial;
            char situacao;
            DateTime dataAbertura;
            int opcao = 0;

            msgInicial = "\n ...:: Editar Fornecedor ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Fornecedor))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum fornecedor cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");

                do
                {
                    Console.Write(" Para comecar, digite seu CNPJ: ");
                    cnpj = Console.ReadLine();
                    if (cnpj == "9")
                    {
                        return;
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14)))
                        {
                            Console.WriteLine("\n xxxx CNPJ nao encontrado.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else if (!Validacoes.ValidarCNPJ(cnpj))
                        {
                            Console.WriteLine("\n xxxx CNPJ invalido.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                            Console.ReadKey();
                        }
                        else
                            flagInterna = false;
                    }

                } while (flagInterna);

                flagInterna = true;
                do
                {
                    cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                    linha = Arquivos.RecuperaLinhaInteira(Arquivos.Fornecedor, cnpj, 0, 14);
                    cnpj = cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                    razaoSocial = linha.Substring(14, 50).Trim();
                    dataAbertura = DateTime.Parse(linha.Substring(64, 8).Insert(2, "/").Insert(5, "/"));
                    situacao = char.Parse(linha.Substring(88, 1));

                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine($" CNPJ: {cnpj}");
                    Console.WriteLine($" Razao Social: {razaoSocial}");
                    Console.WriteLine($" Data de Abertura: {dataAbertura:dd/MM/yyyy}");
                    Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Bloqueado")}");
                    Console.WriteLine("\n Qual campo deseja alterar? Apos escolha, nao sera possivel retornar ate terminar o processo!");
                    Console.WriteLine(" 1 - Razao Social");
                    Console.WriteLine(" 2 - Data Abertura");
                    Console.WriteLine(" 3 - Situacao\n");
                    Console.Write(" Escolha: ");
                    int.TryParse(Console.ReadLine(), out opcao);

                    if (opcao == 9)
                        return;
                    else if (opcao < 1 || opcao > 3)
                    {
                        Console.WriteLine("\n Opcao invalida.\n");
                        Console.WriteLine(" Pressione ENTER para voltar...");
                        Console.ReadKey();
                    }
                    else if (opcao == 1)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n ...:: Alteracao da Razao Social ::...\n");
                            Console.WriteLine($" CNPJ: {cnpj}");
                            Console.WriteLine($"\n Atual razao social: {linha.Substring(14, 50).Trim()}");
                            Console.Write(" Nova razao social: ");
                            razaoSocial = Console.ReadLine();

                            if (razaoSocial.Length > 50)
                            {
                                Console.WriteLine("\n xxxx Tamanho da razao social excedeu o limite (maximo: 50 caracteres).");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else if (string.IsNullOrEmpty(razaoSocial))
                            {
                                Console.WriteLine("\n xxxx A razao social nao pode ser vazia.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else
                            {
                                cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                                Fornecedor tempForn = new(decimal.Parse(cnpj), razaoSocial, dataAbertura,
                                    DateTime.Parse(linha.Substring(72, 8).Insert(2, "/").Insert(5, "/")),
                                    DateTime.Parse(linha.Substring(80, 8).Insert(2, "/").Insert(5, "/")),
                                    char.Parse(linha.Substring(88, 1)));

                                Arquivos.AlterarDocumento(Arquivos.Fornecedor, tempForn.ToString(), cnpj, 0, 14);

                                Console.WriteLine("\n Razao social alterada.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }
                        } while (flagInterna);
                    }
                    else if (opcao == 2)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n ...:: Alteracao da Data de Abertura ::...\n");
                            Console.WriteLine($" CNPJ: {cnpj}");
                            Console.WriteLine($" Razao social: {razaoSocial}");
                            Console.WriteLine($"\n Data de abertura: {dataAbertura:dd/MM/yyyy}");
                            Console.Write(" Nova data de abertura: ");
                            string date = Console.ReadLine();

                            if (!date.Contains("/"))
                            {
                                date = date.Insert(2, "/").Insert(5, "/");
                            }

                            dataAbertura = Convert.ToDateTime(date);
                            int meses = Convert.ToInt32(Math.Floor(DateTime.Today.Subtract(dataAbertura).TotalDays / 30));

                            if (meses < 6)
                            {
                                Console.WriteLine("\n xxxx So podemos cadastrar fornecedores com mais 6 meses de abertura.");
                                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                                Fornecedor tempForn = new(decimal.Parse(cnpj), razaoSocial, dataAbertura,
                                    DateTime.Parse(linha.Substring(72, 8).Insert(2, "/").Insert(5, "/")),
                                    DateTime.Parse(linha.Substring(80, 8).Insert(2, "/").Insert(5, "/")),
                                    char.Parse(linha.Substring(88, 1)));

                                Arquivos.AlterarDocumento(Arquivos.Fornecedor, tempForn.ToString(), cnpj, 0, 14);

                                Console.WriteLine("\n Data de abertura alterada.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }
                        } while (flagInterna);
                    }
                    else if (opcao == 3)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n ...:: Alteracao da Situacao ::...\n");
                            Console.WriteLine($" CNPJ: {cnpj}");
                            Console.WriteLine($" Razao social: {razaoSocial}");
                            Console.WriteLine($" Data de abertura: {dataAbertura:dd/MM/yyyy}");
                            Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Bloqueado")}");

                            if (situacao == 'A')
                                situacao = 'I';
                            else
                                situacao = 'A';

                            Console.WriteLine($"\n Nova situacao: {(situacao == 'A' ? "Ativo" : "Bloqueado")}");

                            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                            Fornecedor tempForn = new(decimal.Parse(cnpj), razaoSocial, dataAbertura,
                                DateTime.Parse(linha.Substring(72, 8).Insert(2, "/").Insert(5, "/")),
                                DateTime.Parse(linha.Substring(80, 8).Insert(2, "/").Insert(5, "/")),
                                situacao);

                            if (situacao == 'I')
                            {
                                Arquivos.AlterarDocumento(Arquivos.Fornecedor, tempForn.ToString(), cnpj, 0, 14, true);
                                Arquivos.Gravar(tempForn.ToString(), Arquivos.Bloqueado);
                                Console.WriteLine("\n Situacao alterada.");
                                Console.WriteLine("\n Fornecedor incluido na lista de bloqueado.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            Console.ReadKey();
                            return;

                        } while (flagInterna);
                    }
                } while (flagPrincipal);
            }
        }

        public void Bloqueado()
        {
            string msgInicial, msgSaida, cnpj, linha, razaoSocial, escolha;
            bool flagInterna = true, flagPrincipal = true;
            DateTime dataAbertura, ultimaCompra;
            char situacao;

            msgInicial = "\n ...:: Fornecedores Bloqueados ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Bloqueado))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum fornrcedor cadastrado como bloqueado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
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
                    Console.WriteLine(" 1 - Mostrar Bloqueados");
                    Console.WriteLine(" 2 - Retirar um fornecedor do cadastro de bloqueio");
                    Console.Write("\n Escolha: ");
                    escolha = Console.ReadLine();

                    if (escolha == "9")
                        return;
                    else if (escolha != "1" && escolha != "2" && escolha != "9")
                    {
                        Console.WriteLine("\n xxxx Digite apenas '1' ou '2'.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (escolha == "1")
                    {
                        ImprimirBloqueados();
                    }
                    else if (escolha == "2")
                    {

                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");

                        do
                        {
                            Console.Write(" Para comecar, digite seu CNPJ: ");
                            cnpj = Console.ReadLine();
                            if (cnpj == "9")
                            {
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, cnpj.Replace(".", "").Replace("-", "").Replace("/", ""), 0, 14)))
                                {
                                    Console.WriteLine("\n xxxx CNPJ nao encontrado no cadastro de bloqueados.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else if (!Validacoes.ValidarCNPJ(cnpj))
                                {
                                    Console.WriteLine("\n xxxx CNPJ invalido.");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }
                        } while (flagInterna);

                        flagInterna = true;

                        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                        linha = Arquivos.RecuperaLinhaInteira(Arquivos.Bloqueado, cnpj, 0, 14);
                        cnpj = cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                        razaoSocial = linha.Substring(14, 50).Trim();
                        dataAbertura = DateTime.Parse(linha.Substring(64, 8).Insert(2, "/").Insert(5, "/"));
                        ultimaCompra = DateTime.Parse(linha.Substring(72, 8).Insert(2, "/").Insert(5, "/"));
                        situacao = char.Parse(linha.Substring(88, 1));

                        do
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" CNPJ: {cnpj}");
                            Console.WriteLine($" Razao Social: {razaoSocial}");
                            Console.WriteLine($" Data de Abertura: {dataAbertura:dd/MM/yyyy}");
                            Console.WriteLine($" Data da Ultima Compra: {ultimaCompra:dd/MM/yyyy}");
                            Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Bloqueado")}");
                            Console.WriteLine("\n Deseja retirar esse fornecedor do cadastro de bloqueados?\n");
                            Console.WriteLine(" Digite 1 - Sim / 2 - Nao");
                            Console.Write("\n Escolha: ");
                            escolha = Console.ReadLine();

                            if (escolha == "9")
                                return;
                            else if (escolha != "1" && escolha != "2" && escolha != "9")
                            {
                                Console.WriteLine("\n xxxx Digite apenas '1' para SIM ou '2' para NAO.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else if (escolha == "2")
                            {
                                Console.WriteLine("\n xxxx Acao cancelada pelo usuario.");
                                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                Console.ReadKey();
                                flagInterna = false;
                                return;
                            }
                            else if (escolha == "1")
                            {
                                situacao = 'A';
                                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

                                Fornecedor tempFornecedor = new(decimal.Parse(cnpj), razaoSocial, dataAbertura, ultimaCompra,
                                            DateTime.Parse(linha.Substring(80, 8).Insert(2, "/").Insert(5, "/")),
                                            situacao);

                                Arquivos.AlterarDocumento(Arquivos.Bloqueado, tempFornecedor.ToString(), cnpj, 0, 14, true);
                                Arquivos.Gravar(tempFornecedor.ToString(), Arquivos.Fornecedor);

                                Console.WriteLine("\n Situacao alterada.");
                                Console.WriteLine("\n Fornecedor excluido da lista de bloqueados.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;
                            }
                        } while (flagInterna);
                    }
                } while (flagPrincipal);
            }
        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Fornecedor))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum fornecedor cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Fornecedor");
            }
        }

        public void ImprimirBloqueados()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Bloqueado))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum fornecedor cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Bloqueado");
            }
        }
    }
}
