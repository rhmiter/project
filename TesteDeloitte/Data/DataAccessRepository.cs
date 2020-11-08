using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TesteDeloitte.Models;

namespace TesteDeloitte.Data
{
    public class DataAccessRepository
    {
        private SqlConnection con = null;

        public DataAccessRepository()
        {
            con = GetConnection();
        }

        #region Public
        public void InsertNotaFiscal(NotaFiscal obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_NotaFiscal_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteId", obj.ClienteId);
                cmd.Parameters.AddWithValue("@ProdutoId", obj.ProdutoId);
                cmd.Parameters.AddWithValue("@Valor", obj.Valor);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateNotaFiscal(NotaFiscal obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_NotaFiscal_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@ClienteId", obj.ClienteId);
                cmd.Parameters.AddWithValue("@ProdutoId", obj.ProdutoId);
                cmd.Parameters.AddWithValue("@Valor", obj.Valor);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteNotaFiscal(string Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_NotaFiscal_Delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public NotaFiscalViewModel SelectNotaFiscalByID(string Id)
        {
            DataSet ds = null;
            NotaFiscalViewModel obj = null;
            try
            {
                SqlCommand cmd = new SqlCommand("usp_NotaFiscal_SelectSpecific", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new NotaFiscalViewModel();
                    obj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    obj.ClienteId = Convert.ToInt32(ds.Tables[0].Rows[i]["ClienteId"].ToString());
                    obj.ProdutoId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProdutoId"].ToString());
                    obj.Valor = Convert.ToDecimal(ds.Tables[0].Rows[i]["Valor"].ToString());
                }
                return obj;
            }
            catch
            {
                return obj;
            }
            finally
            {
                con.Close();
            }
        }

        public List<NotaFiscalViewModel> SelectAllNotaFiscal()
        {
            List<NotaFiscalViewModel> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_NotaFiscal_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                list = new List<NotaFiscalViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    NotaFiscalViewModel obj = new NotaFiscalViewModel();
                    obj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    obj.NomeCliente = ds.Tables[0].Rows[i]["NomeCliente"].ToString();
                    obj.NomeProduto = ds.Tables[0].Rows[i]["NomeProduto"].ToString();
                    obj.Valor = Convert.ToDecimal(ds.Tables[0].Rows[i]["Valor"].ToString());
                    obj.Observacao = ds.Tables[0].Rows[i]["Observacao"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return list;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Cliente> SelectAllClientes()
        {
            List<Cliente> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Clientes_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                list = new List<Cliente>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Cliente obj = new Cliente();
                    obj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    obj.Nome = ds.Tables[0].Rows[i]["Nome"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return list;
            }
            finally
            {
                con.Close();
            }
        }

        public List<ProdutoViewModel> SelectAllProdutos()
        {
            List<ProdutoViewModel> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Produtos_SelectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                list = new List<ProdutoViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ProdutoViewModel obj = new ProdutoViewModel();
                    obj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"].ToString());
                    obj.NomeCompleto = ds.Tables[0].Rows[i]["NomeProduto"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return list;
            }
            finally
            {
                con.Close();
            }
        }

        public List<RelatorioVendasProdutoViewModel> SelectReportVendasProduto()
        {
            List<RelatorioVendasProdutoViewModel> list = null;
            try
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Relatorio_Vendas_Produto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                list = new List<RelatorioVendasProdutoViewModel>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RelatorioVendasProdutoViewModel obj = new RelatorioVendasProdutoViewModel();
                    obj.ProdutoId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProdutoId"].ToString());
                    obj.NomeProduto = ds.Tables[0].Rows[i]["NomeProduto"].ToString();
                    obj.QtdeVendas = Convert.ToDecimal(ds.Tables[0].Rows[i]["QtdeVendas"].ToString());
                    obj.TotalVendas = Convert.ToDecimal(ds.Tables[0].Rows[i]["TotalVendas"].ToString());
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return list;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Private 
        private NotaFiscal InitiateNotaFiscal()
        {
            return new NotaFiscal()
            {
                Cliente = new Cliente(),
                Produto = new Produto()
                {
                    Fornecedor = new Fornecedor()
                }
            };
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection($"{ConfigurationManager.ConnectionStrings["TesteDeloitte"]}Initial Catalog=TesteDeloitte;");
        }
        #endregion
    }
}