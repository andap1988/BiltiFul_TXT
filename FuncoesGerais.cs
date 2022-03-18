using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class FuncoesGerais
    {
        Arquivos Arquivos = new();

        public void Imprimir(string setor)
        {
            string nomeExibicao, arquivo = "";

            if (setor == "Materia")
            {
                arquivo = Arquivos.MateriaPrima;
                nomeExibicao = "Materias-primas";
            }
            else if (setor == "Cliente")
            {
                arquivo = Arquivos.Cliente;
                nomeExibicao = "Clientes";
            }
            else if (setor == "Fornecedor")
            {
                arquivo = Arquivos.Fornecedor;
                nomeExibicao = "Fornecedores";
            }
            else if (setor == "Produto")
            {
                arquivo = Arquivos.Produto;
                nomeExibicao = "Fornecedores";
            }
            else if (setor == "Risco")
            {
                arquivo = Arquivos.Inadimplente;
                nomeExibicao = "Inadimplentes";
            }
            else if (setor == "Bloqueado")
            {
                arquivo = Arquivos.Bloqueado;
                nomeExibicao = "Bloqueados";
            }
            else if (setor == "Venda")
            {
                arquivo = Arquivos.Venda;
                nomeExibicao = "Vendas";
            }
            else if (setor == "Compra")
            {
                arquivo = Arquivos.Compra;
                nomeExibicao = "Compras";
            }
            else if (setor == "Producao")
            {
                arquivo = Arquivos.Producao;
                nomeExibicao = "Producoes";
            }

            List<string> linhas = Arquivos.MontarLista(arquivo);

            int posicao = 0, max = linhas.Count;
            string escolha = "0", msgInicial, msgSaida;

            msgInicial = $"\n ...:: Lista de {setor} ::...\n";
            msgSaida = " Caso queira voltar ao menu anterior, basta digitar 9 e pressionar ENTER\n";

            Console.Clear();
            Console.WriteLine(msgInicial);
            Console.WriteLine(msgSaida);
            Console.WriteLine(" -------------------------------------------------------------------------\n");
            Console.WriteLine($" 1º Registro\n");
            DesenharDados(setor, linhas.First());

            do
            {
                Console.WriteLine("\n 1 - Primeiro / 2 - Anterior / 3 - Proximo / 4 - Ultimo\n");
                Console.Write(" Escolha: ");
                escolha = Console.ReadLine();

                if (escolha != "1" && escolha != "2" && escolha != "3" && escolha != "4" && escolha != "9")
                {
                    Console.WriteLine("\n xxxx Opcao invalida.");
                    Console.WriteLine("\n Pressione ENTER para voltar...\n");
                    Console.ReadKey();
                }
                else if (escolha == "9")
                    return;
                else
                {
                    if (escolha == "1")
                    {
                        if (posicao == 0)
                            Console.WriteLine("\n xxxx Ja estamos no primeiro registro.");
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" 1º Registro\n");
                            DesenharDados(setor, linhas.First());
                            posicao = 0;
                        }
                    }
                    else if (escolha == "2")
                    {
                        if (posicao == 0)
                            Console.WriteLine("\n xxxx Nao ha registro anterior.");
                        else
                        {
                            posicao--;
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" {posicao + 1}º Registro\n");
                            DesenharDados(setor, linhas[posicao]);
                        }
                    }
                    else if (escolha == "3")
                    {
                        if (posicao == linhas.Count - 1)
                            Console.WriteLine("\n xxxx Nao ha proximo registro.");
                        else
                        {
                            posicao++;
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" {posicao + 1}º Registro\n");
                            DesenharDados(setor, linhas[posicao]);
                        }
                    }
                    else if (escolha == "4")
                    {
                        if (posicao == linhas.Count - 1)
                            Console.WriteLine("\n xxxx Ja estamos no ultimo registro.");
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(msgInicial);
                            Console.WriteLine(msgSaida);
                            Console.WriteLine(" -------------------------------------------------------------------------\n");
                            Console.WriteLine($" {linhas.Count}º Registro (ultimo registro)\n");
                            DesenharDados(setor, linhas.Last());
                            posicao = linhas.Count - 1;
                        }
                    }
                }
            } while (escolha != "9");
        }

        public void DesenharDados(string setor, string dado, List<string> itens = null)
        {
            if (setor == "Fornecedor")
            {
                string cnpj = dado.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                string razaoSocial = dado.Substring(14, 50).Trim();
                string dataAbertura = dado.Substring(64, 8).Insert(2, "/").Insert(5, "/");
                string ultimaCompra = dado.Substring(72, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(80, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(88, 1);

                Console.WriteLine($"\n CNPJ:             {cnpj}");
                Console.WriteLine($" Razao Social:     {razaoSocial}");
                Console.WriteLine($" Data de Abertura: {dataAbertura}");
                Console.WriteLine($" Ultima Compra:    {ultimaCompra}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Bloqueado")}\n");
            }
            else if (setor == "Cliente")
            {
                string cpf = dado.Substring(0, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-");
                string nome = dado.Substring(11, 50).Trim();
                string dataNasc = dado.Substring(61, 8).Insert(2, "/").Insert(5, "/");
                string sexo = dado.Substring(69, 1);
                string ultimaCompra = dado.Substring(70, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(78, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(86, 1);

                Console.WriteLine($"\n CPF:              {cpf}");
                Console.WriteLine($" Nome:             {nome}");
                Console.WriteLine($" Data de Nasc.:    {dataNasc}");
                Console.WriteLine($" Sexo:             {(sexo == "M" ? "Masculino" : "Feminino")}");
                Console.WriteLine($" Ultima Compra:    {ultimaCompra}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Inadimplente")}\n");
            }
            else if (setor == "Materia")
            {
                string id = dado.Substring(0, 6);
                string nome = dado.Substring(6, 20).Trim();
                string ultimaCompra = dado.Substring(26, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(34, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(42, 1);

                Console.WriteLine($" ID:               {id}");
                Console.WriteLine($" Nome:             {nome}");
                Console.WriteLine($" Ultima Compra:    {ultimaCompra}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Inativo")}\n");
            }
            else if (setor == "Produto")
            {
                string id = dado.Substring(0, 13);
                string nome = dado.Substring(13, 20).Trim();
                string valorVenda = dado.Substring(33, 5).Insert(3, ",");
                string ultimaVenda = dado.Substring(38, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(46, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(54, 1);

                Console.WriteLine($" ID:               {id}");
                Console.WriteLine($" Nome:             {nome}");
                Console.WriteLine($" Valor de Venda:   {valorVenda:###.#0}");
                Console.WriteLine($" Ultima Venda:     {ultimaVenda}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Inativo")}\n");
            }
            else if (setor == "Risco")
            {
                string cpf = dado.Substring(0, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-");
                string nome = dado.Substring(11, 50).Trim();
                string dataNasc = dado.Substring(61, 8).Insert(2, "/").Insert(5, "/");
                string sexo = dado.Substring(69, 1);
                string ultimaCompra = dado.Substring(70, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(78, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(86, 1);

                Console.WriteLine($"\n CPF:              {cpf}");
                Console.WriteLine($" Nome:             {nome}");
                Console.WriteLine($" Data de Nasc.:    {dataNasc}");
                Console.WriteLine($" Sexo:             {(sexo == "M" ? "Masculino" : "Feminino")}");
                Console.WriteLine($" Ultima Compra:    {ultimaCompra}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Inadimplente")}\n");
            }
            else if (setor == "Bloqueado")
            {
                string cnpj = dado.Substring(0, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                string razaoSocial = dado.Substring(14, 50).Trim();
                string dataAbertura = dado.Substring(64, 8).Insert(2, "/").Insert(5, "/");
                string ultimaCompra = dado.Substring(72, 8).Insert(2, "/").Insert(5, "/");
                string dataCadastro = dado.Substring(80, 8).Insert(2, "/").Insert(5, "/");
                string situacao = dado.Substring(88, 1);

                Console.WriteLine($"\n CNPJ:             {cnpj}");
                Console.WriteLine($" Razao Social:     {razaoSocial}");
                Console.WriteLine($" Data de Abertura: {dataAbertura}");
                Console.WriteLine($" Ultima Compra:    {ultimaCompra}");
                Console.WriteLine($" Data de Cadastro: {dataCadastro}");
                Console.WriteLine($" Situacao:         {(situacao == "A" ? "Ativo" : "Bloqueado")}\n");
            }
            else if (setor == "Venda")
            {
                List<string> itensVenda = new();

                string codVenda = dado.Substring(0, 5);
                string dataVenda = dado.Substring(5, 8).Insert(2, "/").Insert(5, "/");
                string cpf = dado.Substring(13, 11).Insert(3, ".").Insert(7, ".").Insert(11, "-");
                string valorVenda = dado.Substring(24, 6);

                Console.WriteLine($"\n Venda nº:        {codVenda}");
                Console.WriteLine($" Data Venda:       {dataVenda}");
                Console.WriteLine($" CPF:              {cpf}");
                Console.WriteLine($" Valor da Venda:   {valorVenda.Insert(1, ".").Insert(5, ","):#,###.#0}");

                itensVenda = Arquivos.MontarLista(Arquivos.ItemVenda, dado.Substring(0, 5), 0, 5, true);

                Console.WriteLine("\n Cod.          Produto              V. Unitario       Qt.         Total");
                Console.WriteLine(" -------------------------------------------------------------------------\n");
                itensVenda.ForEach(item =>
                {
                    string linha = Arquivos.RecuperaLinhaInteira(Arquivos.Produto, item.Substring(5, 13), 0, 13);
                    string nome = linha.Substring(13, 20).Trim();
                    Console.WriteLine($" {linha.Substring(0, 13)} {nome.PadRight(20, ' ')} {item.Substring(21, 5).Insert(3, ","),8:###.#0}     {item.Substring(18, 3),8:000}        {item.Substring(26, 6).Insert(1, ".").Insert(5, ","),8:#,##0.#0}");
                });
                Console.WriteLine("\n\n                               --/-------/--\n");
            }
            else if (setor == "Compra")
            {
                List<string> itensCompra = null;

                string codCompra = dado.Substring(0, 5);
                string dataCompra = dado.Substring(5, 8).Insert(2, "/").Insert(5, "/");
                string cnpj = dado.Substring(13, 14).Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                string valorCompra = dado.Substring(27, 6);

                Console.WriteLine($"\n Compra nº:         {codCompra}");
                Console.WriteLine($" Data Compra:       {dataCompra}");
                Console.WriteLine($" CNPJ:              {cnpj}");
                Console.WriteLine($" Valor da Compra:   {valorCompra.Insert(1, ".").Insert(5, ","):#,##0.#0}");

                itensCompra = Arquivos.MontarLista(Arquivos.ItemCompra, dado.Substring(0, 5), 0, 5, true);

                Console.WriteLine("\n Cod.       Materia-prima           V. Unitario         Qt.         Total");
                Console.WriteLine(" ---------------------------------------------------------------------------\n");
                itensCompra.ForEach(item =>
                {
                    string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.Substring(13, 6), 0, 6);
                    string nome = linha.Substring(6, 20).Trim();
                    Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')}     {item.Substring(24, 5).Insert(3, ","),8:##0.#0}      {item.Substring(19, 5).Insert(3, ","),8:##0.#0}       {item.Substring(29, 6).Insert(1, ".").Insert(5, ","),8:#,##0.#0}");
                });
                Console.WriteLine("\n\n                               --/-------/--\n");
            }
            else if (setor == "Producao")
            {
                List<string> itensProducao = null;

                string codProducao = dado.Substring(0, 5);
                string dataProducao = dado.Substring(5, 8).Insert(2, "/").Insert(5, "/");
                string codProduto = dado.Substring(13, 13);
                string qtProduto = dado.Substring(26, 5).Insert(3, ",");

                Console.WriteLine($"\n Producao nº:      {codProducao}");
                Console.WriteLine($" Data Producao:    {dataProducao}");
                Console.WriteLine($" Cod. Produto:     {codProduto}");
                Console.WriteLine($" Qtd. de Produto:  {qtProduto:###.#0}");

                itensProducao = Arquivos.MontarLista(Arquivos.ItemProducao, dado.Substring(0, 5), 0, 5, true);

                Console.WriteLine("\n Cod.       Materia-prima            Qt.  ");
                Console.WriteLine(" -------------------------------------------\n");
                itensProducao.ForEach(item =>
                {
                    string linha = Arquivos.RecuperaLinhaInteira(Arquivos.MateriaPrima, item.Substring(13, 6), 0, 6);
                    string nome = linha.Substring(6, 20).Trim();
                    Console.WriteLine($" {linha.Substring(0, 6)}     {nome.PadRight(20, ' ')} {item.Substring(19, 5).Insert(3, ","),8:##0.#0}");
                });
                Console.WriteLine("\n\n             --/-------/--\n");

            }
        }
    }
}
