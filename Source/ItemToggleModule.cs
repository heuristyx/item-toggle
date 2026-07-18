using Celeste.Mod.Celeste_Multiworld;
using System;
using MonoMod.RuntimeDetour;
using System.Reflection;
using Celeste.Mod.Celeste_Multiworld.UI;
using Celeste.Mod.ItemToggle.UI;
using Archipelago.MultiClient.Net;

namespace Celeste.Mod.ItemToggle;

public class ItemToggleModule : EverestModule
{
    public static ItemToggleModule Instance { get; private set; }

    public override Type SettingsType => typeof(ItemToggleModuleSettings);
    public static ItemToggleModuleSettings Settings => (ItemToggleModuleSettings)Instance._Settings;

    public override Type SessionType => typeof(ItemToggleModuleSession);
    public static ItemToggleModuleSession Session => (ItemToggleModuleSession)Instance._Session;

    public override Type SaveDataType => typeof(ItemToggleModuleSaveData);
    public static ItemToggleModuleSaveData SaveData => (ItemToggleModuleSaveData)Instance._SaveData;

    private ToggleUIManager ToggleUIManagerInst;

    private static Hook DeathLinkGetterDetour = new Hook(typeof(ArchipelagoManager).GetMethod("get_DeathLink",BindingFlags.Instance | BindingFlags.Public), DeathLinkPatch);

    private static Hook SendPacketDetour = new Hook(typeof(ArchipelagoManager).GetMethod("SendPacket",BindingFlags.Instance | BindingFlags.NonPublic), SendPacketPatch);

    public ItemToggleModule()
    {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(ItemToggleModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(ItemToggleModule), LogLevel.Info);
#endif
    }

    public override void Load()
    {
        new CollectibleDetours();
        CollectibleDetours.Instance.Load();

        var connectUIDelegate = (On.Celeste.MainMenuClimb.hook_Render) typeof(modMainMenu).GetMethod("modMainMenuClimb_Render",BindingFlags.Static | BindingFlags.NonPublic).CreateDelegate(typeof(On.Celeste.MainMenuClimb.hook_Render));
        On.Celeste.MainMenuClimb.Render -= connectUIDelegate;
        On.Celeste.MainMenuClimb.Confirm += MainMenuClimb_Confirm;

        // Prevent showing locations in the journal; not needed anyway
        Type modJournalType = typeof(Celeste_MultiworldModule).Assembly.GetType("Celeste.Mod.Celeste_Multiworld.UI.modJournal");
        var apJournalDelegate = (On.Celeste.OuiJournalProgress.hook_ctor) modJournalType.GetMethod("modOuiJournalProgress_ctor",BindingFlags.Static | BindingFlags.NonPublic).CreateDelegate(typeof(On.Celeste.OuiJournalProgress.hook_ctor));
        On.Celeste.OuiJournalProgress.ctor -= apJournalDelegate;

        // AP configuration
        ArchipelagoManager.Instance.ActiveLevels = new([
            "0a","1a","1b","1c","2a","2b","2c","3a","3b","3c","4a","4b","4c","5a","5b","5c",
            "6a","6b","6c","7a","7b","7c","8a","9a","9b","9c","10a","10b","10c"
        ]);
        ArchipelagoManager.Instance.CrouchShuffle = true;
        ArchipelagoManager.Instance.GoalLevel = "10c";
    }

    public override void Unload()
    {
        CollectibleDetours.Instance.Unload();
        On.Celeste.MainMenuClimb.Confirm -= MainMenuClimb_Confirm;
    }

    private static void MainMenuClimb_Confirm(On.Celeste.MainMenuClimb.orig_Confirm orig, MainMenuClimb self)
    { 
        if (Instance.ToggleUIManagerInst == null)
        {
            // Populate AP save data upon first time starting game
            OuiConnection.Instance.BeginGame();

            Instance.ToggleUIManagerInst = new ToggleUIManager(Celeste.Instance);
            Celeste_MultiworldModule.SaveData.GoalItem = true;
        }
        else
        {
            (self.Scene as Overworld).Goto<OuiChapterSelect>();
        }
    }

    // Disable deathlink (roundabout due to it being read-only)
    private static bool DeathLinkPatch(Func<ArchipelagoManager,bool> orig, ArchipelagoManager self) => false;

    // Prevent AP from sending any packets
    private static void SendPacketPatch(Action<ArchipelagoManager,ArchipelagoPacketBase> orig, ArchipelagoManager self, ArchipelagoPacketBase packet) {}
}