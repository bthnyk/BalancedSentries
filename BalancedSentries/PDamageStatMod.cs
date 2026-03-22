using Gameplay.Utilities;
using Gameplay.Tags;
using CG.Game;

namespace BalancedSentries
{
    class PDamageStatMod : StatMod, IDescriptiveModifierSource
    {
        // We pass the dynamic damage multiplier directly to the base constructor.
        // The mod will use the latest multiplier from Configs whenever the item is equipped.
        public PDamageStatMod(ModTagConfiguration tagConfig) : base(new FloatModifier(Configs.GetCurrentDamageMultiplier(), ModifierType.AdditiveMultiplier), StatType.Damage.Id, tagConfig)
        {
        }

        public string GetDescription()
        {
            // We return an empty string to prevent the UI from appending the "(Inactive)" tag.
            // This ensures a clean and professional look in the item tooltip.
            return ""; 
        }

        public string GetHeader()
        {
            // We use the Header to provide visual confirmation since it's immune to "(Inactive)" tags.
            // It also displays the current player count for clarity.
            int playerCount = (ClientGame.Current?.Players != null) ? ClientGame.Current.Players.Count : 1;
            return $"Dynamic Sentry Damage Active ({playerCount}P)";
        }
    }
}