using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Projectiles.Yamata
{
    public class TC : ModProjectile
	{
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.PaladinsHammerFriendly);
			projectile.width = 18;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.magic = false;
			projectile.penetrate = 6;
			projectile.timeLeft = 550;
			projectile.light = 0.9f;
			projectile.extraUpdates = 2;
		}

        int ProjTimer = 0;

        public override void PostAI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                ProjTimer++;
                if (ProjTimer >= 20)
                {
                    ProjTimer = 0;
                    Projectile.NewProjectile(projectile.position, Vector2.Zero, ModContent.ProjectileType<FlairdraCyclone>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }

         public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            YWSplit();
        }

 public void YWSplit()
        {
            Player player = Main.player[projectile.owner];
            float num72 = 45f;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 *= num80;
            num79 *= num80;
            int num112 = 3;
            for (int num113 = 0; num113 < num112; num113++)
            {
                vector2 = new Vector2(player.position.X + (player.width * 0.5f) + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - -600f);
                vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-200, 201);
                vector2.Y -= 100 * num113;
                num78 = Main.mouseX + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
                num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (num79 < 0f)
                {
                    num79 *= -1f;
                }
                if (num79 < 20f)
                {
                    num79 = 20f;
                }
                num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
                num80 = num72 / num80;
                num78 *= num80;
                num79 *= num80;
                float num114 = num78;
                float num115 = num79 + (Main.rand.Next(-40, 41) * 0.05f);
                Projectile.NewProjectile(vector2.X, vector2.Y, num114 * 0.75f, num115 * -0.75f, ModContent.ProjectileType<YWSplit>(), projectile.damage, projectile.damage, player.whoAmI, 0f, -0.5f + ((float)Main.rand.NextDouble() * 0.3f));
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Crescent");
        }
    }
}
