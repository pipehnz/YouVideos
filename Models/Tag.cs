using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models {
    public class Tag : Base {
        [Required(ErrorMessage = "El campo 'Descripción' requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo 'Visibilidad' es requerido")]
        public bool EsVisible { get; set; }

        public ICollection<VideoTag> VideoTag { get; set; }
    }
}