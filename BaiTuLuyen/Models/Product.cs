using System.ComponentModel.DataAnnotations;

namespace BaiTuLuyen.Models
{
    public class Product : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; } = string.Empty;

        // Dùng để hứng file upload từ Form (Không lưu trong DB)
        [Display(Name = "Chọn ảnh sản phẩm")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        [Range(10000, double.MaxValue, ErrorMessage = "Giá sản phẩm phải từ 10.000đ trở lên")]
        [Display(Name = "Giá chuẩn")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá khuyến mãi")]
        [Range(0, float.MaxValue, ErrorMessage = "Giá khuyến mãi không được âm")]
        [Display(Name = "Giá khuyến mãi")]
        public float SalePrice { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm")]
        [StringLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SalePrice >= Price * 0.9f)
            {
                yield return new ValidationResult(
                    $"Giá khuyến mãi ({SalePrice:N0}đ) phải nhỏ hơn giá chuẩn ({Price:N0}đ) ít nhất 10% (Tối đa {Price * 0.9f:N0}đ)",
                    new[] { nameof(SalePrice) }
                );
            }

            if (string.IsNullOrEmpty(Image) && ImageFile == null)
            {
                yield return new ValidationResult(
                    "Vui lòng chọn file hình ảnh sản phẩm",
                    new[] { nameof(ImageFile) }
                );
            }

            var sensitiveWords = new[] { "die", "admin", "fack", "hack", "fuck" };
            if (!string.IsNullOrEmpty(Description))
            {
                foreach (var word in sensitiveWords)
                {
                    if (Description.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        yield return new ValidationResult(
                            $"Mô tả chứa từ ngữ nhạy cảm không hợp lệ ('{word}')",
                            new[] { nameof(Description) }
                        );
                        break;
                    }
                }
            }
        }
    }
}
