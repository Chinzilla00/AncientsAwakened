using Terraria;
using Terraria.ModLoader;

namespace AAMod.Misc
{
    public class ShakePlayer : ModPlayer
    {
        public float shakePower;

        public override void ResetEffects()
        {
            shakePower = 0;
        }

        public override void ModifyScreenPosition()
        {
            Main.screenPosition += Utils.RandomVector2(Main.rand, -shakePower, shakePower);
        }
    }
}
