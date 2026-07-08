using System;

namespace Celeste.Mod.ItemToggle
{
    public class CollectibleDetours
    {
        public static CollectibleDetours Instance;

        public CollectibleDetours()
        {
            Instance = this;
        }

        public void Load()
        {
            On.Celeste.Level.LoadLevel += Level_LoadLevel;
            On.Celeste.SaveData.AddStrawberry_EntityID_bool += SaveData_AddStrawberry;
            On.Celeste.SaveData.CheckStrawberry_EntityID += SaveData_CheckStrawberry;
            On.Celeste.HeartGem.RegisterAsCollected += HeartGem_RegisterAsCollected;
            On.Celeste.SaveData.RegisterCassette += SaveData_RegisterCassette;
            On.Celeste.SaveData.RegisterSummitGem += SaveData_RegisterSummitGem;
        }

        public void Unload()
        {
            On.Celeste.Level.LoadLevel -= Level_LoadLevel;
            On.Celeste.SaveData.AddStrawberry_EntityID_bool -= SaveData_AddStrawberry;
            On.Celeste.SaveData.CheckStrawberry_EntityID -= SaveData_CheckStrawberry;
            On.Celeste.HeartGem.RegisterAsCollected -= HeartGem_RegisterAsCollected;
            On.Celeste.SaveData.RegisterCassette -= SaveData_RegisterCassette;
            On.Celeste.SaveData.RegisterSummitGem -= SaveData_RegisterSummitGem;
        }

        // Prevent collectibles from being written to save data
        private static void SaveData_AddStrawberry(On.Celeste.SaveData.orig_AddStrawberry_EntityID_bool orig, SaveData self, EntityID strawberry, bool golden)
        {
            if (ItemToggleModule.Settings.CollectiblesPersist) orig(self,strawberry,golden);
        }
        private static bool SaveData_CheckStrawberry(On.Celeste.SaveData.orig_CheckStrawberry_EntityID orig, SaveData self, EntityID strawberry)
        {
            if (ItemToggleModule.Settings.CollectiblesPersist) return orig(self,strawberry);
            else return false;
        }
        private static void HeartGem_RegisterAsCollected(On.Celeste.HeartGem.orig_RegisterAsCollected orig, HeartGem self, Level level, string poemID)
        {
            if (ItemToggleModule.Settings.CollectiblesPersist) orig(self,level,poemID);
        }
        private static void SaveData_RegisterCassette(On.Celeste.SaveData.orig_RegisterCassette orig, SaveData self, AreaKey area)
        {
            if (ItemToggleModule.Settings.CollectiblesPersist) orig(self,area);
        }
        private static void SaveData_RegisterSummitGem(On.Celeste.SaveData.orig_RegisterSummitGem orig, SaveData self, int id)
        {
            if (ItemToggleModule.Settings.CollectiblesPersist) orig(self,id);
        }

        private static void Level_LoadLevel(On.Celeste.Level.orig_LoadLevel orig, Level self, Player.IntroTypes playerIntro, bool isFromLoader)
        {
            if (!ItemToggleModule.Settings.CollectiblesPersist) {
                // Clear session collectible data & reset level state
                self.Session.DoNotLoad.Clear();
                self.Session.Keys.Clear();
                Array.Fill(self.Session.SummitGems,false);
                self.Session.HeartGem = false;
                self.Session.Cassette = false;
                foreach (var item in self.Session.Strawberries) self.Session.SetFlag($"collected_seeds_of_{item}",false);
                self.Session.Strawberries.Clear();
            }
            Celeste_Multiworld.Celeste_MultiworldModule.SaveData.KeyLocations.Clear(); // Keys are stored in AP save data, so clear this too
            orig(self,playerIntro,isFromLoader);
        }
    }
}
