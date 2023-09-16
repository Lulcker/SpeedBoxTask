using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskApiCdek.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Вес груза в граммах")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Габариты упаковки. Длина (в миллиметрах)")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Габариты упаковки. Ширина (в миллиметрах)")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Габариты упаковки. Высота (в миллиметрах)")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Фиас код города отправления")]
        public string? FiasFrom { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Display(Name = "Фиас код города получения")]
        public string? FiasTo { get; set; }
    }
}
