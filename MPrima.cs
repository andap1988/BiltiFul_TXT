using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class MPrima
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public Arquivos Arquivos { get; set; }

        public MPrima() { Arquivos = new Arquivos(); }

        public MPrima(string id, string nome, DateTime ultimaCompra, DateTime dataCadastro, char situacao)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = ultimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return Id
                + Nome.PadLeft(20, ' ')
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
                Console.WriteLine("\n .....:::: Menu Materia-prima ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Materia-prima");
                Console.WriteLine(" 2 - Editar Materia-prima");
                Console.WriteLine(" 3 - Mostrar Materias-primas");
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
            string msgInicial, msgSaida, nome, idLinha;
            bool flagPrincipal = true, flagInterna = true;
            int id;

            if (Arquivos.VerificarArquivoVazio(Arquivos.IdMPrima))
            {
                idLinha = Arquivos.RecuperaLinhaInteira(Arquivos.IdMPrima, "MP", 0, 2);
                id = int.Parse(idLinha.Substring(2, 4));
                id++;
            }
            else
            {
                id = 1;
            }

            msgInicial = "\n ...:: Cadastrar Materia-prima ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            do
            {
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");

                Console.WriteLine($" Id: MP{id.ToString("0000")}");
                do
                {
                    Console.Write(" Nome (maximo: 20 caracteres): ");
                    nome = Console.ReadLine();

                    if (nome == "9")
                        return;
                    else if (nome.Length > 50)
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
                    Console.WriteLine($" Id: MP{id.ToString("0000")}");
                    Console.WriteLine($" Nome: {nome}");
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
                        MPrima tempMPrima = new MPrima("MP" + id.ToString("0000"), nome, DateTime.Now.Date, DateTime.Now.Date, 'A');

                        Arquivos.Gravar(tempMPrima.ToString(), Arquivos.MateriaPrima);
                        Arquivos.DeletarArquivo(Arquivos.IdMPrima);
                        Arquivos.Gravar("MP" + id.ToString("0000"), Arquivos.IdMPrima);

                        Console.WriteLine("\n oooo Materia-prima cadastrada com sucesso.");
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
            string msgInicial, msgSaida, linha, nome, idDigitado;
            char situacao;
            int opcao = 0, id;

            msgInicial = "\n ...:: Editar Materia-prima ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.MateriaPrima))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma materia-prima cadastrada.");
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
                    Console.Write(" Para comecar, digite o codigo da materia-prima (MP + numero ou somente numeros): ");
                    idDigitado = Console.ReadLine();

                    if (idDigitado == "9")
                    {
                        return;
                    }
                    else if (string.IsNullOrEmpty(idDigitado))
                    {
                        Console.WriteLine("\n xxxx O ID nao pode ser vazio.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                        flagInterna = false;
                } while (flagInterna);

                if (idDigitado.Contains("MP"))
                    id = int.Parse(idDigitado.Remove(0, 2));

                id = int.Parse(idDigitado);
                flagInterna = true;

                linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, "MP" + id.ToString("0000"), 0, 6);

                if (string.IsNullOrEmpty(linha))
                {
                    Console.WriteLine("\n xxxx O ID informado nao existe.");
                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                    Console.ReadKey();
                }
                else
                {
                    do
                    {
                        string idFormatado = linha.Substring(0, 6);
                        nome = linha.Substring(6, 20).Trim();
                        situacao = char.Parse(linha.Substring(42, 1));

                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");
                        Console.WriteLine($" ID: {idFormatado}");
                        Console.WriteLine($" Nome: {nome}");
                        Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");
                        Console.WriteLine("\n Qual campo deseja alterar? Apos escolha, nao sera possivel retornar ate terminar o processo!");
                        Console.WriteLine(" 1 - Nome");
                        Console.WriteLine(" 2 - Situacao\n");
                        Console.Write(" Escolha: ");
                        int.TryParse(Console.ReadLine(), out opcao);

                        if (opcao == 9)
                            return;
                        else if (opcao < 1 || opcao > 2)
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
                                Console.WriteLine($" ID: {idFormatado}");
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
                                    MPrima tempMPrima = new MPrima(idFormatado, nome,
                                          DateTime.Parse(linha.Substring(26, 8).Insert(2, "/").Insert(5, "/")),
                                          DateTime.Parse(linha.Substring(34, 8).Insert(2, "/").Insert(5, "/")),
                                          situacao);

                                    Arquivos.AlterarDocumento(Arquivos.MateriaPrima, tempMPrima.ToString(), idFormatado, 0, 6);

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
                                Console.WriteLine("\n ...:: Alteracao da Situacao ::...\n");
                                Console.WriteLine($" ID: {idFormatado}");
                                Console.WriteLine($" Nome: {nome}");
                                Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");

                                if (situacao == 'A')
                                    situacao = 'I';
                                else
                                    situacao = 'A';

                                Console.WriteLine($"\n Nova situacao: {(situacao == 'A' ? "Ativo" : "Inativo")}");

                                MPrima tempMPrima = new MPrima(idFormatado, nome,
                                           DateTime.Parse(linha.Substring(26, 8).Insert(2, "/").Insert(5, "/")),
                                           DateTime.Parse(linha.Substring(34, 8).Insert(2, "/").Insert(5, "/")),
                                           situacao);

                                Arquivos.AlterarDocumento(Arquivos.MateriaPrima, tempMPrima.ToString(), idFormatado, 0, 6);

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

        public void AlterarDataUltimaCompra(string dataUltimaCompra, string codigoMPrima)
        {
            string linhaMPrima = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, codigoMPrima, 0, 6);
            string novaMPrima = linhaMPrima.Substring(0, 6)
                               + linhaMPrima.Substring(6, 20)
                               + dataUltimaCompra
                               + linhaMPrima.Substring(34, 8)
                               + linhaMPrima.Substring(42, 1);

            Arquivos.AlterarDocumento(Arquivos.MateriaPrima, dataUltimaCompra, codigoMPrima, 0, 6, true);
            Arquivos.AlterarDocumento(Arquivos.MateriaPrima, novaMPrima, codigoMPrima, 0, 6);
        }

        public void Imprimir()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.MateriaPrima))
            {
                Console.WriteLine("\n xxxx Nao ha nenhuma materia-prima cadastrada.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Materia");
            }
        }
    }
}
