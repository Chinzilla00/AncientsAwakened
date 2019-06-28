using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Darkpuppey
{
    public class F : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.White;
            AlphaInterval = 70;
            Debuff = 0;
        }
    }
}