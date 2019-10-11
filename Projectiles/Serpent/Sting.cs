using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Serpent
{
    public class Sting : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sting");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bullet);
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Dust dust1;
            Vector2 position = projectile.position;
            dust1 = Main.dust[Dust.NewDust(position, 0, 0, Terraria.ModLoader.ModContent.DustType<Dusts.SnowDust>(), 4f, 0f, 46, default, 1f)];
            dust1.noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 100);
        }
    }
}