using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models {
    public class Video : Base {
        [Required(ErrorMessage = "El campo 'Titulo' es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo 'Descripción' es requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo 'Url' es requerido")]
        public string Url { get; set; }

        public ICollection<VideoTag> VideoTag { get; set; }
    }
}