using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions
{
    public class ChaosConstructShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Fireball");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.minion = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 120;
            projectile.friendly = true;
        }

        public override void AI()
        {
            int d = projectile.ai[1] == 0 ? ModContent.DustType<Dusts.DragonflameDust>() : ModContent.DustType<Dusts.HydratoxinDust>();
            for (int num572 = 0; num572 < 5; num572++)
            {
                float num573 = projectile.velocity.X * 0.2f * num572;
                float num574 = -(projectile.velocity.Y * 0.2f) * num572;
                int num575 = Dust.NewDust(Vector2.Zero, projectile.width, projectile.height, d, 0f, 0f, 100, default, 1f);
                Main.dust[num575].noGravity = true;
                Main.dust[num575].velocity *= 0f;
                Dust expr_178B4_cp_0 = Main.dust[num575];
                expr_178B4_cp_0.position.X -= num573;
                Dust expr_178D3_cp_0 = Main.dust[num575];
                expr_178D3_cp_0.position.Y -= num574;
            }

            if (projectile.frameCounter++ > 5)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            int d = projectile.ai[1] == 0 ? ModContent.DustType<Dusts.DragonflameDust>() : ModContent.DustType<Dusts.HydratoxinDust>();
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(projectile.Center, projectile.width, 1, d, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2.5f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int shader = projectile.ai[1] == 0 ? Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingFlameDye) : Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingOceanDye);

            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 4, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], shader, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, Color.White, true);
            return false;
        }
    }
}
