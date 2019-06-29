using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Projectiles.Darkpuppey
{
    public class H : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.Purple;
            AlphaInterval = 70;
            Debuff = BuffID.ShadowFlame;
        }
    }
}