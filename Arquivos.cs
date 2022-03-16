using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiltiFulBD
{
    public class Arquivos
    {
        public string DataBase { get; set; }
        public string Cliente { get; set; }
        public string Fornecedor { get; set; }
        public string Produto { get; set; }
        public string MateriaPrima { get; set; }
        public string Inadimplente { get; set; }
        public string Bloqueado { get; set; }
        public string Venda { get; set; }
        public string ItemVenda { get; set; }
        public string Compra { get; set; }
        public string ItemCompra { get; set; }
        public string Producao { get; set; }
        public string ItemProducao { get; set; }
        public string IdMPrima { get; set; }
        public string IdVenda { get; set; }
        public string IdCompra { get; set; }

        public Arquivos()
        {
            Cliente = "Clientes.dat";
            Fornecedor = "Fornecedor.dat";
            MateriaPrima = "Materia.dat";
            Produto = "Cosmetico.dat";
            Inadimplente = "Risco.dat";
            Bloqueado = "Bloqueado.dat";
            Venda = "Venda.dat";
            ItemVenda = "ItemVenda.dat";
            Compra = "Compra.dat";
            ItemCompra = "ItemCompra.dat";
            Producao = "Producao.dat";
            ItemProducao = "ItemProducao.dat";
            IdMPrima = "IdMPrima.dat";
            IdVenda = "IdVenda.dat";
            IdCompra = "IdCompra.dat";
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\"));
            DataBase = Path.Combine(Directory.GetCurrentDirectory() + "\\Database\\");
        }

        public void GerarArquivos()
        {
            try
            {
                if (!File.Exists(DataBase + Cliente))
                    File.Create(DataBase + Cliente).Close();
                if (!File.Exists(DataBase + Fornecedor))
                    File.Create(DataBase + Fornecedor).Close();
                if (!File.Exists(DataBase + MateriaPrima))
                    File.Create(DataBase + MateriaPrima).Close();
                if (!File.Exists(DataBase + Produto))
                    File.Create(DataBase + Produto).Close();
                if (!File.Exists(DataBase + Inadimplente))
                    File.Create(DataBase + Inadimplente).Close();
                if (!File.Exists(DataBase + Bloqueado))
                    File.Create(DataBase + Bloqueado).Close();
                if (!File.Exists(DataBase + Venda))
                    File.Create(DataBase + Venda).Close();
                if (!File.Exists(DataBase + ItemVenda))
                    File.Create(DataBase + ItemVenda).Close();
                if (!File.Exists(DataBase + Compra))
                    File.Create(DataBase + Compra).Close();
                if (!File.Exists(DataBase + ItemCompra))
                    File.Create(DataBase + ItemCompra).Close();
                if (!File.Exists(DataBase + Producao))
                    File.Create(DataBase + Producao).Close();
                if (!File.Exists(DataBase + ItemProducao))
                    File.Create(DataBase + ItemProducao).Close();
                if (!File.Exists(DataBase + IdMPrima))
                    File.Create(DataBase + IdMPrima).Close();
                if (!File.Exists(DataBase + IdVenda))
                    File.Create(DataBase + IdVenda).Close();
                if (!File.Exists(DataBase + IdCompra))
                    File.Create(DataBase + IdCompra).Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ex -> " + ex.Message);
            }
        }

        public void Gravar(string dado, string setor)
        {
            try
            {
                if (!File.Exists(DataBase + setor))
                {
                    using (StreamWriter sw = new StreamWriter(DataBase + setor))
                    {
                        sw.WriteLine(dado);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(DataBase + setor, append: true))
                    {
                        sw.WriteLine(dado);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ex -> " + ex.Message);
            }
        }

        public void AlterarDocumento(string setor, string novoDado, string identificador, int indexInicial, int indexFinal, bool deletar = false)
        {
            string linha;
            List<string> dados = new();

            try
            {
                using (StreamReader sr = new StreamReader(DataBase + setor))
                {
                    linha = sr.ReadLine();
                    do
                    {
                        if (linha.Substring(indexInicial, indexFinal) != identificador)
                            dados.Add(linha);

                        linha = sr.ReadLine();
                    } while (linha != null);
                }

                File.Delete(DataBase + setor);

                if (deletar)
                {
                    using (StreamWriter sw = new StreamWriter(DataBase + setor))
                    {
                        dados.ForEach(dado =>
                        {
                            sw.WriteLine(dado);
                        });
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(DataBase + setor))
                    {
                        sw.WriteLine(novoDado);
                        dados.ForEach(dado =>
                        {
                            sw.WriteLine(dado);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ex -> " + ex.Message);
            }
        }

        public bool VerificarArquivoVazio(string setor)
        {
            bool temLinha = false;

            try
            {
                using (StreamReader sr = new StreamReader(DataBase + setor))
                {
                    if (!string.IsNullOrEmpty(sr.ReadLine()))
                        temLinha = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Ex -> " + ex.Message);
            }
            return temLinha;
        }

        public string RecuperaLinhaInteira(string setor, string identificador = null, int indexInicial = 0, int indexFinal = 0)
        {
            string linha, encontrado = null;

            if (string.IsNullOrEmpty(identificador))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(DataBase + setor))
                    {
                        linha = sr.ReadLine();
                        while (linha != null)
                        {
                            encontrado = linha;

                            linha = sr.ReadLine();
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Ex -> " + ex.Message);
                }
            }
            else
            {
                try
                {
                    using (StreamReader sr = new StreamReader(DataBase + setor))
                    {
                        linha = sr.ReadLine();
                        while (linha != null)
                        {
                            if (linha.Substring(indexInicial, indexFinal) == identificador)
                                encontrado = linha;

                            linha = sr.ReadLine();
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Ex -> " + ex.Message);
                }
            }
            return encontrado;
        }

        public List<string> MontarLista(string setor, string identificador = null, int indexInicial = 0, int indexFinal = 0, bool inclusao = false)
        {
            List<string> lista = new();

            if (inclusao)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(DataBase + setor))
                    {
                        string linha = sr.ReadLine();

                        do
                        {
                            if (linha.Substring(indexInicial, indexFinal) == identificador)
                                lista.Add(linha);
                            linha = sr.ReadLine();
                        } while (linha != null);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Ex -> " + ex.Message);
                }
            }
            else
            {
                if (identificador != null)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(DataBase + setor))
                        {
                            string linha = sr.ReadLine();

                            do
                            {
                                if (linha.Substring(indexInicial, indexFinal) != identificador)
                                    lista.Add(linha);
                                linha = sr.ReadLine();
                            } while (linha != null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Ex -> " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(DataBase + setor))
                        {
                            string linha = sr.ReadLine();

                            do
                            {
                                lista.Add(linha);
                                linha = sr.ReadLine();
                            } while (linha != null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Ex -> " + ex.Message);
                    }
                }
            }
            return lista;
        }

        public void DeletarArquivo(string setor)
        {
            if (File.Exists(DataBase + setor))
                File.Delete(DataBase + setor);
        }

    }
}
