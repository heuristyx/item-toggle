using Microsoft.Xna.Framework.Input;

namespace Celeste.Mod.ItemToggle;

public class ItemToggleModuleSettings : EverestModuleSettings {
    [DefaultButtonBinding(button: Buttons.LeftStick, key: Keys.Z)]
    public ButtonBinding OpenItemToggleMenu { get; set; }

    [DefaultButtonBinding(button: Buttons.LeftTrigger, key: Keys.LeftAlt)]
    public ButtonBinding LockItem { get; set; }

    public enum FlagResetSetting
    {
        None,
        Standard,
        All
    }

    [SettingName("Reset level changes on reload"), SettingSubText("On \"Standard\", resets all collectibles, hidden/breakable blocks and toggled dash\nswitches upon reload. Selecting \"All\" may have unintended consequences.")]
    public FlagResetSetting ResetFlagsOnLoad { get; set; } = FlagResetSetting.Standard;
}