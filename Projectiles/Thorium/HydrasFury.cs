using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Thorium
{
    public class HydrasFury : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 130;
			projectile.height = 130;
			projectile.aiStyle = 0;
			projectile.penetrate = -1;
			projectile.light = 0.2f;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 26;
			aiType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];	
			
			projectile.ai[0]++;
			
			if (player.dead)
			{
				projectile.Kill();
				return;
			}
			
			if (player.direction > 0)
			{
				projectile.rotation += 0.35f;
				projectile.spriteDirection = 1;
			}
			else
			{
				projectile.rotation -= 0.35f;
				projectile.spriteDirection = -1;
			}
			
			projectile.position.X = player.Center.X - (projectile.width / 2f);
			projectile.position.Y = player.Center.Y - (projectile.height / 2f);
			
			Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("HydrasFuryDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("HydrasFuryDamage"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			
			if (projectile.timeLeft == 13)
			{
				Projectile.NewProjectile(projectile.Center.X + 20, projectile.Center.Y, -15f, 0f, mod.ProjectileType("HydrasFuryDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
				Projectile.NewProjectile(projectile.Center.X - 20, projectile.Center.Y, 15f, 0f, mod.ProjectileType("HydrasFuryDamage2"), (int)(projectile.damage * .35), projectile.knockBack, projectile.owner, 0f, 0f);
			}
			
			if (projectile.timeLeft < 8)
			{
				projectile.alpha = 100;
			}
			if (projectile.timeLeft < 6)
			{
				projectile.alpha = 140;
			}
			if (projectile.timeLeft < 4)
			{
				projectile.alpha = 180;
			}
			if (projectile.timeLeft < 2)
			{
				projectile.alpha = 220;
			}
		}
	}

    public class HydrasFuryDamage : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 130;
            projectile.height = 130;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 8;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 200, false);
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
            {
                crit = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }
    public class HydrasFuryDamage2 : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 130;
            projectile.height = 130;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 4;
            aiType = ProjectileID.Bullet;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
            {
                crit = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - (projectile.width / 2f);
            projectile.position.Y = player.Center.Y - (projectile.height / 2f);
        }
    }
    public class HydrasFuryEffect : ModProjectile
    {
        public static Color lightColor = new Color(41, 60, 103);
        public override string Texture => "AAMod/BlankTex";

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.tileCollide = false; projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 24;
        }

        public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
            float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
            return new Vector2(newPosX, newPosY);
        }

        public Vector2 rotVec = new Vector2(0, 65);
        public float rot = 0f;

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (player.direction > 0)
            {
                rot += 0.20f;
            }
            else
            {
                rot -= 0.20f;
            }

            projectile.Center = player.Center + new Vector2(-8f, -8f) + RotateVector(default, rotVec, rot + (projectile.ai[0] * (6.28f / 2)));

            for (int m = 0; m < 3; m++)
            {
                float velX = projectile.velocity.X / 3f * m;
                float velY = projectile.velocity.Y / 3f * m;
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.AcidDust>(), 0, 0, 0);
                //int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 0, default, 1.2f);
                Main.dust[dustID].position.X = projectile.Center.X - velX;
                Main.dust[dustID].position.Y = projectile.Center.Y - velY;
                Main.dust[dustID].velocity *= 0f;
                Main.dust[dustID].alpha = 180;
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].scale = 0.8f;
            }
        }
    }
}