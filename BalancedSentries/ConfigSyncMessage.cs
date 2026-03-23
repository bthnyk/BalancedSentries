using VoidManager.ModMessages; 
using CG.Game; 

namespace BalancedSentries
{
    public class ConfigSyncMessage : ModMessage
    {
        public static void SendToClients()
        {
            if (!Configs.IsHost || GameSessionManager.ActiveSession == null) return;

            object[] payload = new object[]
            {
                Configs.ActiveEnableOnDestroyer, Configs.ActiveEnableOnStriker, Configs.ActiveEnableOnFrigate,
                Configs.ActiveDamage2P, Configs.ActiveDamage3to4P, Configs.ActiveDamage5to6P, Configs.ActiveDamage7Plus,
                Configs.ActivePower2P, Configs.ActivePower3to4P, Configs.ActivePower5to6P, Configs.ActivePower7Plus
            };

            ModMessage.Send(MyPluginInfo.PLUGIN_GUID, "ConfigSyncMessage", (Photon.Realtime.Player)null, payload, true);
        }

        public override void Handle(object[] arguments, Photon.Realtime.Player sender)
        {
            if (Configs.IsHost) return;
            try {
                Configs.ActiveEnableOnDestroyer = (bool)arguments[0];
                Configs.ActiveEnableOnStriker = (bool)arguments[1];
                Configs.ActiveEnableOnFrigate = (bool)arguments[2];
                Configs.ActiveDamage2P = (float)arguments[3];
                Configs.ActiveDamage3to4P = (float)arguments[4];
                Configs.ActiveDamage5to6P = (float)arguments[5];
                Configs.ActiveDamage7Plus = (float)arguments[6];
                Configs.ActivePower2P = (int)arguments[7];
                Configs.ActivePower3to4P = (int)arguments[8];
                Configs.ActivePower5to6P = (int)arguments[9];
                Configs.ActivePower7Plus = (int)arguments[10];
            } catch { /* Unpack error handling */ }
        }
    }
}