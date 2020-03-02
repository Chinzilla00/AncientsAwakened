using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Projectiles.Yamata
{
    public class AbyssalYariP2 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 75;
            projectile.height = 75;
            projectile.scale = 1f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.timeLeft = 90;
            projectile.extraUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 8;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Yari");
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, Color.DarkRed.R / 100, Color.DarkRed.G / 100, Color.DarkRed.B / 180);
            for (int m = projectile.oldPos.Length - 1; m > 0; m--)
            {
                projectile.oldPos[m] = projectile.oldPos[m - 1];
            }
            projectile.oldPos[0] = projectile.position;
            
			if(projectile.timeLeft < 60)
			{
				projectile.velocity.Y += projectile.velocity.Y > 0f ? 0.04f : -0.04f;
				if(projectile.velocity.Y <= -8f) projectile.velocity.Y = -8f;
				if(projectile.velocity.Y >= 8f) projectile.velocity.Y = 8f;
			}
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + .25f * (float)Math.PI;
			for (int i = 0; i < 3; i++)
			{
				int d = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("YamataADust"), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
				if (Main.rand.Next(6) != 0)
				{
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity.X *= 2f;
					Main.dust[d].velocity.Y *= 2f;
				}else
				{
					Main.dust[d].noGravity = true;
					Main.dust[d].velocity.X *= 1.2f;
					Main.dust[d].velocity.Y *= 1.2f;
				}
			}
        }

        public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(mod.BuffType("Moonraze"), 500);
        }		

        public override void Kill(int timeLeft)
        {
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 80;
            projectile.Center = projectile.position;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            Vector2 position = projectile.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 15; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 200, default, 3.7f);
                Main.dust[num88].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += projectile.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 100, default, 1.5f);
                Main.dust[num88].position = projectile.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Crimson * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += projectile.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 10; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 2.7f);
                Main.dust[num90].position = projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += projectile.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 30; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, ModContent.DustType<Dusts.YamataADust>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num92].position = projectile.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += projectile.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, new Color(Color.White.R, Color.White.G, Color.White.B, 20 * k), projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
