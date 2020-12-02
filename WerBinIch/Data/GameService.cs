using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerBinIch.Data
{
    public class GameService
    {
        /// <summary>
        /// Stores all current games.
        /// </summary>
        private IList<Game> games = new List<Game>();
        
        /// <summary>
        /// Tries to find a game for the given game key.
        /// </summary>
        /// <param name="gameKey">Game key to identify the game.</param>
        /// <param name="game">Game for this key.</param>
        /// <returns>True if game was found, false if not.</returns>
        public bool TryFindGame(string gameKey, out Game game)
        {
            game = games.FirstOrDefault(g => g.Key.ToUpper() == gameKey.ToUpper());
            return game != null;
        }

        /// <summary>
        /// Hosts a new game. It will automatically be registered with the service.
        /// </summary>
        /// <returns>The newly hosted game.</returns>
        public Game HostNewGame()
        {
            var game = new Game() { Key = GenerateKey() };
            games.Add(game);
            return game;
        }

        public void EndGame(Game game)
        {
            games.Remove(game);
        }

        private string GenerateKey()
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUWVXYZ123456789";
            var rng = new Random();

            var keyCharacters = new char[6];
            for (int i = 0; i < keyCharacters.Length; i++)
            {
                keyCharacters[i] = characters[rng.Next(characters.Length)];
            }

            var key = new string(keyCharacters);

            // Make sure the key is unique.
            if (games.Any(g => g.Key.ToUpper() == key.ToUpper()))
            {
                return GenerateKey();
            }

            return key;
        }
    }
}
