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
        public event EventHandler? PersonaAssigned;
        public event EventHandler? PhaseChanged;

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
                OnPlayerJoined();
            }
        }

        public void Start()
        {
            Status = GameStatus.PersonaAssignment;
            AssignPersonaCreators();
            OnPhaseChanged();
        }

        public void AssingPersona(Player targetPlayer, string persona)
        {
            targetPlayer.Persona = persona;
            OnPersonaAssigned();
        }

        public void FinalizePersonaAssignments()
        {
            Status = GameStatus.Playing;
            OnPhaseChanged();
        }

        protected virtual void OnPlayerJoined()
        {
            PlayerJoined?.Invoke(this, new EventArgs());
        }

        protected virtual void OnPersonaAssigned()
        {
            PersonaAssigned?.Invoke(this, new EventArgs());
        }

        protected virtual void OnPhaseChanged()
        {
            PhaseChanged?.Invoke(this, new EventArgs());
        }

        private void AssignPersonaCreators()
        {
            // Use the randomly generated GUID to bring the players into an order.
            Players = Players.OrderBy(p => p.Guid).ToList();
            // Shift the order by one to get the assignments.
            var creators = Players.Skip(1).Concat(Players.Take(1)).ToList();
            
            // Merge the two lists into tuples that hold the assignments.
            var assignments = Players.Zip(creators);
            // Update the player list.
            foreach ((var player, var creator) in assignments)
            {
                // Since we have just passed references around, this will update the Players list.
                player.PersonaCreator = creator;
            }
        }
    }
}
