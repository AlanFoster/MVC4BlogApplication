using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual User Poster { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual List<Comment> Comments { get; set; } 
        // Note, this is MSSQL's 'date' format type - should this ever be used explicitly?
        //[Column(TypeName = "date")]
   /*     [Column("DateStarted", Order = 1, TypeName = "date"),
        DatabaseGenerated(DatabaseGeneratedOption.Computed)]*/
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? PostDate { get; set; }
    }
}
