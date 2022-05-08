//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Facebook.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            this.CommentLikes = new HashSet<CommentLike>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> Cdate { get; set; }
        public string content { get; set; }
        public Nullable<int> postID { get; set; }
        public Nullable<int> userID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}