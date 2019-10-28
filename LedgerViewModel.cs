using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyStmt.ViewModels
{
    public class LedgerViewModel
    {
         public string DBName { get; set; }
        public int LedgerRID { get; set; }

        [Required]
        [StringLength(20)]
        public string AgentID { get; set; }
        
        public string AgentName { get; set; }
        public string Period { get; set; }

       public DateTime? LedgerDate { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Commission { get; set; }       

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? ChargeBack { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Escrow { get; set; }       

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Credit { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Net { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Adjustment")]
        public decimal? Adjustments { get; set; }        

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? Hold { get; set; }
        
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal? DebitBalance { get; set; }       
        

    }
}