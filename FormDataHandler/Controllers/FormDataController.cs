using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Generic.FormDataHandler.BusinessLogic;
using Generic.FormDataHandler.Helper;
using Generic.FormDataHandler.Models;

using Newtonsoft.Json;
using System.Data;
using System.Xml;

namespace Generic.FormDataHandler.Controllers
{
    public class FormsDataController : ApiController
    {
        private const string colName_FormID = "FormID";
        private const string colName_ProjectID = "ProjectID";
        private const string colName_FormData = "FormData";
        private const string colName_UserIP = "UserIP";
        private const string colName_UserLocation = "UserLocation";
        private const string colName_SubmitDate = "SubmitDate";

        public string GetFormData(FormBase formData)
        {
            BusinessManager manager = new BusinessManager();
            DataFormat returnFormat;
            returnFormat = DataFormat.JSON;
            string returnValue = string.Empty;

            DataTableCollection tables;
            if (formData != null)
            {
                if (!string.IsNullOrEmpty(formData.Format))
                {

                    if (!Enum.TryParse(formData.Format, out returnFormat))
                    {
                        returnFormat = DataFormat.JSON;
                    }
                }

                tables = manager.GetFormData(formData.FormID, formData.ProjectID, formData.SubmitDate);
            }
            else
            {
                tables = manager.GetFormData(null, null, DateTime.MinValue);
            }

            List<FormBase> deserializedFormData = GetDeserializedFormData(tables, returnFormat);

            if (returnFormat == DataFormat.XML)
            {
                returnValue = ContentHelper.GetXmlFromObject(deserializedFormData);
            }
            else
            {
                returnValue = ContentHelper.GetJSONFromObject(deserializedFormData);
            }
            return returnValue;
        }

        private List<FormBase> GetDeserializedFormData(DataTableCollection tables, DataFormat format)
        {
            List<FormBase> formsData = new List<FormBase>();
            foreach (DataTable table in tables)
            {
                if (table.Rows != null)
                {
                    for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                    {
                        DataRow row = table.Rows[rowIndex];
                        FormBase form = new FormBase();
                        if (row[colName_FormID] != DBNull.Value)
                        {
                            form.FormID = row[colName_FormID].ToString();
                        }
                        if (row[colName_ProjectID] != DBNull.Value)
                        {
                            form.ProjectID = row[colName_ProjectID].ToString();
                        }
                        if (row[colName_FormData] != DBNull.Value)
                        {
                            byte[] dataBytes = (byte[])row[colName_FormData];
                            form.FormData = ContentHelper.ByteArrayToObject<string>(dataBytes);
                        }
                        if (row[colName_UserIP] != DBNull.Value)
                        {
                            form.UserIP = row[colName_UserIP].ToString();
                        }
                        if (row[colName_UserLocation] != DBNull.Value)
                        {
                            form.UserLocation = row[colName_UserLocation].ToString();
                        }
                        if (row[colName_SubmitDate] != DBNull.Value)
                        {
                            form.SubmitDate = (DateTime)row[colName_SubmitDate];
                        }

                        formsData.Add(form);
                    }
                }
            }
            return formsData;
        }

        public void PostFormData(FormBase formData)
        {
            BusinessManager manager = new BusinessManager();
            manager.InsertFormData(formData.FormID, formData.ProjectID, formData.FormData, formData.UserIP, formData.UserLocation);
        }
    }
}