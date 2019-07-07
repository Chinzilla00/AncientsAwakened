using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Darkpuppey
{
    public class D : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 18;
            GlowColor = Color.Goldenrod;
            AlphaInterval = 70;
            Debuff = 0;
        }
    }
}