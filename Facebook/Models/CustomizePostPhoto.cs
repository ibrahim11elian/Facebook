using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Facebook.Models
{
    [MetadataType(typeof(postPhotos))]
    public partial class PostPhoto
    {
    }
    public class postPhotos
    {
        public int Id { get; set; }
        public Nullable<int> postID { get; set; }

        [FileExtensions(Extensions ="jpg,jpeg,png")]
        [DataType(DataType.ImageUrl)]
        public string pphoto { get; set; }

        public virtual Post Post { get; set; }
    }
}