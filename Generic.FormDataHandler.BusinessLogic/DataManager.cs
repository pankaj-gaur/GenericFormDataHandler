using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Generic.FormDataHandler.Helper;
using System.Data;

namespace Generic.FormDataHandler.BusinessLogic
{
    internal class DataManager
    {
        readonly string connectionString = null;
        readonly string cmdSPInsertFormData = null;
        readonly string cmdSPSelectFormData = null;

        readonly string paramFormID = null;
        readonly string paramProjectID = null;
        readonly string paramFormData = null;
        readonly string paramUserIP = null;
        readonly string paramUserLocation = null;
        readonly string paramSubmitDate = null;

        internal DataManager()
        {
            ContentHelper helper = new ContentHelper();
            connectionString = helper.GetValueFromConfiguration("connectionString");

            cmdSPInsertFormData = "sp_Insert_FormData";
            cmdSPSelectFormData = "sp_Select_FormData";

            paramFormID = "FormID";
            paramProjectID = "ProjectID";
            paramFormData = "FormData";
            paramUserIP = "UserIP";
            paramUserLocation = "UserLocation";
            paramSubmitDate = "SubmitDate";
        }

        internal void ExecuteSP_InsertFormData(string formID, string projectID, byte[] formData, string userIP, string userLocation)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = cmdSPInsertFormData;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(paramFormID, formID);
                    cmd.Parameters.AddWithValue(paramProjectID, projectID);
                    cmd.Parameters.AddWithValue(paramFormData, formData);
                    cmd.Parameters.AddWithValue(paramUserIP, userIP);
                    cmd.Parameters.AddWithValue(paramUserLocation, userLocation);

                    cmd.Connection = conn;
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
        }

        internal DataTableCollection ExecuteSP_SelectFormData(string formID, string projectID, DateTime submitDate)
        {
            SqlDataAdapter adaptor;
            DataSet dataSet;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string cmdText = cmdSPSelectFormData;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = cmdText;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(formID))
                    {
                        cmd.Parameters.AddWithValue(paramFormID, formID);
                    }
                    if (!string.IsNullOrEmpty(projectID))
                    {
                        cmd.Parameters.AddWithValue(paramProjectID, projectID);
                    }
                    if (submitDate != DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue(paramSubmitDate, submitDate);
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    adaptor = new SqlDataAdapter(cmd);
                    dataSet = new DataSet();
                    dataSet.Clear();
                    adaptor.Fill(dataSet);

                    return dataSet != null ? dataSet.Tables : null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                    dataSet = null;
                    adaptor = null;
                }
            }
        }
    }
}
