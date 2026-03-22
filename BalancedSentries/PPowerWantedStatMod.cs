using Gameplay.Utilities;
using Gameplay.Tags;
using CG.Game;

namespace BalancedSentries
{
    class PPowerWantedStatMod : StatMod, IDescriptiveModifierSource
    {
        // We pass the dynamic value directly to the base constructor.
        // Since the class is re-instantiated when the item is equipped, 
        // the latest config value will be applied automatically.
        public PPowerWantedStatMod(ModTagConfiguration tagConfig) : base(new IntModifier(Configs.GetCurrentPowerWanted(), ModifierType.PrimaryAddend), StatType.PowerWanted.Id, tagConfig)
        {
        }

        public string GetDescription()
        {
            // We return an empty string to prevent the UI from appending "(Inactive)".
            // This keeps the item tooltip clean and professional.
            return ""; 
        }

        public string GetHeader()
        {
            // We provide visual confirmation here, as headers are immune to the "(Inactive)" tag.
            int playerCount = (ClientGame.Current?.Players != null) ? ClientGame.Current.Players.Count : 1;
            return $"Dynamic Sentry Scaling Active ({playerCount}P)"; 
        }
    }
}