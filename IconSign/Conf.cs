using BepInEx.Configuration;

namespace IconSign
{
    public abstract class Conf
    {
        internal static ConfigEntry<bool> xHotbarPaint;

        internal static bool HotbarPaintEnabled() => xHotbarPaint.Value;
    }
}