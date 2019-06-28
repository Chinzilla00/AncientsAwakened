using Microsoft.Xna.Framework;

namespace AAMod.Projectiles.Darkpuppey
{
    public class I : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.LimeGreen;
            AlphaInterval = 70;
            Debuff = mod.BuffType<Buffs.Terrablaze>();
        }
    }
}