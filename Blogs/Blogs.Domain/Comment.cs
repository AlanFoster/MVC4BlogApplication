using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual User Poster { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
/*        [Column("DateCreated", Order = 1, TypeName = "date"),*/
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? PostDate { get; set; }
    }
}
