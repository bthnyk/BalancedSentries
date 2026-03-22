using CG.Game;
using Gameplay.Utilities;
using Gameplay.CompositeWeapons;

namespace BalancedSentries
{
    class BlessedHomunculusPatcher
    {
        // ==========================================
        // SHIPS GUID's
        // ==========================================
        private static GUIDUnion destroyerShipGUID = new GUIDUnion("4bc2ff9e1d156c94a9c94286a7aaa79b");
        private static GUIDUnion strikerShipGUID = new GUIDUnion("d872500325a2d54498189cdfd4b788e7"); 
        private static GUIDUnion frigateShipGUID = new GUIDUnion("ac98ae5eb3b940747b0fbb7bd616b019");

        private static PPlayerCondition PPlayerCondition = new PPlayerCondition();

        public static void ApplyPatch(CarryableMod homunculus)
        {
            // IN-GAME GUID FINDER (Temporary Code)
            // BepinPlugin.Log.LogInfo($"[GUID FINDER] The GUID of the ship you are currently on: {ClientGame.Current.PlayerShip.assetGuid}");

            var currentShipGuid = ClientGame.Current.PlayerShip.assetGuid;

            // CONFIGURATION CHECKS: Is that the correct ship, and has it been enabled in the settings?
            bool isDestroyer = (currentShipGuid == destroyerShipGUID) && Configs.ActiveEnableOnDestroyer;
            bool isStriker = (currentShipGuid == strikerShipGUID) && Configs.ActiveEnableOnStriker;
            bool isFrigate = (currentShipGuid == frigateShipGUID) && Configs.ActiveEnableOnFrigate;

            // If the ship we're on has been disabled via the configuration, apply the patch
            if (!isDestroyer && !isStriker && !isFrigate) return;

            StatMod damageStatMod = null;
            StatMod powerWantedStatMod = null;

            // check whether mods still need to be applied
            foreach (StatMod currentModBeingApplied in homunculus.Modifiers)
            {
                // Stop if custom modifiers have already been applied
                if (currentModBeingApplied.DynamicCondition is PPlayerCondition) return;

                if (currentModBeingApplied.DynamicCondition is SinglePlayerModRule)
                {
                    if (currentModBeingApplied.Type == StatType.Damage.Id)
                    {
                        damageStatMod = currentModBeingApplied;
                    }
                    else if (currentModBeingApplied.Type == StatType.PowerWanted.Id)
                    {
                        powerWantedStatMod = currentModBeingApplied;
                    }
                }
            }

            // create new power wanted mod and add it to the homunculus
            if (powerWantedStatMod != null) {
                BepinPlugin.Log.LogInfo($"Applying P Power Wanted Mod to Blessed Homunculus");
                StatMod newMod0 = new PPowerWantedStatMod(powerWantedStatMod.TagConfiguration);
                newMod0.Mod.Source = homunculus;
                newMod0.Mod.InformationSource = homunculus;
                newMod0.DynamicCondition = PPlayerCondition;
                homunculus.Modifiers.Add(newMod0);
            }

            // create new damage mod and add it to the homunculus
            if (damageStatMod != null) {
                BepinPlugin.Log.LogInfo($"Applying P Damage Mod to Blessed Homunculus");
                StatMod newMod1 = new PDamageStatMod(damageStatMod.TagConfiguration);
                newMod1.Mod.Source = homunculus;
                newMod1.Mod.InformationSource = homunculus;
                newMod1.DynamicCondition = PPlayerCondition;
                homunculus.Modifiers.Add(newMod1);
            }

        }
    }
}
