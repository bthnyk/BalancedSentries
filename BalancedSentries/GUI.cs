using VoidManager.CustomGUI;
using UnityEngine;
using BepInEx.Configuration;

namespace BalancedSentries
{
    internal class HostSettingsMenu : ModSettingsMenu
    {
        public override string Name() => "Balanced Sentries (Host)";

        public override void Draw()
        {
            GUILayout.Label("<color=#ff4d4d><b>CRITICAL SYSTEM NOTICE</b></color>");
            GUILayout.BeginVertical("box");
            GUILayout.Label("<color=yellow>Modifications require a hardware reset to apply visually.</color>");
            GUILayout.Label("<color=#b3b3b3>Unequip/Re-equip the <b>Blessed Homunculus</b> after changes.</color>");
            GUILayout.EndVertical();
            GUILayout.Space(15);

            GUILayout.Label("<color=cyan><b>MOD LOGIC:</b></color>");
            GUILayout.Label("<color=#b3b3b3>Disabled in Solo (1 Player) to preserve the original Solo Buff.</color>");
            GUILayout.Space(10);

            UnityEngine.GUI.changed = false;

            // --- SHIPS ---
            GUILayout.Label("<color=yellow>--- Ship Compatibility ---</color>");
            Configs.LocalEnableOnDestroyer.Value = GUILayout.Toggle(Configs.LocalEnableOnDestroyer.Value, " Enable on Destroyer Class");
            Configs.LocalEnableOnStriker.Value = GUILayout.Toggle(Configs.LocalEnableOnStriker.Value, " Enable on Striker Class");
            Configs.LocalEnableOnFrigate.Value = GUILayout.Toggle(Configs.LocalEnableOnFrigate.Value, " Enable on Frigate Class");
            GUILayout.Space(15);

            // --- DAMAGE ---
            GUILayout.Label("<color=yellow>--- Damage Scaling Multipliers ---</color>");
            DrawRow("Duo (2P) [Default: 1.00]", Configs.LocalDamage2P, 0.05f, 0.25f, 1.00f, true);
            DrawRow("Standard (3-4P) [Default: 0.75]", Configs.LocalDamage3to4P, 0.05f, 0.25f, 1.00f, true);
            DrawRow("Large (5-6P) [Default: 0.50]", Configs.LocalDamage5to6P, 0.05f, 0.25f, 1.00f, true);
            DrawRow("More Crew (7+P) [Default: 0.25]", Configs.LocalDamage7Plus, 0.05f, 0.25f, 1.00f, true);
            GUILayout.Space(15);

            // --- POWER ---
            GUILayout.Label("<color=yellow>--- Power Consumption Demand ---</color>");
            DrawRow("Duo (2P) [Default: -2]", Configs.LocalPower2P, 1, -2, 1, false);
            DrawRow("Standard (3-4P) [Default: -1]", Configs.LocalPower3to4P, 1, -2, 1, false);
            DrawRow("Large (5-6P) [Default: 0]", Configs.LocalPower5to6P, 1, -2, 1, false);
            DrawRow("More Crew (7+P) [Default: 1]", Configs.LocalPower7Plus, 1, -2, 1, false);

            if (UnityEngine.GUI.changed && Configs.IsHost)
            {
                Configs.SyncLocalToActive();
                ConfigSyncMessage.SendToClients();
            }
        }

        private void DrawRow<T>(string label, ConfigEntry<T> config, float step, float min, float max, bool isFloat)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(180));
            string valStr = isFloat ? $"{System.Convert.ToSingle(config.Value):F2}" : config.Value.ToString();
            GUILayout.Label($"<b>{valStr}</b>", GUILayout.Width(50));
            
            if (GUILayout.Button("-", GUILayout.Width(30))) {
                if (isFloat) config.BoxedValue = Mathf.Clamp(System.Convert.ToSingle(config.Value) - step, min, max);
                else config.BoxedValue = (int)Mathf.Clamp(System.Convert.ToInt32(config.Value) - (int)step, min, max);
            }
            if (GUILayout.Button("+", GUILayout.Width(30))) {
                if (isFloat) config.BoxedValue = Mathf.Clamp(System.Convert.ToSingle(config.Value) + step, min, max);
                else config.BoxedValue = (int)Mathf.Clamp(System.Convert.ToInt32(config.Value) + (int)step, min, max);
            }
            GUILayout.EndHorizontal();
        }
    }

    internal class ServerSettingsMenu : ModSettingsMenu
    {
        public override string Name() => "Balanced Sentries (Server Status)";
        public override void Draw()
        {
            GUILayout.Label("<color=#ff4d4d><b>CRITICAL SYSTEM NOTICE</b></color>");
            GUILayout.BeginVertical("box");
            GUILayout.Label("<color=yellow>Modifications require a hardware reset to apply visually.</color>");
            GUILayout.Label("<color=#b3b3b3>Unequip/Re-equip the <b>Blessed Homunculus</b> after changes.</color>");
            GUILayout.EndVertical();
            GUILayout.Space(15);

            GUILayout.Label("<color=cyan>--- Active Server Profile ---</color>");
            GUILayout.Label($"Ships: {(Configs.ActiveEnableOnDestroyer?"DES ":"")}{(Configs.ActiveEnableOnStriker?"STR ":"")}{(Configs.ActiveEnableOnFrigate?"FRI":"")}");
            GUILayout.Space(10);
            
            GUILayout.BeginVertical("box");
            DrawInfo("Duo (2P)", Configs.ActiveDamage2P, Configs.ActivePower2P);
            DrawInfo("Standard (3-4P)", Configs.ActiveDamage3to4P, Configs.ActivePower3to4P);
            DrawInfo("Large (5-6P)", Configs.ActiveDamage5to6P, Configs.ActivePower5to6P);
            DrawInfo("Massive (7+P)", Configs.ActiveDamage7Plus, Configs.ActivePower7Plus);
            GUILayout.EndVertical();
        }
        private void DrawInfo(string l, float d, int p) => GUILayout.Label($"{l}: <color=cyan>{d:F2}x Dmg</color> | <color=orange>{p} Pwr</color>");
    }
}