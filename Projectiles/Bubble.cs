using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Bubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FUCKING BUBBLES");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BulletHighVelocity);
            aiType = ProjectileID.BulletHighVelocity;
        }
        public override void AI()
        {
            Dust dust1;
            Vector2 position = projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, 0, 0, mod.DustType<Dusts.MireBubbleDust>(), 4f, 0f, 46, default, 1f)];
            dust1.noGravity = true;
        }
    }
}