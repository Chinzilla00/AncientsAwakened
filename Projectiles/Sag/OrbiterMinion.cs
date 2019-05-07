using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;

namespace AAMod.Projectiles.Sag
{
	public class OrbiterMinion : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.damage = 0;
            projectile.penetrate = -1;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.ignoreWater = true;		
        }

		public void SetRot()
		{
			float oldInit = rotInit;
			int[] projs = BaseAI.GetProjectiles(Main.player[projectile.owner].Center, projectile.type, projectile.owner, 200f);
			rotInit = projs.Length == 0 ? 0f : ((float)Math.PI * 2f / projs.Length);

			if (rotInit != oldInit)
			{
				int projSlot = 0;
				for(int m = 0; m < projs.Length; m++)
				{
					if (projs[m] == projectile.identity) { projSlot = m; }
				}
				rot = rotInit * ((float)projSlot + 1f);
			}
		}

        NPC nPC2 = null;


        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.dead || !player.HasBuff(mod.BuffType("SagOrbiter"))) projectile.Kill();
            if (modPlayer.SagOrbiter)
            {
				projectile.timeLeft = 2;
				player.AddBuff(mod.BuffType("SagOrbiter"), 2, true);
            }

            projectile.ai[0] = 30 * player.minionDamage;

            Vector2 vector46 = projectile.position;
            bool flag25 = false;
            float num633 = 700f;

            for (int num645 = 0; num645 < 200; num645++)
            {
                nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        vector46 = nPC2.Center;
                        flag25 = true;
                    }
                }
            }
            if  (nPC2.life <= 0)
            {
                nPC2 = null;
            }

            BaseAI.ShootPeriodic(projectile, vector46, nPC2.width, nPC2.height, mod.ProjectileType<Darkray>(), ref projectile.ai[1], 120, (int)projectile.ai[0], 7, true);
			
            if (projectile.active) { SetRot(); }
			BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 80f, 20f, 0.07f, true);
		}

		public override void Kill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(projectile.Center, projectile.type, projectile.owner, 200f);
		}
	}
}