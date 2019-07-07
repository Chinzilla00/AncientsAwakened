using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
	public class DoomRay : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Ray");
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.magic = true;
			projectile.ignoreWater = true;
		}

		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0] += 1f;
            int num16 = 0;
            if (projectile.ai[0] >= 60f)
            {
                num16++;
            }
            if (projectile.ai[0] >= 180f)
            {
                num16++;
            }
            bool flag4 = false;
            if (projectile.ai[0] == 60f || projectile.ai[0] == 180f || (projectile.ai[0] > 180f && projectile.ai[0] % 20f == 0f))
            {
                flag4 = true;
            }
            bool flag5 = projectile.ai[0] >= 180f;
            int num17 = 10;
            if (!flag5)
            {
                projectile.ai[1] += 1f;
            }
            bool flag6 = false;
            if (flag5 && projectile.ai[0] % 20f == 0f)
            {
                flag6 = true;
            }
            if (projectile.ai[1] >= num17 && !flag5)
            {
                projectile.ai[1] = 0f;
                flag6 = true;
                if (!flag5)
                {
                    float scaleFactor3 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value9 = vector;
                    Vector2 value10 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - value9;
                    if (player.gravDir == -1f)
                    {
                        value10.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - value9.Y;
                    }
                    Vector2 vector6 = Vector2.Normalize(value10);
                    if (float.IsNaN(vector6.X) || float.IsNaN(vector6.Y))
                    {
                        vector6 = -Vector2.UnitY;
                    }
                    vector6 *= scaleFactor3;
                    if (vector6.X != projectile.velocity.X || vector6.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector6;
                }
            }
            if (projectile.soundDelay <= 0 && !flag5)
            {
                projectile.soundDelay = num17 - num16;
                projectile.soundDelay *= 2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item15, projectile.position);
                }
            }
            if (projectile.ai[0] > 10f && !flag5)
            {
                Vector2 vector7 = Vector2.UnitX * 18f;
                vector7 = vector7.RotatedBy(projectile.rotation - 1.57079637f);
                Vector2 value11 = projectile.Center + vector7;
                for (int k = 0; k < num16 + 1; k++)
                {
                    int num18 = 226;
                    float num19 = 0.4f;
                    if (k % 2 == 1)
                    {
                        num18 = 226;
                        num19 = 0.65f;
                    }
                    Vector2 vector8 = value11 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - num16 * 2);
                    int num20 = Dust.NewDust(vector8 - Vector2.One * 8f, 16, 16, num18, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0);
                    Main.dust[num20].velocity = Vector2.Normalize(value11 - vector8) * 1.5f * (10f - num16 * 2f) / 10f;
                    Main.dust[num20].noGravity = true;
                    Main.dust[num20].scale = num19;
                    Main.dust[num20].customData = player;
                }
            }
            if (flag6 && Main.myPlayer == projectile.owner)
            {
                bool flag7 = !flag4 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
                bool flag8 = player.channel && flag7 && !player.noItems && !player.CCed;
                if (flag8)
                {
                    if (projectile.ai[0] == 180f)
                    {
                        Vector2 center = projectile.Center;
                        Vector2 vector9 = Vector2.Normalize(projectile.velocity);
                        if (float.IsNaN(vector9.X) || float.IsNaN(vector9.Y))
                        {
                            vector9 = -Vector2.UnitY;
                        }
                        int num21 = (int)(projectile.damage * 3f);
                        int num22 = Projectile.NewProjectile(center.X, center.Y, vector9.X, vector9.Y, mod.ProjectileType<DoomRay1>(), num21, projectile.knockBack, projectile.owner, 0f, projectile.whoAmI);
                        projectile.ai[1] = num22;
                        projectile.netUpdate = true;
                    }
                    else if (flag5)
                    {
                        Projectile Laser = Main.projectile[(int)projectile.ai[1]];
                        if (!Laser.active || Laser.type != mod.ProjectileType<DoomRay1>())
                        {
                            projectile.Kill();
                            return;
                        }
                    }
                }
                else
                {
                    if (!flag5)
                    {
                        int num23 = mod.ProjectileType<RealityLaser>();
                        float scaleFactor4 = 10f;
                        Vector2 center2 = projectile.Center;
                        Vector2 vector10 = Vector2.Normalize(projectile.velocity) * scaleFactor4;
                        if (float.IsNaN(vector10.X) || float.IsNaN(vector10.Y))
                        {
                            vector10 = -Vector2.UnitY;
                        }
                        float num24 = 0.7f + num16 * 0.3f;
                        int num25 = (num24 < 1f) ? projectile.damage : ((int)(projectile.damage * 2f));
                        Projectile.NewProjectile(center2.X, center2.Y, vector10.X, vector10.Y, num23, num25, projectile.knockBack, projectile.owner, 0f, num24);
                    }
                    projectile.Kill();
                }
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector24.X = player.bodyFrame.Width - vector24.X;
            }
            if (player.gravDir != 1f)
            {
                vector24.Y = player.bodyFrame.Height - vector24.Y;
            }
            vector24 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - projectile.velocity;
        }
    }
}
