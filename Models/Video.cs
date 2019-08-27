using System.ComponentModel.DataAnnotations;

namespace HelloWorldMVC.Models {
    public class Video : Base {
        [Required(ErrorMessage = "El campo es 'Titulo' requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo es 'Descripción' requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo es 'Url' requerido")]
        public string Url { get; set; }
    }
}