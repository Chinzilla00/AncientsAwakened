using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class FerretNote : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Note of Furet");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 150;
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
			HandleTargetingMovement(0.025f, 0.05f, projectile.velocity.Length(), 9f);
			projectile.frame = projectile.whoAmI % 4;
			projectile.rotation = projectile.velocity.X * 0.025f;

            //int num557 = 8;
            //dust!
            //int dustId = Dust.NewDust(new Vector2(projectile.position.X + (float)num557, projectile.position.Y + (float)num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            //Main.dust[dustId].noGravity = true;
            //int dustId3 = Dust.NewDust(new Vector2(projectile.position.X + (float)num557, projectile.position.Y + (float)num557), projectile.width - num557 * 2, projectile.height - num557 * 2, 6, 0f, 0f, 0);
            //Main.dust[dustId3].noGravity = true;
        }

		public float maxDistToAttack = 3000f;
		public int target = -1;
		public int targetDelay = 8;
		public void HandleTargetingMovement(float rotScalar = 0.1f, float entVelScalar = 0.25f, float newVelSpeed = 11f, float maxSpeed = 11f)
		{
			Target();
			if (target != -1)
			{
				Entity ent = Main.npc[target];
				projectile.velocity += BaseMod.BaseUtility.RotateVector(default, new Vector2(maxSpeed, 0f), BaseMod.BaseUtility.RotationTo(projectile.Center, ent.Center)) * rotScalar;
				if(Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) > maxSpeed){ projectile.velocity.Normalize(); projectile.velocity *= maxSpeed; }
				projectile.position += ent.velocity * entVelScalar;
			}	
		}

		public void Target()
		{
			targetDelay = Math.Max(0, targetDelay - 1);
			if (target != -1 && !CanTarget(Main.npc[target])) { target = -1; }
			if (target == -1 && targetDelay == 0 && projectile.timeLeft % 20 == 0)
			{
				Vector2 startPos = projectile.Center;
				int[] npcs = BaseMod.BaseAI.GetNPCs(startPos, -1, maxDistToAttack);
				if (npcs.Length > 0)
				{
					float prevDist = maxDistToAttack;
					foreach (int i in npcs)
					{
						NPC npc = Main.npc[i];
						float dist = Vector2.Distance(startPos, npc.Center);
						if (CanTarget(npc) && dist < prevDist) { target = npc.whoAmI; prevDist = dist; }
					}
				}
			}
		}	

		public bool CanTarget(NPC npc)
		{
			return npc.active && npc.life > 0 && !npc.friendly && !npc.dontTakeDamage && npc.lifeMax > 5;
		}	

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }

		public override Color? GetAlpha(Color lightColor)
		{
			int percentile = (projectile.owner + projectile.whoAmI) % 4;
			switch(percentile)
			{
				case 0: return new Color(255, 0, 0, 150);
				case 1: return new Color(107, 40, 75, 150);
				case 2: return new Color(120, 0, 0, 150);
				default: return new Color(175, 20, 20, 150);				
			}
		}

        public override void Kill(int timeLeft)
        {
            for (int m = 0; m < 10; m++)
            {
                int dustID = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, 1, mod.DustType<Dusts.AkumaDustLight>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default, 2f);
                Main.dust[dustID].noGravity = true;
                Main.dust[dustID].velocity *= 2f;
                dustID = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.AkumaDustLight>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default);
                Main.dust[dustID].velocity *= 2f;
            }
        }		
    }
}