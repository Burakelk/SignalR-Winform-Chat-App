using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProje.Model
{
    public class MessageProperties
    {
        [Key] public int MessageId {  get; set; }
        public string Message { get; set; }
        public int User1Id {  get; set; }
        public int User2Id { get; set; }

    }
}
