using System;
using System.Collections.Generic;

namespace Celeste.Mod.ItemToggle.UI
{
    public static class ToggleUIItemData
    {
        public static readonly ToggleableItem Spring     = new ToggleableItem(0xCA12000, "spring", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Traffic    = new ToggleableItem(0xCA12001, "trafficblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CassetteP  = new ToggleableItem(0xCA12002, "pinkcassette", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CassetteB  = new ToggleableItem(0xCA12003, "bluecassette", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CassetteY  = new ToggleableItem(0xCA1201A, "yellowcassette", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CassetteG  = new ToggleableItem(0xCA1201B, "greencassette", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Refill     = new ToggleableItem(0xCA12018, "refill", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem DreamBlock = new ToggleableItem(0xCA12004, "dreamblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Coins      = new ToggleableItem(0xCA12005, "coins", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Seeds      = new ToggleableItem(0xCA1201F, "strawberryseed", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem SinkPlat   = new ToggleableItem(0xCA12020, "sinkingplatform", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem MovePlat   = new ToggleableItem(0xCA12006, "movingplatform", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem MoveBlock  = new ToggleableItem(0xCA12009, "moveblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem BlueCloud  = new ToggleableItem(0xCA12008, "blueclouds", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem PinkCloud  = new ToggleableItem(0xCA12010, "fragileclouds", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem WhiteBlock = new ToggleableItem(0xCA12021, "whiteblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem BlueBoost  = new ToggleableItem(0xCA12007, "boosterblue", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem RedBoost   = new ToggleableItem(0xCA1200B, "boosterred", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem DashSwitch = new ToggleableItem(0xCA1201C, "dashswitch", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem SwapBlock  = new ToggleableItem(0xCA1200A, "swapblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Theo       = new ToggleableItem(0xCA1200C, "theocrystal", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Seeker     = new ToggleableItem(0xCA1201D, "seeker", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem TorchB     = new ToggleableItem(0xCA12022, "bluetorch", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem TorchY     = new ToggleableItem(0xCA12024, "yellowtorch", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Feather    = new ToggleableItem(0xCA1200D, "feather", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Bumper     = new ToggleableItem(0xCA1200E, "bumper", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Kevin      = new ToggleableItem(0xCA1200F, "kevin", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Badeline   = new ToggleableItem(0xCA12011, "badeline", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem IceBall    = new ToggleableItem(0xCA12012, "iceball", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CoreToggle = new ToggleableItem(0xCA12013, "coreswitch", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem CoreBlock  = new ToggleableItem(0xCA12014, "coreblock", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem DblRefill  = new ToggleableItem(0xCA12019, "doublerefill", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Pufferfish = new ToggleableItem(0xCA12015, "puffer", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Jellyfish  = new ToggleableItem(0xCA12016, "glider", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Breaker    = new ToggleableItem(0xCA12017, "breaker", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem Bird       = new ToggleableItem(0xCA12023, "bird", Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Interactables);
        public static readonly ToggleableItem DashUL     = new ToggleableItem(0xCA10088, "upleftdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpLeftDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpLeftDash = newValue; });
        public static readonly ToggleableItem DashU      = new ToggleableItem(0xCA10081, "updash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpDash = newValue; });
        public static readonly ToggleableItem DashUR     = new ToggleableItem(0xCA10082, "uprightdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpRightDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.UpRightDash = newValue; });
        public static readonly ToggleableItem DashL      = new ToggleableItem(0xCA10087, "leftdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.LeftDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.LeftDash = newValue; });
        public static readonly ToggleableItem Crouch     = new ToggleableItem(0xCA1008C, "crouch", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Crouch, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.Crouch = newValue; });
        public static readonly ToggleableItem DashR      = new ToggleableItem(0xCA10083, "rightdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.RightDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.RightDash = newValue; });
        public static readonly ToggleableItem DashDL     = new ToggleableItem(0xCA10086, "downleftdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownLeftDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownLeftDash = newValue; });
        public static readonly ToggleableItem DashD      = new ToggleableItem(0xCA10085, "downdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownDash = newValue; });
        public static readonly ToggleableItem DashDR     = new ToggleableItem(0xCA10084, "downrightdash", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownRightDash, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.DownRightDash = newValue; });
        public static readonly ToggleableItem ClimbLeft  = new ToggleableItem(0xCA1008A, "climbleft", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.LeftClimb, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.LeftClimb = newValue; });
        public static readonly ToggleableItem ClimbRight = new ToggleableItem(0xCA1008B, "climbright", () => Celeste_Multiworld.Celeste_MultiworldModule.SaveData.RightClimb, (newValue) => { Celeste_Multiworld.Celeste_MultiworldModule.SaveData.RightClimb = newValue; });
        
        public static readonly ToggleableItem AllOn      = new ToggleableItem(       -1, "allon", () => false, (_) => ToggleAll(true));
        public static readonly ToggleableItem AllOff     = new ToggleableItem(       -1, "alloff", () => false, (_) => ToggleAll(false));
        public static readonly ToggleableItem KeyMenu    = new ToggleableItem(       -1, "key", () => false, null ); // Setter defined in ToggleUIManager constructor

        public static List<ToggleableItem> KeyList { get; set; } = new List<ToggleableItem>();

        public static ToggleableItem[,] ItemGrid { get; set; } = new ToggleableItem[,] {
            { Spring    , Traffic   , CassetteP , CassetteB , CassetteY , CassetteG , null      , DashUL    , DashU     , DashUR     },
            { Refill    , DreamBlock, Coins     , Seeds     , SinkPlat  , MovePlat  , null      , DashL     , Crouch    , DashR      },
            { MoveBlock , BlueCloud , PinkCloud , WhiteBlock, BlueBoost , RedBoost  , null      , DashDL    , DashD     , DashDR     },
            { DashSwitch, SwapBlock , Theo      , Seeker    , TorchB    , TorchY    , null      , ClimbLeft , ClimbRight, null       },
            { Feather   , Bumper    , Kevin     , Badeline  , IceBall   , CoreToggle, null      , null      , null      , null       },
            { CoreBlock , DblRefill , Pufferfish, Jellyfish , Breaker   , Bird      , null      , KeyMenu   , AllOn     , AllOff     }
        };

        private static void ToggleAll(bool newValue)
        {
            foreach (ToggleableItem item in ItemGrid)
            {
                if (item?.ItemID == -1) continue;
                item?.SetActive(newValue);
            }
        }
    }

    public class ToggleableItem
    {
        public long ItemID { get; set; }
        public string Name { get; set; }
        public Func<bool> GetActive { get; set; }
        public Action<bool> SetActive { get; set; }

        protected ToggleableItem() {}

        public ToggleableItem(long id, string name, Func<bool> getActive, Action<bool> setActive)
        {
            ItemID = id;
            Name = name;
            GetActive = getActive;
            SetActive = setActive;
        }

        // Simplified constructor since we can pass dictionaries by reference
        public ToggleableItem(long id, string name, Dictionary<long,bool> dataDict)
        {
            ItemID = id;
            Name = name;
            GetActive = () => dataDict.TryGetValue(ItemID, out bool isActive) ? isActive : false;
            SetActive = (newValue) => { dataDict[ItemID] = newValue; };
        }
    }
}
