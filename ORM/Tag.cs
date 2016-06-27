namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
