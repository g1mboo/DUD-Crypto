using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Discord.Assembly
{
    public class ArgumentInfo
    {
        public string Name { get; }
        public string Description { get; }

        public ArgumentInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
