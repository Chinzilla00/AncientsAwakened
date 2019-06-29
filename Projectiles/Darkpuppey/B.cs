using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Projectiles.Darkpuppey
{
    public class B : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.Red;
            AlphaInterval = 70;
            Debuff = BuffID.Confused;
        }
    }
}