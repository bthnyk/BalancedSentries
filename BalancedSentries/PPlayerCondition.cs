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

        public override bool ShouldApply()
        {
            // Condition is only 'Met' when there is a crew of 2 or more.
            // This allows Solo players to keep the original game's Blessed Homunculus logic.
            return ClientGame.Current.Players.Count >= 2;
        }

        public override string Description()
        {
            // Check for null to prevent errors in the ship selection menu
            int playerCount = (ClientGame.Current?.Players != null) ? ClientGame.Current.Players.Count : 1;
            
            // Visual feedback for Solo vs Multi
            if (playerCount <= 1)
            {
                // Indicates that the mod is intentionally idle to allow original game buffs
                return "Condition: <color=#FFD700>Dynamic Scaling</color> [<color=#ff4d4d>Idle - Solo Mode</color>]";
            }
            
            // High-tech look for active scaling
            return $"Condition: <color=#FFD700>Dynamic Player Scaling</color> [<color=cyan>{playerCount} Players Active</color>]";
        }
    }
}