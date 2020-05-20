# Ashes-Cinders-DS3-Mod-Switch
A tool to switch between Cinders and Champion's Ashes mods for Dark Souls 3.

This software is provided as-is. Please make sure to back up saves before use!
Provided under the GNU General Public License v3.0. (See LICENSE for more details.)

# Important
Cinders and Champion's Ashes, when played online, will both ban your account. Use only with caution. Normally, Cinders has a block on online play to protect users; I have disabled this by default. Please be very careful!

# Installation and Setup
Use bin/DkS3ModSwitcher.exe!

-First and foremost, make sure you run Dark Souls 3 unmodded beforehand. I would recommend creating a single character and saving.
-Download and install Cinders and Champion's Ashes. Refer to their individual mod pages to work through the installation. I would recommend installing Cinders before Ashes. 
-Run Ashes' launcher and click on the "Mod Disabled" button to enable the mod.
-You should be ready to run the mod switcher now.

Before you get started, it would probably be a good idea to know a few things:
1. Where your Dark Souls 3 executable is
2. Where your Dark Souls 3 save location is
3. Where the save data you want to use for Ashes, Cinders, and Vanilla are located (these could all be the same file!)

With that knowledge, we can start by running  'DkS3ModSwitcher.exe'.
The program should walk you through it but the steps in order are:
  1. Select DS3 executable folder. This is usually located in '[Your Steam Library]\steamapps\common\DARK SOULS III\Game' for the steam version.
  1.5. If there is not a copy of dinput8.dll (this should be in the game folder after installing the mods), you will need to point to a folder containing it.
  2. Select the folder the game uses for save data. Usually this is in C:\Users\[Your Username]\AppData\Roaming\DarkSoulsIII\[your save folder], though if you are having trouble finding it, you may need to go to View and check "Hidden Items" in Windows Explorer.
  3. Following this, you will need to select a save file for vanilla, Ashes, and Cinders. Each time you select a save file, a copy is made to AppData\Roaming\DS3 Mod Switch in an appropriate location. If you don't already have multiple save files, select the save file you DO have and then, in game after switching mods, delete the character slots that you do not want to play on that mod.
  4. Finally, the software will set itself to no mod at default and you are done with setup!
  
# Usage
1. Select the mod you want to use with the radio button and click "Switch Mod."

# Completely Uninstalling
1. Delete the executable
2. Delete the 'C:\Users\[your name]\AppData\Roaming\DS3 Mod Switch' folder
3. Delete the 'C:\Users\[your name]\AppData\Local\DkS3ModSwitcher' folder.

# Troubleshooting
1. Make sure that another window hasn't opened.
2. Follow steps above to completely uninstall and relaunch.

# Known Issues
-Windows pop under other windows making it look like nothing is happening.
-The program open in a small box in the corner of the screen and can be hard to see.
