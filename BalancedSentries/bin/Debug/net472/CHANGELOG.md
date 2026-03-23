## 1.1.0

* "The Fleet Expansion & UI Overhaul"
* Added:
* More Crew Support: Added scaling support for massive lobbies (7+ players). Perfect for those using increased crew size mods!
* Solo Protection: The mod now intelligently disables itself during Solo play (1 player) to prevent overlapping with the game's built-in "Solo Buff."
* Refined Player Grouping: Re-balanced scaling tiers into: Duo (2P), Standard (3-4P), Large (5-6P), and Massive (7+P).
* Fixes:
* Visual Sync Patch: Fixed a critical desync where clients would see incorrect power draw on the UI. Values now force-sync with the Host's configuration.
* Main Menu NullRef: Fixed a crash occurring when adjusting settings in the Main Menu before a session started.
* Config Isolation: Fixed a bug where local client configs could interfere with active server settings. Host settings are now the absolute "Source of Truth."
* UI/UX:
* Redesigned F5 Menu: Cleaner layout with categorized sections for Ships, Damage, and Power.
* Dynamic Headers: Tooltips now clearly state "Dynamic Sentry Scaling Active" along with the current player count, removing the confusing "(Inactive)" tag.
* Safety Clamps: Added hard limits to Damage (0.25x - 1.00x) and Power (-2 to +1) to prevent game-breaking exploits.

## 1.0.1

* Fixing visual bug sync problem.

## 1.0.0

* Initial release of Balanced Sentries.