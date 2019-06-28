using Microsoft.Xna.Framework;
using Terraria;

namespace AAMod.Projectiles.Darkpuppey
{
    public class G : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 13;
            GlowColor = Color.DarkRed;
            AlphaInterval = 50;
            Debuff = 0;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.position, Vector2.Zero, mod.ProjectileType<AH.MagicBoom>(), projectile.damage, projectile.knockBack, Main.myPlayer);
        }
    }
}