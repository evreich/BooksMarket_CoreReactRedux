using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QPD.DBUpdaters
{
    public abstract class DBModel
    {
        [Key]
        public int Id { get; set; }
    }
}
