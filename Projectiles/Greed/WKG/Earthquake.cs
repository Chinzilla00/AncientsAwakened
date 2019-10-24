using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed.WKG
{
    public class Earthquake : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            projectile.width = 200;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 5;
        }

        public override void AI()
        {
            Vector2 bottom = projectile.Bottom;
            for (float num3 = 0f; num3 < 20; num3++)
            {
                Dust dust3 = Dust.NewDustDirect(projectile.Bottom, projectile.width, 1, DustID.Stone, 0f, 0f, 0, default, 1f);
                dust3.alpha = 0;
                dust3.position.Y = bottom.Y;
                Dust expr_336_cp_0 = dust3;
                expr_336_cp_0.velocity.Y -= 3f;
                Dust expr_34E_cp_0 = dust3;
                expr_34E_cp_0.velocity.X *= 0.5f;
                dust3.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
            }
            for (float num4 = 0f; num4 < 20; num4++)
            {
                Dust dust4 = Dust.NewDustDirect(projectile.Bottom, projectile.width, 1, DustID.Dirt, 0f, 0f, 0, default, 1f);
                dust4.position.Y = bottom.Y;
                Dust expr_433_cp_0 = dust4;
                expr_433_cp_0.velocity.Y -= 5f;
                Dust expr_44B_cp_0 = dust4;
                expr_44B_cp_0.velocity.X *= 0.8f;
                dust4.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.velocity.Y = knockback * target.knockBackResist;
        }
    }
}