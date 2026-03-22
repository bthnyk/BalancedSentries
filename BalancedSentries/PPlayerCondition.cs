using System;
using CG.Game;
using Gameplay.Utilities;
using CG.Game.Player;

namespace BalancedSentries
{
    [Serializable]
    public class PPlayerCondition : ModDynamicCondition
    {
        public override void OnInitialize()
        {
            if (ClientGame.Current?.ModelEventBus != null)
            {
                ClientGame.Current.ModelEventBus.OnPlayerAdded.Subscribe(OnPlayerAdded);
                ClientGame.Current.ModelEventBus.OnPlayerRemoved.Subscribe(OnPlayerRemoved);
            }
        }

        public override void OnDestroy()
        {
            if (ClientGame.Current?.ModelEventBus != null)
            {
                ClientGame.Current.ModelEventBus.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
                ClientGame.Current.ModelEventBus.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
            }
        }

        private void OnPlayerRemoved(Player obj) => CheckIfActive();

        private void OnPlayerAdded(Player obj)
        {
            CheckIfActive();
            if (Configs.IsHost)
            {
                ConfigSyncMessage.SendToClients();
            }
        }

        // We set this to true consistently to bypass the game's "(Inactive)" tag logic.
        public override bool ShouldApply() => true;

        public override string Description()
        {
            // We check for null to prevent errors in the ship selection menu
            int playerCount = (ClientGame.Current?.Players != null) ? ClientGame.Current.Players.Count : 1;
            
            // Returns the active scaling status without extra tags if possible.
            return $"Condition: <color=#FFD700>Dynamic Player Scaling</color> [{playerCount}P]";
        }
    }
}