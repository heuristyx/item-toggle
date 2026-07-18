using Microsoft.Xna.Framework.Input;

namespace Celeste.Mod.ItemToggle;

public class ItemToggleModuleSettings : EverestModuleSettings {
    [DefaultButtonBinding(button: Buttons.LeftStick, key: Keys.Z)]
    public ButtonBinding OpenItemToggleMenu { get; set; }

    [DefaultButtonBinding(button: Buttons.LeftTrigger, key: Keys.LeftAlt)]
    public ButtonBinding LockItem { get; set; }

    public bool CollectiblesPersist { get; set; } = false;
}