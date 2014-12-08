using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Generic.FormDataHandler.Helper;

namespace Generic.FormDataHandler.BusinessLogic
{
    public class BusinessManager
    {
        public void InsertFormData(string formID, string projectID, string formData, string userIP, string userLocation)
        {
            DataManager dataManager = new DataManager();
            byte[] formDataBlob = ContentHelper.ObjectToByteArray(formData);
            dataManager.ExecuteSP_InsertFormData(formID, projectID, formDataBlob, userIP, userLocation);
        }

        public DataTableCollection GetFormData(string formID, string projectID, DateTime dateTime)
        {
            DataManager dataManager = new DataManager();
            return dataManager.ExecuteSP_SelectFormData(formID, projectID, dateTime);
        }
    }
}
