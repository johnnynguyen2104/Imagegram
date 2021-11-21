using System;
using System.Collections.Generic;
using System.Text;

namespace Imagegram.Functions.Dtos
{
    public class SinglePostCreation
    {
        public int PostBy { get; set; }

        public string Caption { get; set; }

        public byte[] Image { get; set; }
    }
}
