using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ThinkBridgeTest.Models;

namespace ThinkBridgeTest.Repository
{
    public class InventoryRepository
    {
        private readonly SqlConnection _connection;
        DataTable dt = new DataTable();
        public InventoryRepository()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["WebApiConnection"].ConnectionString);

        }
        public Responce InsertInventoryData(Inventory model)
        {
            Responce ObjResponse = new Responce();
            int success = 0;
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("pr_InsertInventoryData", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.strItemName);
                    cmd.Parameters.AddWithValue("@Description", model.strItemDescription);
                    cmd.Parameters.AddWithValue("@Price", model.intPrice);
                    SqlParameter para = new SqlParameter("@Retval", SqlDbType.Int);
                    para.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    success = Convert.ToInt32(cmd.Parameters["@Retval"].Value);

                    if (success == 1)
                    {
                        ObjResponse.Status = true;
                        ObjResponse.Message = "Inventory Added Successfully.";
                    }
                    else if(success == 2)
                    {
                        ObjResponse.Status = false;
                        ObjResponse.Message = "Inventory Item Already Exists.";
                    }

                    else
                    {
                        ObjResponse.Status = false;
                        ObjResponse.Message = "Something Went Wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResponse.Status = false;
                ObjResponse.Message = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
            return ObjResponse;
        }

        public Responce ModifyInventoryData(Inventory model)
        {
            Responce ObjResponse = new Responce();
            int success = 0;
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("pr_ModifyInventoryData", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.intId);
                    cmd.Parameters.AddWithValue("@Name", model.strItemName);
                    cmd.Parameters.AddWithValue("@Description", model.strItemDescription);
                    cmd.Parameters.AddWithValue("@Price", model.intPrice);
                    SqlParameter para = new SqlParameter("@Retval", SqlDbType.Int);
                    para.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    success = Convert.ToInt32(cmd.Parameters["@Retval"].Value);

                    if (success == 1)
                    {
                        ObjResponse.Status = true;
                        ObjResponse.Message = "Inventory Modified Successfully.";
                    }

                    else
                    {
                        ObjResponse.Status = false;
                        ObjResponse.Message = "Something Went Wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResponse.Status = false;
                ObjResponse.Message = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
            return ObjResponse;
        }

        public Responce DeleteInventoryData(Inventory model)
        {
            Responce ObjResponse = new Responce();
            int success = 0;
            try
            {
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("pr_DeleteInventoryData", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.intId);
                    SqlParameter para = new SqlParameter("@Retval", SqlDbType.Int);
                    para.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                    success = Convert.ToInt32(cmd.Parameters["@Retval"].Value);

                    if (success == 1)
                    {
                        ObjResponse.Status = true;
                        ObjResponse.Message = "Inventory Deleted Successfully.";
                    }

                    else
                    {
                        ObjResponse.Status = false;
                        ObjResponse.Message = "Something Went Wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResponse.Status = false;
                ObjResponse.Message = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
            return ObjResponse;
        }

        public IEnumerable<Inventory> DisplayInventoryData()
        {
            try
            {
                _connection.Open();
                DataTable dt = new DataTable();
                List<Inventory> InventoryList = new List<Inventory>();
                using (SqlCommand cmd = new SqlCommand("proc_GetInventoryList", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Inventory objcomp = new Inventory();
                            objcomp.intId = Convert.ToInt32(dr["Id"]);
                            objcomp.strItemName = Convert.ToString(dr["ItemName"]);
                            objcomp.strItemDescription = Convert.ToString(dr["ItemDescription"]);
                            objcomp.intPrice = Convert.ToInt32(dr["Price"]);
                            objcomp.CreatedDate = Convert.ToString(dr["CreatedDate"]);

                            InventoryList.Add(objcomp);
                        }
                    }
                };
                return InventoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }

        public InventorySearch SearchItem(SearchItem model)
        {
            try
            {
                DataTable dt = new DataTable();
                List<InventorySearch> InventoryList = new List<InventorySearch>();
                InventorySearch objcomp1 = new InventorySearch();
                List<Inventory> InventorySearchList = new List<Inventory>();

                using (SqlCommand cmd = new SqlCommand("proc_getSearchItemList", _connection))
                {
                    _connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Search", model.Search);
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Inventory objcomp = new Inventory();
                            objcomp.intId = Convert.ToInt32(dr["Id"]);
                            objcomp.strItemName = Convert.ToString(dr["ItemName"]);
                            objcomp.strItemDescription = Convert.ToString(dr["ItemDescription"]);
                            objcomp.intPrice = Convert.ToInt32(dr["Price"]);
                            objcomp.CreatedDate = Convert.ToString(dr["CreatedDate"]);

                            InventorySearchList.Add(objcomp);
                            InventoryList.Add(objcomp1);
                        }
                    }
                    
                };
                objcomp1.List= InventorySearchList.ToPagedList(1,20);
                return objcomp1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}