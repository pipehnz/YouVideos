using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models {
    public class Tag : Base {
        [Required(ErrorMessage = "El campo es 'Descripción' requerido")]
        public string Descripcion { get; set; }

        public ICollection<VideoTag> VideoTag { get; set; }
    }
}