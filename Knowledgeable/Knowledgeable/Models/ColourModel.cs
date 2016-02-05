using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knowledgeable.Models
{
    public class ColourModel
    {
        public System.Guid ColourID { get; set; }
        public string ColourName { get; set; }
        public byte[] Hex { get; set; }
    }
}