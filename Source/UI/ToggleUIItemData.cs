using System;
using System.Collections.Generic;

namespace Celeste.Mod.ItemToggle.UI
{
    public static class ToggleUIItemData
    {
        public static readonly ToggleableItem Spring     = new ToggleableItem(0xCA12000, "spring");
        public static readonly ToggleableItem Traffic    = new ToggleableItem(0xCA12001, "trafficblock");
        public static readonly ToggleableItem CassetteP  = new ToggleableItem(0xCA12002, "pinkcassette");
        public static readonly ToggleableItem CassetteB  = new ToggleableItem(0xCA12003, "bluecassette");
        public static readonly ToggleableItem CassetteY  = new ToggleableItem(0xCA1201A, "yellowcassette");
        public static readonly ToggleableItem CassetteG  = new ToggleableItem(0xCA1201B, "greencassette");
        public static readonly ToggleableItem Refill     = new ToggleableItem(0xCA12018, "refill");
        public static readonly ToggleableItem DreamBlock = new ToggleableItem(0xCA12004, "dreamblock");
        public static readonly ToggleableItem Coins      = new ToggleableItem(0xCA12005, "coins");
        public static readonly ToggleableItem Seeds      = new ToggleableItem(0xCA1201F, "strawberryseed");
        public static readonly ToggleableItem SinkPlat   = new ToggleableItem(0xCA12020, "sinkingplatform");
        public static readonly ToggleableItem MovePlat   = new ToggleableItem(0xCA12006, "movingplatform");
        public static readonly ToggleableItem MoveBlock  = new ToggleableItem(0xCA12009, "moveblock");
        public static readonly ToggleableItem BlueCloud  = new ToggleableItem(0xCA12008, "blueclouds");
        public static readonly ToggleableItem PinkCloud  = new ToggleableItem(0xCA12010, "fragileclouds");
        public static readonly ToggleableItem WhiteBlock = new ToggleableItem(0xCA12021, "whiteblock");
        public static readonly ToggleableItem BlueBoost  = new ToggleableItem(0xCA12007, "boosterblue");
        public static readonly ToggleableItem RedBoost   = new ToggleableItem(0xCA1200B, "boosterred");
        public static readonly ToggleableItem DashSwitch = new ToggleableItem(0xCA1201C, "dashswitch");
        public static readonly ToggleableItem SwapBlock  = new ToggleableItem(0xCA1200A, "swapblock");
        public static readonly ToggleableItem Theo       = new ToggleableItem(0xCA1200C, "theocrystal");
        public static readonly ToggleableItem Seeker     = new ToggleableItem(0xCA1201D, "seeker");
        public static readonly ToggleableItem TorchB     = new ToggleableItem(0xCA12022, "bluetorch");
        public static readonly ToggleableItem TorchY     = new ToggleableItem(0xCA12024, "yellowtorch");
        public static readonly ToggleableItem Feather    = new ToggleableItem(0xCA1200D, "feather");
        public static readonly ToggleableItem Bumper     = new ToggleableItem(0xCA1200E, "bumper");
        public static readonly ToggleableItem Kevin      = new ToggleableItem(0xCA1200F, "kevin");
        public static readonly ToggleableItem Badeline   = new ToggleableItem(0xCA12011, "badeline");
        public static readonly ToggleableItem IceBall    = new ToggleableItem(0xCA12012, "iceball");
        public static readonly ToggleableItem CoreToggle = new ToggleableItem(0xCA12013, "coreswitch");
        public static readonly ToggleableItem CoreBlock  = new ToggleableItem(0xCA12014, "coreblock");
        public static readonly ToggleableItem DblRefill  = new ToggleableItem(0xCA12019, "doublerefill");
        public static readonly ToggleableItem Pufferfish = new ToggleableItem(0xCA12015, "puffer");
        public static readonly ToggleableItem Jellyfish  = new ToggleableItem(0xCA12016, "glider");
        public static readonly ToggleableItem Breaker    = new ToggleableItem(0xCA12017, "breaker");
        public static readonly ToggleableItem Bird       = new ToggleableItem(0xCA12023, "bird");
        public static readonly ToggleableItem DashUL     = new ToggleableItem(0xCA10088, "upleftdash");
        public static readonly ToggleableItem DashU      = new ToggleableItem(0xCA10081, "updash");
        public static readonly ToggleableItem DashUR     = new ToggleableItem(0xCA10082, "uprightdash");
        public static readonly ToggleableItem DashL      = new ToggleableItem(0xCA10087, "leftdash");
        public static readonly ToggleableItem Crouch     = new ToggleableItem(0xCA1008C, "crouch");
        public static readonly ToggleableItem DashR      = new ToggleableItem(0xCA10083, "rightdash");
        public static readonly ToggleableItem DashDL     = new ToggleableItem(0xCA10086, "downleftdash");
        public static readonly ToggleableItem DashD      = new ToggleableItem(0xCA10085, "downdash");
        public static readonly ToggleableItem DashDR     = new ToggleableItem(0xCA10084, "downrightdash");
        public static readonly ToggleableItem ClimbLeft  = new ToggleableItem(0xCA1008A, "climbleft");
        public static readonly ToggleableItem ClimbRight = new ToggleableItem(0xCA1008B, "climbright");
        
        public static readonly ToggleableItem AllOn      = new ToggleableItem(0xFF10000, "allon");
        public static readonly ToggleableItem AllOff     = new ToggleableItem(0xFF10001, "alloff");
        public static readonly ToggleableItem KeyMenu    = new ToggleableItem(0xFF10002, "key");

        public static ToggleableItem[,] ItemGrid { get; set; } = new ToggleableItem[,] {
            { Spring    , Traffic   , CassetteP , CassetteB , CassetteY , CassetteG , null      , DashUL    , DashU     , DashUR     },
            { Refill    , DreamBlock, Coins     , Seeds     , SinkPlat  , MovePlat  , null      , DashL     , Crouch    , DashR      },
            { MoveBlock , BlueCloud , PinkCloud , WhiteBlock, BlueBoost , RedBoost  , null      , DashDL    , DashD     , DashDR     },
            { DashSwitch, SwapBlock , Theo      , Seeker    , TorchB    , TorchY    , null      , ClimbLeft , ClimbRight, null       },
            { Feather   , Bumper    , Kevin     , Badeline  , IceBall   , CoreToggle, null      , null      , null      , null       },
            { CoreBlock , DblRefill , Pufferfish, Jellyfish , Breaker   , Bird      , null      , KeyMenu   , AllOn     , AllOff     }
        };
    }

    public class ToggleableItem
    {
        public long ItemID { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = false;
        public Func<bool> GetActive { get; set; }
        public Action<bool> SetActive { get; set; }

        protected ToggleableItem() {}

        public ToggleableItem(long id, string name)
        {
            ItemID = id;
            Name = name;
        }
    }
}
