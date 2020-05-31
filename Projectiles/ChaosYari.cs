﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosYari : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Yari");
		}
    	
        public override void SetDefaults()
        {
			projectile.width = 40;  //The width of the .png file in pixels divided by 2.
			projectile.aiStyle = 19;
			projectile.melee = true;  //Dictates whether this is a melee-class weapon.
			projectile.timeLeft = 90;
			projectile.height = 40;  //The height of the .png file in pixels divided by 2.
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.ownerHitCheck = true;
			projectile.hide = true;
        }

        public override void AI()
        {
        	Main.player[projectile.owner].direction = projectile.direction;
        	Main.player[projectile.owner].heldProj = projectile.whoAmI;
        	Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
        	projectile.position.X = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - projectile.width / 2;
        	projectile.position.Y = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - projectile.height / 2;
        	projectile.position += projectile.velocity * projectile.ai[0];
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, Main.rand.Next(2) == 0 ? ModContent.DustType<Dusts.AkumaDust>() : ModContent.DustType<Dusts.YamataAuraDust>(), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
        	if(projectile.ai[0] == 0f)
        	{
        		projectile.ai[0] = 3f;
        		projectile.netUpdate = true;
        	}
        	if(Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
        	{
        		projectile.ai[0] -= 2.4f;
				if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner && !Globals.AAGlobalProjectile.AnyProjectiles(mod.ProjectileType("ChaosYariShot")))
				{
					projectile.localAI[0] = 1f;
					Projectile.NewProjectile(Main.player[projectile.owner].position.X, Main.player[projectile.owner].position.Y, projectile.velocity.X * 1.4f, projectile.velocity.Y * 1.4f, mod.ProjectileType("ChaosYariShot"), (int)((double)projectile.damage * 0.85f), projectile.knockBack * 0.85f, projectile.owner, 0f, 0f);
				}
        	}
        	else
        	{
        		projectile.ai[0] += 0.95f;
        	}
        	
        	if(Main.player[projectile.owner].itemAnimation == 0)
        	{
        		projectile.Kill();
        	}
        	
        	projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;
        	if(projectile.spriteDirection == -1)
        	{
        		projectile.rotation -= 1.57f;
        	}
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.immune[projectile.owner] = 5;
        }
    }
}