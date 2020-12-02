using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerBinIch.Data
{
    public class Player
    {
        public string Name { get; init; } = string.Empty;

        public string Persona { get; set; } = string.Empty;

        public Player? PersonaCreator { get; set; } = null;

        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
