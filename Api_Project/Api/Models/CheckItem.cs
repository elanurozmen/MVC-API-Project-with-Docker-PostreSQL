using System.ComponentModel.DataAnnotations;

namespace Api_Project.Models
{
    public class CheckItem
        {
            [Key]
            public int checkId { get; set; }
            public string? customerName { get; set; }
            public float checkAmount { get; set; }
            public bool checkStatus{get;set;}
        }
}


