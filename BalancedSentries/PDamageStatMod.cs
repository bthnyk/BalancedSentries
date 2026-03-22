using Gameplay.Utilities;
using Gameplay.Tags;

namespace BalancedSentries
{
    class PDamageStatMod : StatMod, IDescriptiveModifierSource
    {
        // Fetches the dynamic damage multiplier based on player count
        public PDamageStatMod(ModTagConfiguration tagConfig) : base(new FloatModifier(Configs.GetCurrentDamageMultiplier(), ModifierType.AdditiveMultiplier), StatType.Damage.Id, tagConfig)
        {
        }

        public string GetDescription()
        {
            // Converting float multipliers (0.50) to UI percentages (+50% or -50%)
            // and displaying them in a static table format.

            float val1to2P = Configs.ActiveDamage1to2P * 100f;
            float val3to4P = Configs.ActiveDamage3to4P * 100f;
            float val5to6P = Configs.ActiveDamage5to6P * 100f;

            // Simple check to determine the sign (+/-)
            string sign1to2P = val1to2P >= 0 ? "+" : "";
            string sign3to4P = val3to4P >= 0 ? "+" : "";
            string sign5to6P = val5to6P >= 0 ? "+" : "";

            // Displaying the table
            string table = "";
            table += $"<color=cyan>1-2 Players:</color> {sign1to2P}{val1to2P:F0}%\n";
            table += $"<color=cyan>3-4 Players:</color> {sign3to4P}{val3to4P:F0}%\n";
            table += $"<color=cyan>5-6 Players:</color> {sign5to6P}{val5to6P:F0}%";

            return table;
        }

        public string GetHeader()
        {
            // Title for this section on the item description
            return "Balanced Sentry (Damage Scal.)";
        }
    }
}