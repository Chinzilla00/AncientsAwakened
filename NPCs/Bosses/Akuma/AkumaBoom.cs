using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma
{
    public class AkumaBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dayfire");     //The English name of the projectile
            Main.projFrames[projectile.type] = 7;     //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();

                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y *= 0.00f;

        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DragonFire>(), 200);
        }

        public override void Kill(int timeLeft)
        {
            projectile.timeLeft = 0;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            int shader = Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingFlameDye);
            Microsoft.Xna.Framework.Vector2 Drawpos = projectile.Center - Main.screenPosition + new Microsoft.Xna.Framework.Vector2(0, projectile.gfxOffY);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], shader, projectile, Microsoft.Xna.Framework.Color.White, true);
            return false;
        }
    }
}
