using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsManager.Common.Models
{
    public class User
    {
        public Guid id {  get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
}
