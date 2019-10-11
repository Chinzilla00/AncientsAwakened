using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Rajah.Supreme
{
    public class Excalihare : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Excalihare");
		}
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 40;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 5;
            projectile.tileCollide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 8f)
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
            }
            Vector2 spinningpoint = new Vector2(0f, -3f - projectile.ai[0]).RotatedByRandom(3.1415927410125732);
            float num13 = 10f + projectile.ai[0] * 4f;
            Vector2 value6 = new Vector2(1.05f, 1f);
            for (float num14 = 0f; num14 < num13; num14 += 1f)
            {
                int num15 = Dust.NewDust(projectile.Center, 0, 0, 66, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num15].position = projectile.Center;
                Main.dust[num15].velocity = spinningpoint.RotatedBy(6.28318548f * num14 / num13) * value6 * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num15].color = Main.hslToRgb(num14 / num13, 1f, 0.5f);
                Main.dust[num15].noGravity = true;
                Main.dust[num15].scale = 1f + projectile.ai[0] / 3f;
            }
            if (Main.myPlayer == projectile.owner)
            {
                int width = projectile.width;
                int height = projectile.height;
                int num16 = projectile.penetrate;
                projectile.position = projectile.Center;
                projectile.width = projectile.height = 40 + 8 * (int)projectile.ai[0];
                projectile.Center = projectile.position;
                projectile.penetrate = -1;
                projectile.Damage();
                projectile.penetrate = num16;
                projectile.position = projectile.Center;
                projectile.width = width;
                projectile.height = height;
                projectile.Center = projectile.position;
            }
            int p = Projectile.NewProjectile((int)projectile.Center.X, (int)projectile.Center.Y, 0, 0, ModContent.ProjectileType<ExcalihareBoom>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = projectile.Center;
            Main.projectile[p].netUpdate = true;
            return false;
        }

        public override void AI()
        {
            float num99 = Main.DiscoR / 255f;
            float num100 = Main.DiscoG / 255f;
            float num101 = Main.DiscoB / 255f;
            num99 = (0.5f + num99) / 2f;
            num100 = (0.5f + num100) / 2f;
            num101 = (0.5f + num101) / 2f;
            Lighting.AddLight(projectile.Center, num99, num100, num101);
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.velocity.X != 0f)
            {
                projectile.spriteDirection = projectile.direction = -Math.Sign(projectile.velocity.X);
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
                return;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, .5f, 1f, 10, false, 0f, 0f, Main.DiscoColor);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, lightColor, false);
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.InfinityOverload>(), 120);
            int p = Projectile.NewProjectile((int)projectile.Center.X, (int)projectile.Center.Y, 0, 0, ModContent.ProjectileType<ExcalihareBoom>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = projectile.Center;
            Main.projectile[p].netUpdate = true;
        }

        public override void Kill(int i)
        {
            int p = Projectile.NewProjectile((int)projectile.Center.X, (int)projectile.Center.Y, 0, 0, ModContent.ProjectileType<ExcalihareBoom>(), projectile.damage, projectile.knockBack, Main.myPlayer);
            Main.projectile[p].Center = projectile.Center;
            Main.projectile[p].netUpdate = true;
        }
    }
}