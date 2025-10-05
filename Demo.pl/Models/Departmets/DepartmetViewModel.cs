using System.ComponentModel.DataAnnotations;

namespace Demo.pl.Models.Departmets
{
    public class DepartmetViewModel
    {

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
