using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Buoi7_Annotation.Models
{
    public class TnkMember
    {
        [Key]
        public string TnkId { get; set; }
        [DisplayName("Tai khoan")]
        [Required(ErrorMessage ="Ten dang nhap khong duoc de trong")]
        [StringLength(100, MinimumLength =10, ErrorMessage ="Ten phai nam trong khoang 10 - 100")]
        public string TnkUserName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage ="Password khong duoc de trong")]
        [StringLength(100, MinimumLength = 8, ErrorMessage ="Mat khau phai co it nhat 8 ky tu")]
        [DataType(DataType.Password)]
        public string TnkPassword { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email khong duoc de trong")]
        public string TnkEmail {  get; set; }
        [DisplayName("So dien thoai")]
        [RegularExpression(@"^0\d{9,9}$", ErrorMessage ="So dien thoai phai tu du 10 ky tu va bat dau bang so 0")]
        public string TnkPhone {  get; set; }
    }
}
