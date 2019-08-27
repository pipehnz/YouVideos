using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models {
    public class Base {
        [Key]
        public int Id { get; set; }
    }
}