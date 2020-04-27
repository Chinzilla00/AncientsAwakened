using System;

using Terraria;


namespace AAMod.Projectiles.AH
{
    public class FireOrbiter : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;
		
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

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
				rot = rotInit * (projSlot + 1f);
			}
		}

        public override void AI()
		{
			projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
			
			Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.dead || !player.HasBuff(mod.BuffType("Orbiters"))) projectile.Kill();
            if (modPlayer.Orbiters)
            {
				projectile.timeLeft = 2;
				player.AddBuff(mod.BuffType("Orbiters"), 2, true);
            }
			
            if (projectile.active) { SetRot(); }
			BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 40f, 20f, 0.07f, true);
		}

		public override void Kill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(projectile.Center, projectile.type, projectile.owner, 200f);
		}
	}
}