using VoidManager.ModMessages; 
using CG.Game; 

namespace BalancedSentries
{
    // ==========================================
    // NETWORK SYNC MESSAGE
    // ==========================================
    // This class handles broadcasting the active settings from the Host to all connected Clients.
    // It ensures everyone in the lobby has identical damage and power scaling for their sentries.
    public class ConfigSyncMessage : ModMessage
    {
        // --------------------------------------------------------
        // HOST SIDE: Packing and sending the data
        // --------------------------------------------------------
        public static void SendToClients()
        {
            if (!Configs.IsHost) return; // Only the host should broadcast settings

            // If there is no active game session, cancel the message.
            // This prevents the game from crashing while changing settings in the main menu.
            if (GameSessionManager.ActiveSession == null) return;

            // Package all current active settings into a single array
            object[] payload = new object[]
            {
                Configs.ActiveEnableOnDestroyer, // [0] bool
                Configs.ActiveEnableOnStriker,   // [1] bool
                Configs.ActiveEnableOnFrigate,   // [2] bool
                
                Configs.ActiveDamage1to2P,       // [3] float
                Configs.ActiveDamage3to4P,       // [4] float
                Configs.ActiveDamage5to6P,       // [5] float
                
                Configs.ActivePower1to2P,        // [6] int
                Configs.ActivePower3to4P,        // [7] int
                Configs.ActivePower5to6P         // [8] int
            };

            // Send the package to all clients. We explicitly cast null to Photon.Realtime.Player 
            // to tell VoidManager we want to broadcast to everyone, avoiding ambiguity errors.
            ModMessage.Send(MyPluginInfo.PLUGIN_GUID, "ConfigSyncMessage", (Photon.Realtime.Player)null, payload, true);
            
            BepinPlugin.Log.LogInfo("Host successfully broadcasted new Sentry configs to clients.");
        }

        // --------------------------------------------------------
        // CLIENT SIDE: Receiving and unpacking the data
        // --------------------------------------------------------
        public override void Handle(object[] arguments, Photon.Realtime.Player sender)
        {
            if (Configs.IsHost) return; // The host does not need to apply its own message

            try
            {
                // Unpack Ship Toggles
                Configs.ActiveEnableOnDestroyer = (bool)arguments[0];
                Configs.ActiveEnableOnStriker = (bool)arguments[1];
                Configs.ActiveEnableOnFrigate = (bool)arguments[2];

                // Unpack Damage Multipliers
                Configs.ActiveDamage1to2P = (float)arguments[3];
                Configs.ActiveDamage3to4P = (float)arguments[4];
                Configs.ActiveDamage5to6P = (float)arguments[5];

                // Unpack Power Wanted Requirements
                Configs.ActivePower1to2P = (int)arguments[6];
                Configs.ActivePower3to4P = (int)arguments[7];
                Configs.ActivePower5to6P = (int)arguments[8];

                BepinPlugin.Log.LogInfo("Successfully received and applied the Host's Balanced Sentries settings!");
            }
            catch (System.Exception e)
            {
                BepinPlugin.Log.LogError($"Failed to unpack sync message from host: {e.Message}");
            }
        }
    }
}