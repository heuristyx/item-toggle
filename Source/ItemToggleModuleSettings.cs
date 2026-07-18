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

    [SettingName("Reset level changes on reload"), SettingSubText("On \"Standard\", resets all collectibles, breakable blocks and hidden passages\nupon reload. Selecting \"All\" may have unintended consequences.")]
    public FlagResetSetting ResetFlagsOnLoad { get; set; } = FlagResetSetting.Standard;

    [SettingName("Reset dash switches on reload"), SettingSubText("Whether activated dash switches (and gates) should be reset upon reload.\nNote that reloads happen every room, so gates activated in another room\nwill close if this setting is on.")]
    public bool ResetDashSwitches { get; set; } = true;
}