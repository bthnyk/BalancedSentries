using Gameplay.Utilities;
using Gameplay.Tags;

namespace BalancedSentries
{
    class PPowerWantedStatMod : StatMod, IDescriptiveModifierSource
    {
        // Fetches the dynamic power requirement based on player count
        public PPowerWantedStatMod(ModTagConfiguration tagConfig) : base(new IntModifier(Configs.GetCurrentPowerWanted(), ModifierType.PrimaryAddend), StatType.PowerWanted.Id, tagConfig)
        {
        }

        public string GetDescription()
        {
            // Instead of just one value, we display a static table based on Configs.
            // This clearly shows how power consumption changes with player count.
            // We use different colors for a cleaner UI.
            string table = "";
            table += $"<color=cyan>1-2 Players:</color> {Configs.ActivePower1to2P}\n";
            table += $"<color=cyan>3-4 Players:</color> {Configs.ActivePower3to4P}\n";
            table += $"<color=cyan>5-6 Players:</color> {Configs.ActivePower5to6P}";

            return table;
        }

        public string GetHeader()
        {
            // Title for this section on the item description
            return "Balanced Sentry (Power Consump.)"; 
        }
    }
}