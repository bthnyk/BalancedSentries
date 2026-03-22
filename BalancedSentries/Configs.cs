using BepInEx;
using BepInEx.Configuration;
using CG.Game; 

namespace BalancedSentries
{
    internal class Configs
    {
        internal static bool IsHost = true;

        // ==========================================
        // 1. LOCAL SETTINGS (Saved on Your Computer)
        // ==========================================
        // Ship Settings
        internal static ConfigEntry<bool> LocalEnableOnDestroyer;
        internal static ConfigEntry<bool> LocalEnableOnStriker;
        internal static ConfigEntry<bool> LocalEnableOnFrigate;

        // Damage Multipliers (Grouped)
        internal static ConfigEntry<float> LocalDamage1to2P;
        internal static ConfigEntry<float> LocalDamage3to4P;
        internal static ConfigEntry<float> LocalDamage5to6P;

        // Power Wanted Settings (Grouped)
        internal static ConfigEntry<int> LocalPower1to2P;
        internal static ConfigEntry<int> LocalPower3to4P;
        internal static ConfigEntry<int> LocalPower5to6P;

        // ==========================================
        // 2. ACTIVE / SERVER SETTINGS (Values Read by the Game)
        // ==========================================
        internal static bool ActiveEnableOnDestroyer;
        internal static bool ActiveEnableOnStriker;
        internal static bool ActiveEnableOnFrigate;

        internal static float ActiveDamage1to2P;
        internal static float ActiveDamage3to4P;
        internal static float ActiveDamage5to6P;

        internal static int ActivePower1to2P;
        internal static int ActivePower3to4P;
        internal static int ActivePower5to6P;

        // ==========================================
        // LOADING METHOD
        // ==========================================
        internal static void Load(BaseUnityPlugin plugin)
        {
            // Ship Binds
            LocalEnableOnDestroyer = plugin.Config.Bind("Ships", "Destroyer", true, "Should the mod be enabled on the destroyer?");
            LocalEnableOnStriker = plugin.Config.Bind("Ships", "Striker", true, "Should the mod be enabled on the striker?");
            LocalEnableOnFrigate = plugin.Config.Bind("Ships", "Frigate", true, "Should the mod be enabled on the frigate?");

            // Damage Binds
            LocalDamage1to2P = plugin.Config.Bind("Damage Scaling", "1-2_Players", 1.0f, "Damage multiplier for 1-2 players");
            LocalDamage3to4P = plugin.Config.Bind("Damage Scaling", "3-4_Players", 0.75f, "Damage multiplier for 3-4 players");
            LocalDamage5to6P = plugin.Config.Bind("Damage Scaling", "5-6_Players", 0.5f, "Damage multiplier for 5-6 players");

            // Power Wanted Binds
            LocalPower1to2P = plugin.Config.Bind("Power Scaling", "1-2_Players", -2, "Power required for 1-2 players");
            LocalPower3to4P = plugin.Config.Bind("Power Scaling", "3-4_Players", -1, "Power required for 3-4 players");
            LocalPower5to6P = plugin.Config.Bind("Power Scaling", "5-6_Players", 0, "Power required for 5-6 players");

            // Sync Active variables with Local variables on startup
            ActiveEnableOnDestroyer = LocalEnableOnDestroyer.Value;
            ActiveEnableOnStriker = LocalEnableOnStriker.Value;
            ActiveEnableOnFrigate = LocalEnableOnFrigate.Value;

            ActiveDamage1to2P = LocalDamage1to2P.Value;
            ActiveDamage3to4P = LocalDamage3to4P.Value;
            ActiveDamage5to6P = LocalDamage5to6P.Value;

            ActivePower1to2P = LocalPower1to2P.Value;
            ActivePower3to4P = LocalPower3to4P.Value;
            ActivePower5to6P = LocalPower5to6P.Value;
        }

        // ==========================================
        // IN-GAME UTILITY METHODS
        // ==========================================
        internal static float GetCurrentDamageMultiplier()
        {
            if (ClientGame.Current == null || ClientGame.Current.Players == null) return 1.0f;
            int count = ClientGame.Current.Players.Count;

            if (count <= 2) return ActiveDamage1to2P;
            if (count <= 4) return ActiveDamage3to4P;
            return ActiveDamage5to6P;
        }

        internal static int GetCurrentPowerWanted()
        {
            if (ClientGame.Current == null || ClientGame.Current.Players == null) return 0;
            int count = ClientGame.Current.Players.Count;

            if (count <= 2) return ActivePower1to2P;
            if (count <= 4) return ActivePower3to4P;
            return ActivePower5to6P;
        }
    }
}