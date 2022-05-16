using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Facebook.Models
{
    [MetadataType(typeof(MetaDataPost))]
    public partial class Post
    {
    }

    public class MetaDataPost
    {
        public int Id { get; set; }
        [Display(Name ="Post Content")]
        public string content { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> date { get; set; }

        [Display(Name ="Privacy")]
        [Required]
        public string privacy { get; set; }
    }
}