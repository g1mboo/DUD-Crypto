using System;
using System.Collections.Generic;
using System.Text;

namespace DUDCrypto.Model.Discord.Assembly
{
    public class CommandInfo
    {
        public string Name { get; }
        public string Description { get; }

        public List<ArgumentInfo> Arguments { get; }

        public CommandInfo(string name, string description, ArgumentInfo[] arguments)
        {
            Name = name;
            Description = description;
            Arguments = new List<ArgumentInfo>(arguments);
        }
    }
}
