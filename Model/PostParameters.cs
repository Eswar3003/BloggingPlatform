using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common;

namespace Model
{
    public class PostParameters : InputParameters
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public PostFiltering? Filtering { get; set; }
    }
}
