using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra02.Models
{
    public class LopHoc
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống tên lớp học")]
        [Display(Name = "Tên lớp học")]

        public string TenLopHoc { get; set; }
        [Required(ErrorMessage = "Không được để trống năm học")]
        [Display(Name = "Năm nhập học")]

        public string NamNhapHoc { get; set; }
        [Required(ErrorMessage = "Không được để năm ra trường")]
        [Display(Name = "Năm ra trường")]

        public string NamRaTruong { get; set; }
        [Required(ErrorMessage = "Không được để trống số lượng sinh viên")]
        [Display(Name = "Số lượng sinh viên")]

        public string SoLuongSinhVien { get; set; }
    }
}
