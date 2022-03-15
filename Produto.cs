using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Produto
    {
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public Arquivos Arquivos { get; set; }

        public Produto() { Arquivos = new Arquivos(); }

        public Produto(string codigoBarras, string nome, decimal valorVenda, DateTime ultimaVenda, DateTime dataCadastro, char situacao)
        {
            CodigoBarras = codigoBarras;
            Nome = nome;
            ValorVenda = valorVenda;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return CodigoBarras
                + Nome.PadLeft(20, ' ')
                + ValorVenda.ToString("000.00").Replace(",", "")
                + UltimaVenda.ToString("dd/MM/yyyy").Replace("/", "")
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
                Console.WriteLine("\n .....:::: Menu Produto ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Produto");
                Console.WriteLine(" 2 - Editar Produto");
                Console.WriteLine(" 3 - Mostrar Produtos");
                Console.WriteLine(" -----------------------------");
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
                    }
                }
            } while (flag);
        }

        public void Cadastrar()
        {
            string msgInicial, msgSaida, nome, cod, valorRecebido;
            bool flagPrincipal = true, flagInterna = true;
            int id = 0;
            decimal valorVenda = 0;

            msgInicial = "\n ...:: Cadastrar Produto ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");

                do
                {
                    Console.Write(" Codigo do produto: ");
                    cod = Console.ReadLine();

                    if (cod == "9")
                        return;
                    else if (cod.Length != 13)
                    {
                        Console.WriteLine("\n xxxx O codigo precisa ter 13 digitos e comecar com 789.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (cod.Substring(0, 3) != "789")
                    {
                        Console.WriteLine("\n xxxx O codigo precisa ter 13 digitos e comecar com 789.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(
                                Arquivos.RecuperaLinhaInteira(Arquivos.Produto, cod, 0, 13)))
                        {
                            Console.WriteLine("\n xxxx O codigo ja esta cadastrado.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else
                            flagInterna = false;
                    }
                } while (flagInterna);

                flagInterna = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine($" Codigo: {cod}");
                    Console.Write(" Nome (maximo: 20 caracteres): ");
                    nome = Console.ReadLine();

                    if (nome == "9")
                        return;
                    else if (nome.Length > 20)
                    {
                        Console.WriteLine("\n xxxx Tamanho do nome excedeu o limite (maximo: 20 caracteres).");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (string.IsNullOrEmpty(nome))
                    {
                        Console.WriteLine("\n xxxx O nome nao pode ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                        flagInterna = false;

                } while (flagInterna);

                flagInterna = true;

                do
                {
                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine($" Codigo: {cod}");
                    Console.WriteLine($" Nome: {nome}");
                    Console.Write(" Valor de Venda: ");
                    valorRecebido = Console.ReadLine();

                    if (valorRecebido == "9")
                        return;
                    else if (string.IsNullOrEmpty(valorRecebido))
                    {
                        Console.WriteLine("\n xxxx Valor de venda nao pode ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else if (!decimal.TryParse(valorRecebido, out valorVenda))
                    {
                        Console.WriteLine("\n xxxx Valor de venda so aceita numeros e virgula.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        valorVenda = decimal.Parse(valorRecebido);

                        if (!(valorVenda < 1000))
                        {
                            Console.WriteLine("\n xxxx Valor de venda ultrapassou o valor maximo de R$ 999,99.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                            Console.ReadKey();
                        }
                        else
                            flagInterna = false;
                    }

                } while (flagInterna);

                do
                {
                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine($" Codigo: {cod}");
                    Console.WriteLine($" Nome: {nome}");
                    Console.WriteLine($" Valor da venda: {valorVenda.ToString("###.#0")}");
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
                        Produto tempProduto = new Produto(cod, nome, valorVenda, DateTime.Now.Date, DateTime.Now.Date, 'A'); ;

                        Arquivos.Gravar(tempProduto.ToString(), Arquivos.Produto);

                        Console.WriteLine("\n oooo Produto cadastrado com sucesso.");
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
            string msgInicial, msgSaida, linha, nome, idDigitado, cod;
            char situacao;
            int opcao = 0, id;
            decimal valorVenda = 0;

            msgInicial = "\n ...:: Editar Produto ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma produto cadastrado.");
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
                    Console.Write(" Para comecar, digite o codigo do produto: ");
                    cod = Console.ReadLine();

                    if (cod == "9")
                    {
                        return;
                    }
                    else if (string.IsNullOrEmpty(cod))
                    {
                        Console.WriteLine("\n xxxx O codigo nao pode ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                        flagInterna = false;
                } while (flagInterna);

                linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, cod, 0, 13);

                if (string.IsNullOrEmpty(linha))
                {
                    Console.WriteLine("\n xxxx O codigo do produto informado nao existe.");
                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                    Console.ReadKey();
                }
                else
                {
                    do
                    {
                        cod = linha.Substring(0, 13);
                        nome = linha.Substring(13, 20).Trim();
                        valorVenda = decimal.Parse(linha.Substring(33, 5).Insert(3, ","));
                        situacao = char.Parse(linha.Substring(54, 1));

                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine($" Codigo: {cod}");
                        Console.WriteLine($" Nome: {nome}");
                        Console.WriteLine($" Valor de venda: {valorVenda}");
                        Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");
                        Console.WriteLine("\n Qual campo deseja alterar? Apos escolha, nao sera possivel retornar ate terminar o processo!");
                        Console.WriteLine(" 1 - Nome");
                        Console.WriteLine(" 2 - Valor de venda");
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
                                Console.WriteLine("\n ...:: Alteracao do Nome ::...\n");
                                Console.WriteLine($" Codigo: {cod}");
                                Console.WriteLine($"\n Nome: {nome}");
                                Console.Write(" Novo nome: ");
                                nome = Console.ReadLine();

                                if (nome.Length > 20)
                                {
                                    Console.WriteLine("\n xxxx Tamanho do nome excedeu o limite (maximo: 20 caracteres).");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else if (string.IsNullOrEmpty(nome))
                                {
                                    Console.WriteLine("\n xxxx O nome nao pode ser vazio.");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Produto tempProduto = new Produto(cod, nome, valorVenda,
                                          DateTime.Parse(linha.Substring(38, 8).Insert(2, "/").Insert(5, "/")),
                                          DateTime.Parse(linha.Substring(46, 8).Insert(2, "/").Insert(5, "/")),
                                          situacao);

                                    Arquivos.AlterarDocumento(Arquivos.Produto, tempProduto.ToString(), cod, 0, 13);

                                    Console.WriteLine("\n Nome alterado.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    flagInterna = false;
                                    return;
                                }
                            } while (flagInterna);
                        }
                        else if (opcao == 2)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("\n ...:: Alteracao do Valor de Venda ::...\n");
                                Console.WriteLine($" Codigo: {cod}");
                                Console.WriteLine($" Nome: {nome}");
                                Console.WriteLine($"\n Valor de venda: {valorVenda}");
                                Console.Write(" Novo valor de venda: ");
                                string valorRecebido = Console.ReadLine();

                                if (valorRecebido == "9")
                                    return;
                                else if (string.IsNullOrEmpty(valorRecebido))
                                {
                                    Console.WriteLine("\n xxxx Valor de venda nao pode ser vazio.");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else if (!decimal.TryParse(valorRecebido, out valorVenda))
                                {
                                    Console.WriteLine("\n xxxx Valor de venda so aceita numeros e virgula.");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    valorVenda = decimal.Parse(valorRecebido);

                                    if (!(valorVenda < 1000))
                                    {
                                        Console.WriteLine("\n xxxx Valor de venda ultrapassou o valor maximo de R$ 999,99.");
                                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        flagInterna = false;

                                        Produto tempProduto = new Produto(cod, nome, valorVenda,
                                              DateTime.Parse(linha.Substring(38, 8).Insert(2, "/").Insert(5, "/")),
                                              DateTime.Parse(linha.Substring(46, 8).Insert(2, "/").Insert(5, "/")),
                                              situacao);

                                        Arquivos.AlterarDocumento(Arquivos.Produto, tempProduto.ToString(), cod, 0, 13);

                                        Console.WriteLine("\n Valor de venda alterado.");
                                        Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                        Console.ReadKey();
                                        return;
                                    }
                                }
                            } while (flagInterna);
                        }
                        else if (opcao == 3)
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("\n ...:: Alteracao da Situacao ::...\n");
                                Console.WriteLine($" Codigo: {cod}");
                                Console.WriteLine($" Nome: {nome}");
                                Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");

                                if (situacao == 'A')
                                    situacao = 'I';
                                else
                                    situacao = 'A';

                                Console.WriteLine($"\n Nova situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");

                                Produto tempProduto = new Produto(cod, nome, valorVenda,
                                          DateTime.Parse(linha.Substring(38, 8).Insert(2, "/").Insert(5, "/")),
                                          DateTime.Parse(linha.Substring(46, 8).Insert(2, "/").Insert(5, "/")),
                                          situacao);

                                Arquivos.AlterarDocumento(Arquivos.Produto, tempProduto.ToString(), cod, 0, 13);

                                Console.WriteLine("\n Situacao alterada.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                                return;

                            } while (flagInterna);
                        }
                    } while (flagPrincipal);
                }
            }
        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Produto))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum produto cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Produto");
            }
        }
    }
}

