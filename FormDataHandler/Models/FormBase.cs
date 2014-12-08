using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Generic.FormDataHandler.Models
{
    public class FormBase
    {
        public string FormID { get; set; }

        public string ProjectID { get; set; }

        public string FormData { get; set; }

        public string UserIP { get; set; }

        public string UserLocation { get; set; }

        public string Format { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}