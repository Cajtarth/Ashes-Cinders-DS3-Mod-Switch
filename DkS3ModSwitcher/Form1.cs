//Dark Souls 3 Mod Switcher - For switching between Ashes and Cinders mods in Dark Souls 3.
//Copyright (C) 2020 T. A. McNamara
//Provided as-is; use at own risk!

/* 
 * This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DkS3ModSwitcher
{
    enum State
    {
        Ashes,
        Cinders,
        None,
        ModInstalled
    }

    public partial class Form1 : Form
    {
        State state;

        string dinputFileName = "\\dinput8.dll";
        string dinputFullPath;

        string darkSouls3Path;

        bool hasAshesFile;
        bool hasCindersFile;

        bool modIsInstalled;

        bool changesMade = false;

        string appDataFolder;

        string ashesFileFolder;
        string cindersFileFolder;

        string ashesFilePath;
        string cindersFilePath;

        string saveOriginalPath;
        string saveAppDataFolderName = "\\save";
        string saveAppDataPath;
        string saveDataName = "\\DS30000.sl2";

        bool firstRun;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            firstRun = Properties.Settings.Default.FirstRun;
            state = (State)Properties.Settings.Default.State;
            ChangeStateLabel();
            appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DS3 Mod Switch";
            ashesFileFolder = appDataFolder + "\\ashes";
            cindersFileFolder = appDataFolder + "\\cinders";
            ashesFilePath = ashesFileFolder + "\\modengine.ini";
            cindersFilePath = cindersFileFolder + "\\modengine.ini";

            darkSouls3Path = Properties.Settings.Default.DarkSouls3Path;
            if (firstRun)
            {
                MessageBox.Show("Hello! It looks like this is your first run. You'll have to do a bit of setup but this should be on your first run only. Please pay attention to if you need to select a folder or file.", "First Time Setup");
            }

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }

            Debug.WriteLine("Checking for game exe...");
            bool pathFound = false;
            while (!pathFound)
            {
                if (darkSouls3Path == "" || !File.Exists(darkSouls3Path + "\\DarkSoulsIII.exe"))
                {
                    Debug.WriteLine("No path found. Opening browser...");
                    MessageBox.Show("Please select the folder that contains the executable for Dark Souls 3.", "Dark Souls 3 executable");
                    var fbd = new FolderBrowserDialog();
                    fbd.Description = "Please select the folder that has DarkSoulsIII.exe";
                    using (fbd)
                    {
                        DialogResult result = fbd.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            if (File.Exists(fbd.SelectedPath + "\\DarkSoulsIII.exe"))
                            {
                                Debug.WriteLine("OK pressed; copying path...");
                                darkSouls3Path = fbd.SelectedPath;
                                Properties.Settings.Default.DarkSouls3Path = darkSouls3Path;
                                changesMade = true;
                                pathFound = true;
                            }
                            else
                            {
                                MessageBox.Show("Couldn't find the executable there. Try again.", "Please try again.");
                            }
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            Application.Exit();
                            return;
                        }
                    }
                }
                else
                {
                    pathFound = true;
                }
            }

            if (!(darkSouls3Path == "") && File.Exists(darkSouls3Path + "\\DarkSoulsIII.exe"))
            {
                Debug.WriteLine("Found at " + darkSouls3Path + "!");
            }
            else
            {
                Application.Exit();
                return;
            }

            Debug.WriteLine("Looking for dinput8.dll...");

            dinputFullPath = darkSouls3Path + dinputFileName;

            if (firstRun || (!File.Exists(dinputFullPath) && state != State.None))
            {
                MessageBox.Show("I need to ensure that I can find dinput8.dll from modengine.", "Next Step");
            }
            if (File.Exists(dinputFullPath))
            {
                Debug.WriteLine("Found at " + dinputFullPath + "!");
                modIsInstalled = true;
                if (!File.Exists(appDataFolder + dinputFileName))
                {
                    MessageBox.Show("I found dinput8.dll in the game folder but need to copy it to the application data folder.", "Next Step");
                    Debug.WriteLine("Not found in app data folder so copying...");
                    File.Copy(dinputFullPath, appDataFolder + dinputFileName);
                }
            }
            else
            {
                modIsInstalled = false;
                if (!File.Exists(appDataFolder + dinputFileName))
                {
                    bool dFound = false;
                    Debug.WriteLine(Environment.CurrentDirectory + dinputFileName);
                    if (File.Exists(Environment.CurrentDirectory + dinputFileName))
                    {
                        if (firstRun)
                        {
                            MessageBox.Show("I need to make a copy of the dinput8.dll in the local folder.", "Next Step");
                        }
                        Debug.WriteLine("Found in application folder! Copying...");
                        File.Copy(Environment.CurrentDirectory + dinputFileName, appDataFolder + dinputFileName);
                        dFound = true;
                    }
                    else
                    {
                        MessageBox.Show("Couldn't find dinput8.dll from modengine. Please point me to a folder that contains it.", "dinput8.dll Needed");
                        Debug.WriteLine("Not found anywhere! Opening browser...");
                    }
                    while (!dFound)
                    {
                        var fbd = new FolderBrowserDialog();
                        fbd.Description = "Please select the folder that has dinput8.dll";
                        using (fbd)
                        {
                            DialogResult result = fbd.ShowDialog();
                            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                            {
                                if (File.Exists(fbd.SelectedPath + dinputFileName))
                                {
                                    Debug.WriteLine("Found! Copying...");
                                    File.Copy(fbd.SelectedPath + dinputFileName, appDataFolder + dinputFileName);
                                    dFound = true;
                                }
                                else
                                {
                                    MessageBox.Show("Unable to find dinput8.dll at that location!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (result == DialogResult.Cancel)
                            {
                                Application.Exit();
                                return;
                            }
                        }
                    }
                }
            }

            Debug.WriteLine("Looking for ashes file...");
            if (!File.Exists(ashesFilePath))
            {
                Debug.WriteLine("Not found! Creating...");
                if (!Directory.Exists(ashesFileFolder))
                {
                    Directory.CreateDirectory(ashesFileFolder);
                }
                StreamWriter sw;
                sw = File.CreateText(ashesFilePath);
                using (sw)
                {
                    sw.Write("; Mod Engine configuration file\n; Mod Engine (C) 2019 Katalash. All Rights Reserved.\n; Mod Engine is a configurable injection DLL used to modify some aspects of the\n; game to make it more friendly for modding. See the following properties that can\n; be configured.\n[online]\n; Uses low level hook to block game from ever connecting to the internet.\n; This guarantees you won't get banned while playing mods as long as this is\n; enabled. Mod engine uses code injection which is scanned for by the anticheat,\n; so turn this option off at your own risk.\n; You will get banned attempting to play with mods online!\n; Note that if you wish to play a mod online with friends, it is recommended to setup\n; an alternate steam account, share DS3 to it with family sharing, and disable this option\n; on that account only. You will be shadowbanned and be able to play with friends on the\n; ban servers. Make sure this option is enabled again before launching DS3 on your main\n; account or you will risk a ban on it.\nblockNetworkAccess=0\n[savefile]\n; Uses an alternate save file while this is enabled. Playing mods with a separate save\n; file is highly recommended, as save files are analyzed by the server while online and can\n; be used to ban. Using a separate save file with network access blocked guarantees your mod\n; save file is never sent to the server and used to ban. Disable at your own risk!\nuseAlternateSaveFile=0\n[files]\n; Loads loose param data from files instead of from encrypted data0.bdt archive. This is mod\n; specific and should only be enabled by modders who know what they are doing. End users should\n; have no reason to touch this.\nloadLooseParams=1\n; Loads extracted files from UXM instead of data from the archives. Requires a complete UXM extraction\n; and should generally only be used by mod creators.\nloadUXMFiles=0\n; If enabled, a mod will be loaded from a specified override directory.\nuseModOverrideDirectory=1\n; The directory from which to load a mod.\nmodOverrideDirectory=\"\\AshesLauncher\\Ashes\\_ashes\"\n; Caches results of looking up override files. Can speed up loading and reduce hitching, but may require game\n; restart when a file is added or removed from a mod. Mod developers probably want this disabled, while mod\n; users (and released mods) should have this enabled.\ncacheFilePaths=1\n[debug]\n; Shows the debug console when running the game. Can be useful for modders to troubleshoot\nshowDebugLog=0\n; Gameplay asm patches that can be enabled and used by mod creators\n[gameplay]\n; Restores bonfire sacrifice menu and mechanic for mods that require it\nrestoreBonfireSacrifice=0\n[misc]\n; Chain loads another dll that hooks dinput8.dll\n; For example, if you have another dll mod that's named dinput8.dll, you can rename it to\n; othermod.dll or something, place it in the Sekiro directory, and set this path to\n; chainDInput8DLLPath=\"\\othermod.dll\" or whatever you named the dll\nchainDInput8DLLPath=\"\"");
                }
                hasAshesFile = true;
            }
            else
            {
                hasAshesFile = true;
                Debug.WriteLine("Found it!");
            }

            Debug.WriteLine("Looking for cinders file...");
            if (!File.Exists(cindersFilePath))
            {
                Debug.WriteLine("Not found! Creating...");
                if (!Directory.Exists(cindersFileFolder))
                {
                    Directory.CreateDirectory(cindersFileFolder);
                }
                StreamWriter sw;
                sw = File.CreateText(cindersFilePath);
                using (sw)
                {
                    sw.Write("; Mod Engine configuration file\n; Mod Engine (C) 2019 Katalash. All Rights Reserved.\n; Mod Engine is a configurable injection DLL used to modify some aspects of the\n; game to make it more friendly for modding. See the following properties that can\n; be configured.\n[online]\n; Uses low level hook to block game from ever connecting to the internet.\n; This guarantees you won't get banned while playing mods as long as this is\n; enabled. Mod engine uses code injection which is scanned for by the anticheat,\n; so turn this option off at your own risk.\n; You will get banned attempting to play with mods online!\n; Note that if you wish to play a mod online with friends, it is recommended to setup\n; an alternate steam account, share DS3 to it with family sharing, and disable this option\n; on that account only. You will be shadowbanned and be able to play with friends on the\n; ban servers. Make sure this option is enabled again before launching DS3 on your main\n; account or you will risk a ban on it.\nblockNetworkAccess=0\n[savefile]\n; Uses an alternate save file while this is enabled. Playing mods with a separate save\n; file is highly recommended, as save files are analyzed by the server while online and can\n; be used to ban. Using a separate save file with network access blocked guarantees your mod\n; save file is never sent to the server and used to ban. Disable at your own risk!\nuseAlternateSaveFile=0\n[files]\n; Loads loose param data from files instead of from encrypted data0.bdt archive. This is mod\n; specific and should only be enabled by modders who know what they are doing. End users should\n; have no reason to touch this.\nloadLooseParams=1\n; Loads extracted files from UXM instead of data from the archives. Requires a complete UXM extraction\n; and should generally only be used by mod creators.\nloadUXMFiles=0\n; If enabled, a mod will be loaded from a specified override directory.\nuseModOverrideDirectory=1\n; The directory from which to load a mod.\nmodOverrideDirectory=\"\\Cinders\"\n; Caches results of looking up override files. Can speed up loading and reduce hitching, but may require game\n; restart when a file is added or removed from a mod. Mod developers probably want this disabled, while mod\n; users (and released mods) should have this enabled.\ncacheFilePaths=1\n[debug]\n; Shows the debug console when running the game. Can be useful for modders to troubleshoot\nshowDebugLog=0\n; Gameplay asm patches that can be enabled and used by mod creators\n[gameplay]\n; Restores bonfire sacrifice menu and mechanic for mods that require it\nrestoreBonfireSacrifice=0\n[misc]\n; Chain loads another dll that hooks dinput8.dll\n; For example, if you have another dll mod that's named dinput8.dll, you can rename it to\n; othermod.dll or something, place it in the Sekiro directory, and set this path to\n; chainDInput8DLLPath=\"\\othermod.dll\" or whatever you named the dll\nchainDInput8DLLPath=\"\"");
                }
                hasCindersFile = true;
            }
            else
            {
                hasCindersFile = true;
                Debug.WriteLine("Found it!");
            }


            Debug.WriteLine("Checking for save data...");
            saveOriginalPath = Properties.Settings.Default.SaveDataPath;
            if (saveOriginalPath == "" || !Directory.Exists(saveOriginalPath))
            {
                MessageBox.Show("Please point me to the save data used by the game. (Usually this is in AppData/Roaming/DarkSoulsIII/[your user folder])", "Save Location");
                Debug.WriteLine("No path found. Opening browser...");
                var fbd = new FolderBrowserDialog();
                fbd.Description = "Please select the folder that has the save data used by the game...";
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DarkSoulsIII";
                Debug.WriteLine(fbd.SelectedPath);
                using (fbd)
                {
                    bool fileFound = false;
                    while (!fileFound)
                    {
                        DialogResult result = fbd.ShowDialog();
                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            Debug.WriteLine("OK pressed; copying path...");
                            string[] files = Directory.GetFiles(fbd.SelectedPath);
                            if (File.Exists(files[0]) && files[0].Contains(".sl2"))
                            {
                                MessageBox.Show("That looks correct. Moving on...", "Got it!");
                                fileFound = true;
                                saveOriginalPath = fbd.SelectedPath;
                                Properties.Settings.Default.SaveDataPath = saveOriginalPath;
                                changesMade = true;
                            }
                            else
                            {
                                DialogResult res = MessageBox.Show("There isn't a save data (that I can see) in that folder. Is this the location you want to use?", "Confirm Location", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (res == DialogResult.Yes)
                                {
                                    fileFound = true;
                                    saveOriginalPath = fbd.SelectedPath;
                                    Properties.Settings.Default.SaveDataPath = saveOriginalPath;
                                    changesMade = true;
                                }
                            }
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            Debug.WriteLine("Closing...");
                            Application.Exit();
                            return;
                        }
                    }
                    if (!fileFound)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            saveAppDataPath = appDataFolder + saveAppDataFolderName;

            Debug.WriteLine("Checking if save directories exist and creating them if necessary...");
            if (!Directory.Exists(saveAppDataPath))
            {
                Debug.WriteLine("Creating base directory...");
                Directory.CreateDirectory(saveAppDataPath);
            }
            if (!Directory.Exists(saveAppDataPath+"\\ashes"))
            {
                Debug.WriteLine("Creating ashes directory...");
                Directory.CreateDirectory(saveAppDataPath+"\\ashes");
            }
            if (!Directory.Exists(saveAppDataPath + "\\cinders"))
            {
                Debug.WriteLine("Creating cinders directory...");
                Directory.CreateDirectory(saveAppDataPath + "\\cinders");
            }

            if (firstRun)
            {
                MessageBox.Show("Now I need to copy the individual save files that will be switched out. Copies will be made so if you want to select the same file for each selection, that will be ok.", "Next Steps...");
            }

            if (!File.Exists(saveAppDataPath + saveDataName))
            {
                MessageBox.Show("Please select the save data for your vanilla install.", "Vanilla Save Data");
                bool foundFile = false;
                while (!foundFile)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "Dark Souls 3 save data (*.sl2) | *.sl2";
                        DialogResult result = ofd.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string fileName = ofd.SafeFileName;
                            string filePath = ofd.FileName;
                            if (fileName.Contains(".sl2") != true)
                            {
                                DialogResult dRes = MessageBox.Show("This file doesn't look like a DS3 save. Do you want to continue?", "Save Doesn't Look Right", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dRes == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Copying file...");
                                    foundFile = true;
                                    File.Copy(filePath, saveAppDataPath + saveDataName);
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Copying file...");
                                foundFile = true;
                                File.Copy(filePath, saveAppDataPath + saveDataName);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Closing...");
                            Application.Exit();
                            return;
                        }
                    }
                }
            }

            if (!File.Exists(saveAppDataPath+"\\ashes"+saveDataName))
            {
                MessageBox.Show("Please select the save data for Ashes.", "Ashes Save Data");
                bool foundFile = false;
                while (!foundFile)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "Dark Souls 3 save data (*.sl2) | *.sl2";
                        DialogResult result = ofd.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string fileName = ofd.SafeFileName;
                            string filePath = ofd.FileName;
                            if (fileName.Contains(".sl2") != true)
                            {
                                DialogResult dRes = MessageBox.Show("This file doesn't look like a DS3 save. Do you want to continue?", "Save Doesn't Look Right", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dRes == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Copying file...");
                                    foundFile = true;
                                    File.Copy(filePath, saveAppDataPath + "\\ashes" + saveDataName);
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Copying file...");
                                foundFile = true;
                                File.Copy(filePath, saveAppDataPath + "\\ashes" + saveDataName);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Closing...");
                            Application.Exit();
                            return;
                        }
                    }
                }
            }

            if (!File.Exists(saveAppDataPath + "\\cinders" + saveDataName))
            {
                MessageBox.Show("Please select the save data for Cinders.", "Cinders Save Data");
                bool foundFile = false;
                while (!foundFile)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "Dark Souls 3 save data (*.sl2) | *.sl2";
                        DialogResult result = ofd.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string fileName = ofd.SafeFileName;
                            string filePath = ofd.FileName;
                            if (fileName.Contains(".sl2") != true)
                            {
                                DialogResult dRes = MessageBox.Show("This file doesn't look like a DS3 save. Do you want to continue?", "Save Doesn't Look Right", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dRes == DialogResult.Yes)
                                {
                                    Debug.WriteLine("Copying file...");
                                    foundFile = true;
                                    File.Copy(filePath, saveAppDataPath + "\\cinders" + saveDataName);
                                }
                            }
                            else
                            {
                                Debug.WriteLine("Copying file...");
                                foundFile = true;
                                File.Copy(filePath, saveAppDataPath + "\\cinders" + saveDataName);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Closing...");
                            Application.Exit();
                            return;
                        }
                    }
                }
            }

            if (firstRun)
            {
                MessageBox.Show("Almost done! Since this is the first run, mod status will be set to none/vanilla. Copies have been made of save data provided; the save data the game is using will be overwritten to the vanilla version you previously selected. You shouldn't see this again. Enjoy!", "First Run Nearly Complete");
                if (modIsInstalled)
                {
                    Debug.WriteLine("Deleting dinput8.dll...");
                    File.Delete(dinputFullPath);
                    modIsInstalled = false;
                }
                else
                {
                    Debug.WriteLine("Mods already uninstalled, no changes made.");
                }
                state = State.None;
                ChangeStateLabel();
                CopyNewSaveBasedOnState();
                firstRun = false;
                Properties.Settings.Default.FirstRun = firstRun;
            }

            SetRadioButtonBasedOnState();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioNone.Checked)
            {
                if (modIsInstalled)
                {
                    Debug.WriteLine("Deleting dinput8.dll...");
                    File.Delete(dinputFullPath);
                    modIsInstalled = false;
                }
                else
                {
                    Debug.WriteLine("Mods already uninstalled, no changes made.");
                }
                BackupSaveBasedOnState();
                state = State.None;
            }
            else if (radioAshes.Checked)
            {
                if (!modIsInstalled)
                {
                    Debug.WriteLine("dinput8.dll not in game folder!");
                    if (!File.Exists(appDataFolder + dinputFileName))
                    {
                        MessageBox.Show("dinput8.dll not found. Please reopen to repeat setup.", "dinput8.dll Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }

                    Debug.WriteLine("Copying dinput8.dll to game folder...");
                    File.Copy(appDataFolder + dinputFileName, dinputFullPath);
                }
                Debug.WriteLine("Copying ashes file to game folder and overwriting...");
                File.Copy(ashesFilePath, darkSouls3Path + "\\modengine.ini", true);
                modIsInstalled = true;
                BackupSaveBasedOnState();
                state = State.Ashes;
            }
            else if (radioCinders.Checked)
            {
                if (!modIsInstalled)
                {
                    Debug.WriteLine("dinput8.dll not in game folder!");
                    if (!File.Exists(appDataFolder + dinputFileName))
                    {
                        MessageBox.Show("dinput8.dll not found. Please reopen to repeat setup.", "dinput8.dll Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }
                    Debug.WriteLine("Copying dinput8.dll to game folder...");
                    File.Copy(appDataFolder + dinputFileName, dinputFullPath);
                }
                Debug.WriteLine("Copying cinders file to game folder and overwriting...");
                File.Copy(cindersFilePath, darkSouls3Path + "\\modengine.ini", true);
                modIsInstalled = true;
                BackupSaveBasedOnState();
                state = State.Cinders;
            }
            ChangeStateLabel();
            CopyNewSaveBasedOnState();
            MessageBox.Show("Successfully switched to " + GetStateString() + ".", "Mod Switched");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.State = (int)state;
            Properties.Settings.Default.Save();
        }

        private void ChangeStateLabel()
        {
            switch ((int)state)
            {
                case 0:
                    stateLabel.Text = "Ashes";
                    break;
                case 1:
                    stateLabel.Text = "Cinders";
                    break;
                case 2:
                    stateLabel.Text = "Vanilla/None installed";
                    break;
                case 3:
                    stateLabel.Text = "Mod installed";
                    break;
            }

        }

        private void BackupSaveBasedOnState()
        {
            switch ((int)state)
            {
                case 0:
                    Debug.WriteLine("Backing up ashes save...");
                    File.Copy(saveOriginalPath + saveDataName, saveAppDataPath + "\\ashes" + saveDataName, true);
                    break;
                case 1:
                    Debug.WriteLine("Backing up cinders save...");
                    File.Copy(saveOriginalPath + saveDataName, saveAppDataPath + "\\cinders" + saveDataName, true);
                    break;
                case 2:
                    Debug.WriteLine("Backing up vanilla save...");
                    File.Copy(saveOriginalPath + saveDataName, saveAppDataPath + saveDataName, true);
                    break;
                case 3:
                    MessageBox.Show("Error: State 3 in Backup");
                    break;
            }
        }

        private void CopyNewSaveBasedOnState()
        {
            switch ((int)state)
            {
                case 0:
                    Debug.WriteLine("Copying ashes save to game save directory...");
                    File.Copy(saveAppDataPath + "\\ashes" + saveDataName, saveOriginalPath + saveDataName, true);
                    break;
                case 1:
                    Debug.WriteLine("Copying cinders save to game save directory...");
                    File.Copy(saveAppDataPath + "\\cinders" + saveDataName, saveOriginalPath + saveDataName, true);
                    break;
                case 2:
                    Debug.WriteLine("Copying vanilla save to game save directory...");
                    File.Copy(saveAppDataPath + saveDataName, saveOriginalPath + saveDataName, true);
                    break;
                case 3:
                    MessageBox.Show("Error: State 3 in Copy");
                    break;
            }
        }

        private string GetStateString()
        {
            switch ((int)state)
            {
                case 0:
                    return "Ashes";
                case 1:
                    return "Cinders";
                case 2:
                    return "Vanilla";
                case 3:
                    return "Error";
            }
            return "Error";
        }

        private void SetRadioButtonBasedOnState()
        {
            switch ((int)state)
            {
                case 0:
                    radioAshes.Checked = true;
                    break;
                case 1:
                    radioCinders.Checked = true;
                    break;
                case 2:
                    radioNone.Checked = true;
                    break;
                case 3:
                    MessageBox.Show("Error: State 3 in RadioButtons");
                    break;
            }
        }
    }
}
