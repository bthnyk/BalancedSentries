using VoidManager.CustomGUI;
using UnityEngine;

namespace BalancedSentries
{
    // ==========================================
    // 1. MENU: HOST (LOCAL) SETTINGS 
    // ==========================================
    internal class HostSettingsMenu : ModSettingsMenu
    {
        public override string Name() => "Balanced Sentries (Local/Host)";

        public override void Draw()
        {
            // CRITICAL UX WARNING: Re-equip note
            GUILayout.Label("<color=#ff4d4d><b>⚠️ IMPORTANT NOTE:</b></color>");
            GUILayout.Label("<color=yellow>If you change these settings during a match, you MUST unequip and re-equip the Homunculus for the visual and actual stat changes to take effect!</color>");
            GUILayout.Space(15);

            GUILayout.Label("<color=yellow>--- Local Settings ---</color>");
            GUILayout.Label("<color=#b3b3b3>These settings are saved to your PC and apply when YOU are the Host.</color>");
            GUILayout.Space(10);

            UnityEngine.GUI.changed = false;

            // --- SHIP TOGGLES ---
            GUILayout.Label("<b>Active Ships:</b> Select which ships will be affected by this mod.");
            GUILayout.Label("<color=#b3b3b3>[Default: All Active]</color>");
            Configs.LocalEnableOnDestroyer.Value = GUILayout.Toggle(Configs.LocalEnableOnDestroyer.Value, " Enable on Destroyer");
            Configs.LocalEnableOnStriker.Value = GUILayout.Toggle(Configs.LocalEnableOnStriker.Value, " Enable on Striker");
            Configs.LocalEnableOnFrigate.Value = GUILayout.Toggle(Configs.LocalEnableOnFrigate.Value, " Enable on Frigate");

            GUILayout.Space(15);
            
            // --- DAMAGE MULTIPLIER SLIDERS ---
            // The limits have been set to 0.25f and 1.25f using Mathf.Clamp.
            GUILayout.Label("<color=yellow>--- Damage Debuff Multipliers ---</color>");
            GUILayout.Label("<color=#b3b3b3>Adjust the sentry damage output (Min: 0.25, Max: 1.25).</color>");
            
            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"1-2 Players [Default: 1.00]: {Configs.LocalDamage1to2P.Value:F2}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalDamage1to2P.Value = Mathf.Clamp(Configs.LocalDamage1to2P.Value - 0.05f, 0.25f, 1.25f); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalDamage1to2P.Value = Mathf.Clamp(Configs.LocalDamage1to2P.Value + 0.05f, 0.25f, 1.25f); 
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"3-4 Players [Default: 0.75]: {Configs.LocalDamage3to4P.Value:F2}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalDamage3to4P.Value = Mathf.Clamp(Configs.LocalDamage3to4P.Value - 0.05f, 0.25f, 1.25f); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalDamage3to4P.Value = Mathf.Clamp(Configs.LocalDamage3to4P.Value + 0.05f, 0.25f, 1.25f); 
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"5-6 Players [Default: 0.50]: {Configs.LocalDamage5to6P.Value:F2}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalDamage5to6P.Value = Mathf.Clamp(Configs.LocalDamage5to6P.Value - 0.05f, 0.25f, 1.25f); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalDamage5to6P.Value = Mathf.Clamp(Configs.LocalDamage5to6P.Value + 0.05f, 0.25f, 1.25f); 
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            
            // --- POWER WANTED SLIDERS ---
            // Limits are clamped between -2 and 0 to prevent game-breaking power exploits.
            GUILayout.Label("<color=yellow>--- Power Consumption Settings ---</color>");
            GUILayout.Label("<color=#b3b3b3>Adjust how much power (-x) the sentry consumes based on player count.</color>");
            
            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"1-2 Players Power [Default: -2]: {Configs.LocalPower1to2P.Value}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalPower1to2P.Value = Mathf.Clamp(Configs.LocalPower1to2P.Value - 1, -2, 0); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalPower1to2P.Value = Mathf.Clamp(Configs.LocalPower1to2P.Value + 1, -2, 0); 
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"3-4 Players Power [Default: -1]: {Configs.LocalPower3to4P.Value}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalPower3to4P.Value = Mathf.Clamp(Configs.LocalPower3to4P.Value - 1, -2, 0); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalPower3to4P.Value = Mathf.Clamp(Configs.LocalPower3to4P.Value + 1, -2, 0); 
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(); 
            GUILayout.Label($"5-6 Players Power [Default: 0]: {Configs.LocalPower5to6P.Value}"); 
            if (GUILayout.Button("-", GUILayout.Width(25))) Configs.LocalPower5to6P.Value = Mathf.Clamp(Configs.LocalPower5to6P.Value - 1, -2, 0); 
            if (GUILayout.Button("+", GUILayout.Width(25))) Configs.LocalPower5to6P.Value = Mathf.Clamp(Configs.LocalPower5to6P.Value + 1, -2, 0); 
            GUILayout.EndHorizontal();

            if (UnityEngine.GUI.changed)
            {
                if (Configs.IsHost)
                {
                    Configs.ActiveEnableOnDestroyer = Configs.LocalEnableOnDestroyer.Value;
                    Configs.ActiveEnableOnStriker = Configs.LocalEnableOnStriker.Value;
                    Configs.ActiveEnableOnFrigate = Configs.LocalEnableOnFrigate.Value;

                    Configs.ActiveDamage1to2P = Configs.LocalDamage1to2P.Value;
                    Configs.ActiveDamage3to4P = Configs.LocalDamage3to4P.Value;
                    Configs.ActiveDamage5to6P = Configs.LocalDamage5to6P.Value;

                    Configs.ActivePower1to2P = Configs.LocalPower1to2P.Value;
                    Configs.ActivePower3to4P = Configs.LocalPower3to4P.Value;
                    Configs.ActivePower5to6P = Configs.LocalPower5to6P.Value;

                    ConfigSyncMessage.SendToClients();
                }
            }
        }
    }

    // ==========================================
    // 2. MENU: SERVER (ACTIVE) SETTINGS
    // ==========================================
    internal class ServerSettingsMenu : ModSettingsMenu
    {
        public override string Name() => "Balanced Sentries (Server)";

        public override void Draw()
        {
            // CRITICAL UX WARNING: Re-equip note
            GUILayout.Label("<color=#ff4d4d><b>⚠️ IMPORTANT NOTE:</b></color>");
            GUILayout.Label("<color=yellow>If the host changes these settings during a match, you MUST unequip and re-equip the Homunculus for the visual and actual stat changes to take effect!</color>");
            GUILayout.Space(15);

            GUILayout.Label("<color=cyan>--- Current Server Settings ---</color>");
            GUILayout.Label("<color=#b3b3b3>Read-only. These are the active settings enforced by the Host.</color>");
            GUILayout.Space(10);

            GUILayout.Label($"Destroyer: {(Configs.ActiveEnableOnDestroyer ? "<color=green>Enabled</color>" : "<color=red>Disabled</color>")}");
            GUILayout.Label($"Striker: {(Configs.ActiveEnableOnStriker ? "<color=green>Enabled</color>" : "<color=red>Disabled</color>")}");
            GUILayout.Label($"Frigate: {(Configs.ActiveEnableOnFrigate ? "<color=green>Enabled</color>" : "<color=red>Disabled</color>")}");

            GUILayout.Space(10);
            
            GUILayout.Label("<color=cyan>--- Damage Multipliers ---</color>");
            GUILayout.Label($"1-2 Players: {Configs.ActiveDamage1to2P:F2}");
            GUILayout.Label($"3-4 Players: {Configs.ActiveDamage3to4P:F2}");
            GUILayout.Label($"5-6 Players: {Configs.ActiveDamage5to6P:F2}");

            GUILayout.Space(10);

            GUILayout.Label("<color=cyan>--- Power Consumption ---</color>");
            GUILayout.Label($"1-2 Players: {Configs.ActivePower1to2P}");
            GUILayout.Label($"3-4 Players: {Configs.ActivePower3to4P}");
            GUILayout.Label($"5-6 Players: {Configs.ActivePower5to6P}");
        }
    }
}