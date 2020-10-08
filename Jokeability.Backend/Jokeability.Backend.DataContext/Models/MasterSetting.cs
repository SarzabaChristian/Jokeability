using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Jokeability.Backend.DataContext.Models
{
    [Table("tblMasterSetting",Schema ="GS")]
    public class MasterSetting
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public bool isActive { get; set; }
    }
}
