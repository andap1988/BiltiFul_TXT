using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Cliente
    {

        public decimal CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public Arquivos Arquivos { get; set; }

        public Cliente() { Arquivos = new Arquivos(); }

        public Cliente(decimal cpf, string nome, DateTime dataNascimento, char sexo, DateTime ultimaCompra, DateTime dataCadastro, char situacao)
        {
            CPF = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            UltimaCompra = ultimaCompra;
            DataCadastro = dataCadastro;
            Situacao = situacao;
            Arquivos = new Arquivos();
        }

        public override string ToString()
        {
            return CPF.ToString("00000000000")
                + Nome.PadLeft(50, ' ')
                + DataNascimento.ToString("dd/MM/yyyy").Replace("/", "")
                + Sexo
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
                Console.WriteLine("\n .....:::: Menu Cliente ::::.....\n");
                Console.WriteLine(" 1 - Cadastrar Cliente");
                Console.WriteLine(" 2 - Editar Cliente");
                Console.WriteLine(" 3 - Mostrar Clientes");
                Console.WriteLine(" 4 - Clientes Inadimplentes");
                Console.WriteLine(" ---------------------------");
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
                            Inadimplentes();
                            break;
                    }
                }
            } while (flag);
        }

        public void Cadastrar()
        {
            string msgInicial, msgSaida, nome, cpf, sexoString;
            bool flagPrincipal = true, flagInterna = true;
            DateTime dtNasc;
            char sexo = 'N';

            msgInicial = "\n ...:: Cadastrar Cliente ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";
            do
            {
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                do
                {
                    Console.Write(" Para comecar, digite seu CPF: ");
                    cpf = Console.ReadLine();
                    if (cpf == "9")
                        return;
                    else
                    {
                        if (!string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                        {
                            Console.WriteLine("\n xxxx CPF ja cadastrado.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else if (!Validacoes.ValidarCPF(cpf))
                        {
                            Console.WriteLine("\n xxxx CPF invalido.");
                            Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                            Console.ReadKey();
                        }
                        else
                            flagInterna = false;
                    }
                } while (flagInterna);

                if (!cpf.Contains("."))
                    cpf = cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");

                flagInterna = true;
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CPF: {cpf}");

                do
                {
                    Console.Write(" Nome (maximo: 50 caracteres): ");
                    nome = Console.ReadLine();

                    if (nome == "9")
                        return;
                    else if (nome.Length > 50)
                    {
                        Console.WriteLine("\n xxxx Tamanho do nome excedeu o limite (maximo: 50 caracteres).");
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
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CPF: {cpf}");
                Console.WriteLine($" Nome: {nome}");

                do
                {
                    Console.Write(" Data de Nasc.: ");
                    string date = Console.ReadLine();

                    if (date == "9")
                        return;
                    else
                    {
                        if (!date.Contains("/"))
                        {
                            date = date.Insert(2, "/").Insert(5, "/");
                        }

                        dtNasc = Convert.ToDateTime(date);
                        int idade = Convert.ToInt32(Math.Floor(DateTime.Today.Subtract(dtNasc).TotalDays / 365));
                        dtNasc = dtNasc.Date;

                        if (idade < 18)
                        {
                            Console.WriteLine("\n xxxx So podemos cadastrar maiores de 18 anos.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            flagInterna = false;
                            flagPrincipal = false;
                            return;
                        }
                        else
                        {
                            flagInterna = false;
                        }
                    }
                } while (flagInterna);

                flagInterna = true;
                Console.Clear();
                Console.WriteLine(msgInicial);
                Console.WriteLine(msgSaida);
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                Console.WriteLine($" CPF: {cpf}");
                Console.WriteLine($" Nome: {nome}");
                Console.WriteLine($" Data de Nasc.: {dtNasc:dd/MM/yyyy}");

                do
                {
                    Console.Write(" Sexo (F - feminino / M - masculino): ");
                    sexoString = Console.ReadLine().ToUpper();

                    if (sexoString == "9")
                        return;
                    else if (sexoString != "M" && sexoString != "F")
                    {
                        Console.WriteLine("\n xxxx Digite apenas 'M' para masculino ou 'F' para feminino.");
                        Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        sexo = char.Parse(sexoString);
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
                    Console.WriteLine($" CPF: {cpf}");
                    Console.WriteLine($" Nome: {nome}");
                    Console.WriteLine($" Data de Nasc.: {dtNasc:dd/MM/yyyy}");
                    Console.WriteLine($" Sexo: {(sexo == 'F' ? "Feminino" : "Masculino")}");

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
                        flagInterna = false;
                        flagPrincipal = false;
                        return;
                    }
                    else if (escolha == "1")
                    {
                        cpf = cpf.Replace(".", "").Replace("-", "");

                        Cliente cliente = new(decimal.Parse(cpf), nome, dtNasc, sexo, DateTime.Now.Date, DateTime.Now.Date, 'A');
                        Arquivos.Gravar(cliente.ToString(), Arquivos.Cliente);

                        Console.WriteLine("\n oooo Cliente cadastrado com sucesso.");
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
            string cpf, msgInicial, msgSaida, linha, nome;
            char situacao, sexo;
            DateTime dataNasc;
            int opcao = 0;

            msgInicial = "\n ...:: Editar Cliente ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Cliente))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum cliente cadastrado.");
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
                    Console.Write(" Para comecar, digite seu CPF: ");
                    cpf = Console.ReadLine();
                    if (cpf == "9")
                    {
                        return;
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(
                            Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                        {
                            Console.WriteLine("\n xxxx CPF nao encontrado.");
                            Console.WriteLine("\n Pressione ENTER para voltar...\n");
                            Console.ReadKey();
                            return;
                        }
                        else if (!Validacoes.ValidarCPF(cpf))
                        {
                            Console.WriteLine("\n xxxx CPF invalido.");
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
                    cpf = cpf.Replace(".", "").Replace("-", "");
                    linha = Arquivos.RecuperaLinhaInteira(Arquivos.Cliente, cpf, 0, 11);
                    cpf = cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                    nome = linha.Substring(11, 50).Trim();
                    dataNasc = DateTime.Parse(linha.Substring(61, 8).Insert(2, "/").Insert(5, "/"));
                    sexo = char.Parse(linha.Substring(69, 1));
                    situacao = char.Parse(linha.Substring(86, 1));

                    Console.Clear();
                    Console.WriteLine(msgInicial);
                    Console.WriteLine(msgSaida);
                    Console.WriteLine(" -------------------------------------------------------------------------\n");
                    Console.WriteLine($" CPF: {cpf}");
                    Console.WriteLine($" Nome: {nome}");
                    Console.WriteLine($" Data de Nasc: {dataNasc:dd/MM/yyyy}");
                    Console.WriteLine($" Sexo: {(sexo == 'M' ? "Masculino" : "Feminino")}");
                    Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inadimplente")}");
                    Console.WriteLine("\n Qual campo deseja alterar? Apos escolha, nao sera possivel retornar ate terminar o processo!");
                    Console.WriteLine(" 1 - Nome");
                    Console.WriteLine(" 2 - Data de Nascimento");
                    Console.WriteLine(" 3 - Sexo");
                    Console.WriteLine(" 4 - Incluir inadimplente\n");
                    Console.Write(" Escolha: ");
                    int.TryParse(Console.ReadLine(), out opcao);

                    if (opcao == 9)
                        return;
                    else if (opcao < 1 || opcao > 4)
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
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($"\n Nome atual: {linha.Substring(11, 50).Trim()}");
                            Console.Write(" Novo nome: ");
                            nome = Console.ReadLine();

                            if (nome.Length > 50)
                            {
                                Console.WriteLine("\n xxxx Tamanho do nome excedeu o limite (maximo: 50 caracteres).");
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
                                cpf = cpf.Replace(".", "").Replace("/", "").Replace("-", "");

                                Cliente tempCliente = new(decimal.Parse(cpf), nome, dataNasc, sexo,
                                    DateTime.Parse(linha.Substring(70, 8).Insert(2, "/").Insert(5, "/")),
                                    DateTime.Parse(linha.Substring(78, 8).Insert(2, "/").Insert(5, "/")),
                                    char.Parse(linha.Substring(86, 1)));

                                Arquivos.AlterarDocumento(Arquivos.Cliente, tempCliente.ToString(), cpf, 0, 11);

                                Console.WriteLine("\n Nome alterado.\n");
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
                            Console.WriteLine("\n ...:: Alteracao da Data de Nascimento ::...\n");
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($" Nome: {nome}");
                            Console.WriteLine($"\n Data de nascimento: {dataNasc:dd/MM/yyyy}");
                            Console.Write(" Nova data de nascimento: ");
                            string date = Console.ReadLine();

                            if (!date.Contains("/"))
                            {
                                date = date.Insert(2, "/").Insert(5, "/");
                            }

                            dataNasc = Convert.ToDateTime(date);
                            int ano = Convert.ToInt32(Math.Floor(DateTime.Today.Subtract(dataNasc).TotalDays / 365));

                            if (ano < 18)
                            {
                                Console.WriteLine("\n xxxx So podemos cadastrar pessoas com mais 18 anos.");
                                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                cpf = cpf.Replace(".", "").Replace("/", "").Replace("-", "");

                                Cliente tempCliente = new(decimal.Parse(cpf), nome, dataNasc, sexo,
                                    DateTime.Parse(linha.Substring(70, 8).Insert(2, "/").Insert(5, "/")),
                                    DateTime.Parse(linha.Substring(78, 8).Insert(2, "/").Insert(5, "/")),
                                    char.Parse(linha.Substring(86, 1)));

                                Arquivos.AlterarDocumento(Arquivos.Cliente, tempCliente.ToString(), cpf, 0, 11);

                                Console.WriteLine("\n Data de nascimento alterada.\n");
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
                            Console.WriteLine("\n ...:: Alteracao do Sexo ::...\n");
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($" Nome: {nome}");
                            Console.WriteLine($" Data de nascimento: {dataNasc:dd/MM/yyyy}");
                            Console.WriteLine($"\n Sexo: {(sexo == 'M' ? "Masculino" : "Feminino")}");
                            Console.Write(" Novo sexo (F - feminino / M - masculino): ");
                            string sexoString = Console.ReadLine().ToUpper();

                            if (sexoString != "M" && sexoString != "F")
                            {
                                Console.WriteLine("\n xxxx Digite apenas 'M' para masculino ou 'F' para feminino.");
                                Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                Console.ReadKey();
                            }
                            else
                            {
                                sexo = char.Parse(sexoString);
                                cpf = cpf.Replace(".", "").Replace("/", "").Replace("-", "");

                                Cliente tempCliente = new(decimal.Parse(cpf), nome, dataNasc, sexo,
                                            DateTime.Parse(linha.Substring(70, 8).Insert(2, "/").Insert(5, "/")),
                                            DateTime.Parse(linha.Substring(78, 8).Insert(2, "/").Insert(5, "/")),
                                            char.Parse(linha.Substring(86, 1)));

                                Arquivos.AlterarDocumento(Arquivos.Cliente, tempCliente.ToString(), cpf, 0, 11);

                                Console.WriteLine("\n Sexo alterado.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }
                            return;
                        } while (flagInterna);
                    }
                    else if (opcao == 4)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n ...:: Alteracao da Situacao ::...\n");
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($" Nome: {nome}");
                            Console.WriteLine($" Data de nascimento: {dataNasc:dd/MM/yyyy}");
                            Console.WriteLine($" Sexo: {(sexo == 'M' ? "Masculino" : "Feminino")}");
                            Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inadimplente")}");

                            if (situacao == 'A')
                                situacao = 'I';
                            else
                                situacao = 'A';

                            Console.WriteLine($"\n Nova situacao: {(situacao == 'A' ? "Ativo" : "Inadimplente")}");

                            cpf = cpf.Replace(".", "").Replace("/", "").Replace("-", "");

                            Cliente tempCliente = new(decimal.Parse(cpf), nome, dataNasc, sexo,
                                    DateTime.Parse(linha.Substring(70, 8).Insert(2, "/").Insert(5, "/")),
                                    DateTime.Parse(linha.Substring(78, 8).Insert(2, "/").Insert(5, "/")),
                                    situacao);

                            if (situacao == 'I')
                            {
                                Arquivos.AlterarDocumento(Arquivos.Cliente, tempCliente.ToString(), cpf, 0, 11, true);
                                Arquivos.Gravar(tempCliente.ToString(), Arquivos.Inadimplente);
                                Console.WriteLine("\n Situacao alterada.");
                                Console.WriteLine("\n Cliente incluido na lista de inadimplentes.\n");
                                Console.WriteLine(" Pressione ENTER para voltar...");
                                Console.ReadKey();
                            }

                            return;

                        } while (flagInterna);
                    }
                } while (flagPrincipal);
            }
        }

        public void Inadimplentes()
        {
            string msgInicial, msgSaida, cpf, linha, nome, escolha;
            bool flagInterna = true, flagPrincipal = true;
            DateTime dataNasc;
            char sexo, situacao;

            msgInicial = "\n ...:: Clientes Inadimplentes ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            if (!Arquivos.VerificarArquivoVazio(Arquivos.Inadimplente))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum cliente cadastrado como inadimplente.");
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
                    Console.WriteLine(" 1 - Mostrar Inadimplentes");
                    Console.WriteLine(" 2 - Retirar um cliente do cadastro de inadimplente");
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
                        ImprimirInadimplentes();
                    }
                    else if (escolha == "2")
                    {

                        Console.Clear();
                        Console.WriteLine(msgInicial);
                        Console.WriteLine(msgSaida);
                        Console.WriteLine(" -------------------------------------------------------------------------\n");

                        do
                        {
                            Console.Write(" Para comecar, digite o CPF: ");
                            cpf = Console.ReadLine();
                            if (cpf == "9")
                            {
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(
                                    Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, cpf.Replace(".", "").Replace("-", ""), 0, 11)))
                                {
                                    Console.WriteLine("\n xxxx CPF nao encontrado no cadastro de inadimplentes.");
                                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                                    Console.ReadKey();
                                    return;
                                }
                                else if (!Validacoes.ValidarCPF(cpf))
                                {
                                    Console.WriteLine("\n xxxx CPF invalido.");
                                    Console.WriteLine("\n Pressione ENTER para digitar novamente...\n");
                                    Console.ReadKey();
                                }
                                else
                                    flagInterna = false;
                            }

                        } while (flagInterna);

                        flagInterna = true;

                        cpf = cpf.Replace(".", "").Replace("-", "");
                        linha = Arquivos.RecuperaLinhaInteira(Arquivos.Inadimplente, cpf, 0, 11);
                        cpf = cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                        nome = linha.Substring(11, 50).Trim();
                        dataNasc = DateTime.Parse(linha.Substring(61, 8).Insert(2, "/").Insert(5, "/"));
                        sexo = char.Parse(linha.Substring(69, 1));
                        situacao = char.Parse(linha.Substring(86, 1));

                        do
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" CPF: {cpf}");
                            Console.WriteLine($" Nome: {nome}");
                            Console.WriteLine($" Data de Nasc: {dataNasc:dd/MM/yyyy}");
                            Console.WriteLine($" Sexo: {(sexo == 'M' ? "Masculino" : "Feminino")}");
                            Console.WriteLine($" Situacao: {(situacao == 'A' ? "Ativo" : "Inadimplente")}");
                            Console.WriteLine("\n Deseja retirar esse cliente do cadastro de inadimplente?\n");
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
                                cpf = cpf.Replace(".", "").Replace("/", "").Replace("-", "");

                                Cliente tempCliente = new(decimal.Parse(cpf), nome, dataNasc, sexo,
                                            DateTime.Parse(linha.Substring(70, 8).Insert(2, "/").Insert(5, "/")),
                                            DateTime.Parse(linha.Substring(78, 8).Insert(2, "/").Insert(5, "/")),
                                            situacao);

                                Arquivos.AlterarDocumento(Arquivos.Inadimplente, tempCliente.ToString(), cpf, 0, 11, true);
                                Arquivos.Gravar(tempCliente.ToString(), Arquivos.Cliente);

                                Console.WriteLine("\n Situacao alterada.");
                                Console.WriteLine("\n Cliente excluido da lista de inadimplentes.\n");
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
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Cliente))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum cliente cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Cliente");
            }
        }
        public void ImprimirInadimplentes()
        {
            if (!Arquivos.VerificarArquivoVazio(Arquivos.Inadimplente))
            {
                Console.WriteLine("\n xxxx Nao ha nenhum cliente cadastrado.");
                Console.WriteLine("\n Pressione ENTER para voltar...\n");
                Console.ReadKey();
            }
            else
            {
                FuncoesGerais fn = new();
                fn.Imprimir("Risco");
            }
        }
    }
}
