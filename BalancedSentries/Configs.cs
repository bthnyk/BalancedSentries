using BepInEx;
using BepInEx.Configuration;
using CG.Game; 

namespace BalancedSentries
{
    internal class Configs
    {
        internal static bool IsHost = true;

        // --- LOCAL SETTINGS (File Storage) ---
        internal static ConfigEntry<bool> LocalEnableOnDestroyer, LocalEnableOnStriker, LocalEnableOnFrigate;
        internal static ConfigEntry<float> LocalDamage2P, LocalDamage3to4P, LocalDamage5to6P, LocalDamage7Plus;
        internal static ConfigEntry<int> LocalPower2P, LocalPower3to4P, LocalPower5to6P, LocalPower7Plus;

        // --- ACTIVE SETTINGS (In-Game Runtime) ---
        internal static bool ActiveEnableOnDestroyer, ActiveEnableOnStriker, ActiveEnableOnFrigate;
        internal static float ActiveDamage2P, ActiveDamage3to4P, ActiveDamage5to6P, ActiveDamage7Plus;
        internal static int ActivePower2P, ActivePower3to4P, ActivePower5to6P, ActivePower7Plus;

        internal static void Load(BaseUnityPlugin plugin)
        {
            // Ship Binds
            LocalEnableOnDestroyer = plugin.Config.Bind("Ships", "Destroyer", true, "Enable on Destroyer");
            LocalEnableOnStriker = plugin.Config.Bind("Ships", "Striker", true, "Enable on Striker");
            LocalEnableOnFrigate = plugin.Config.Bind("Ships", "Frigate", true, "Enable on Frigate");

            // Damage Binds (New Grouping)
            LocalDamage2P = plugin.Config.Bind("Damage", "2_Players", 1.0f, "Damage for Duo");
            LocalDamage3to4P = plugin.Config.Bind("Damage", "3-4_Players", 0.75f, "Damage for Standard Crew");
            LocalDamage5to6P = plugin.Config.Bind("Damage", "5-6_Players", 0.50f, "Damage for Large Crew");
            LocalDamage7Plus = plugin.Config.Bind("Damage", "7+_Players", 0.25f, "Damage for More Crew lobbies");

            // Power Binds (New Grouping)
            LocalPower2P = plugin.Config.Bind("Power", "2_Players", -2, "Power for Duo");
            LocalPower3to4P = plugin.Config.Bind("Power", "3-4_Players", -1, "Power for Standard Crew");
            LocalPower5to6P = plugin.Config.Bind("Power", "5-6_Players", 0, "Power for Large Crew");
            LocalPower7Plus = plugin.Config.Bind("Power", "7+_Players", 1, "Power for More Crew lobbies");

            SyncLocalToActive();
        }

        internal static void SyncLocalToActive()
        {
            ActiveEnableOnDestroyer = LocalEnableOnDestroyer.Value;
            ActiveEnableOnStriker = LocalEnableOnStriker.Value;
            ActiveEnableOnFrigate = LocalEnableOnFrigate.Value;

            ActiveDamage2P = LocalDamage2P.Value;
            ActiveDamage3to4P = LocalDamage3to4P.Value;
            ActiveDamage5to6P = LocalDamage5to6P.Value;
            ActiveDamage7Plus = LocalDamage7Plus.Value;

            ActivePower2P = LocalPower2P.Value;
            ActivePower3to4P = LocalPower3to4P.Value;
            ActivePower5to6P = LocalPower5to6P.Value;
            ActivePower7Plus = LocalPower7Plus.Value;
        }

        internal static float GetCurrentDamageMultiplier()
        {
            if (ClientGame.Current?.Players == null) return 1.0f;
            int count = ClientGame.Current.Players.Count;

            if (count <= 1) return 0f; // Disable mod for Solo to keep original game buff
            if (count == 2) return ActiveDamage2P;
            if (count <= 4) return ActiveDamage3to4P;
            if (count <= 6) return ActiveDamage5to6P;
            return ActiveDamage7Plus;
        }

        internal static int GetCurrentPowerWanted()
        {
            if (ClientGame.Current?.Players == null) return 0;
            int count = ClientGame.Current.Players.Count;

            if (count <= 1) return 0; // Disable mod for Solo
            if (count == 2) return ActivePower2P;
            if (count <= 4) return ActivePower3to4P;
            if (count <= 6) return ActivePower5to6P;
            return ActivePower7Plus;
        }
    }
}