using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBlog.Models
{
    public class ArticleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Column(TypeName = "ntext")]
        [StringLength(255)]
        public string Content { get; set; }
    }
}
