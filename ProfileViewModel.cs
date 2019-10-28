using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStmt.ViewModels
{
    public class ProfileViewModel
    {
        public string ClientID { get; set; }
        public string SqlServerIp { get; set; }
        public string DBName { get; set; }
        public string CoPhone { get; set; }
        public string CoEmail { get; set; }
        public string EAC { get; set; }
        public string ResetPSWD { get; set; }
        public int AgentRID { get; set; }
        public string fullName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? debitBalance { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? cbBalance { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? mcBalance { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? EscrowBalance { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? YTD1099 { get; set; }

        public string TaxId { get; set; }
        public string TaxType { get; set; }
        public string TaxIDName { get; set; }
       
    }
}