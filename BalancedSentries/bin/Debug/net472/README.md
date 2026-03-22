[![](https://img.shields.io/badge/-bthnyk-111111?style=just-the-label&logo=github&labelColor=24292f)](https://github.com/bthnyk)
![](https://img.shields.io/badge/Game%20Version-v0.28.x-111111?style=flat&labelColor=24292f&color=111111)
[![](https://img.shields.io/discord/1180651062550593536.svg?&logo=discord&logoColor=ffffff&style=flat&label=Discord&labelColor=24292f&color=111111)](https://discord.gg/g2u5wpbMGu "Void Crew Modding Discord")

# Balanced Sentries

Version 1.0.1  
For Game Version 1.1.0  
Developed by HeX, Airborne  
Requires:  BepInEx-BepInExPack-5.4.2100, NihilityShift-VoidManager-1.2.8


---------------------

### 💡 Function(s)

- **Dynamic Scaling:** Automatically adjusts Sentry Turret damage and power consumption based on the number of players in the lobby.
- **Configurable Balance:** Use the F5 Menu (Void Manager) to tweak multipliers for different crew sizes.
- **Host Sync:** The Host controls the balance for the entire session, ensuring a consistent experience for all crew members.

**Default Dynamic Scaling Table:**

| Effect | 1-2 Players | 3-4 Players | 5-6 Players |
| ------ | :---------: | :---------: | :---------: |
| **B.R.A.I.N. Damage** | +100% (1.0) | +75% (0.75) | +50% (0.50) |
| **B.R.A.I.N. Power Usage** | -2 | -1 | 0 |

*Note: Damage and Power values are fully customizable via the Host Settings menu.*

### 🎮 Client Usage

- Simply install. 
- Use **F5** to access the **Balanced Sentries (Local/Host)** and **Balanced Sentries (Server)** menus.
- **⚠️ IMPORTANT:** If the Host changes settings mid-game, you must unequip and re-equip Sentry Brain modules for changes to take effect visually.

### 👥 Multiplayer Functionality

- ✅ **All**
  - All players must have this mod installed to ensure the UI and network sync function correctly.

---------------------

## 🔧 Install Instructions - **Install following the normal BepInEx procedure.**

Ensure that you have [BepInEx 5](https://thunderstore.io/c/void-crew/p/BepInEx/BepInExPack/) (stable version 5 **MONO**) and [VoidManager](https://thunderstore.io/c/void-crew/p/NihilityShift/VoidManager/) installed.

#### ✔️ Mod installation - **Unzip the contents into the BepInEx plugin directory**

Drag and drop `BalancedSentries.dll` into `Void Crew\BepInEx\plugins`
