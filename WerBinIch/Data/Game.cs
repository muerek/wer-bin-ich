using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerBinIch.Data
{
    public class Game
    {
        /// <summary>
        /// Unique key that allows finding and joining the game.
        /// </summary>
        public string Key { get; init; } = string.Empty;

        public GameStatus Status { get; private set; }

        public IList<Player> Players { get; private set; }

        public event EventHandler? PlayerJoined;
        public event EventHandler? StatusChanged;

        public Game()
        {
            Status = GameStatus.PlayersJoining;
            Players = new List<Player>();
        }

        public void Join(Player player)
        {
            if (Status == GameStatus.PlayersJoining)
            {
                Players.Add(player);
                PlayerJoined?.Invoke(this, new EventArgs());
            }
        }

        public void Start()
        {
            Status = GameStatus.PersonaAssignment;
            StatusChanged?.Invoke(this, new EventArgs());
        }

        public void AssignPersonas()
        {
            Status = GameStatus.Playing;
            StatusChanged?.Invoke(this, new EventArgs());
        }
    }

    public enum GameStatus
    {
        PlayersJoining,
        PersonaAssignment,
        Playing
    }
}
