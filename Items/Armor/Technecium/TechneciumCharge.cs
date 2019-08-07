using System;
using Terraria;
using BaseMod;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Armor.Technecium
{
    public class TechneciumCharge : AAProjectile
	{
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

        float rot = 0f;
        float rotInit = -1f;

        public override void AI()
		{
			projectile.frameCounter++;
            if (projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                projectile.frame += 1;
            }
            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }
            Player player = Main.player[projectile.owner];

            if (projectile.ai[0] == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                if (player.dead || !player.HasBuff(mod.BuffType("Charge1"))) projectile.Kill();
                if (modPlayer.Orbiters)
                {
                    projectile.timeLeft = 2;
                    player.AddBuff(mod.BuffType("Charge1"), 2, true);
                }

                if (projectile.active) { SetRot(); }
                BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 40f, 20f, 0.07f, true);
            }
            else if (projectile.ai[0] == 1)
            {
                float num633 = 700f;
                Vector2 vector46 = projectile.position;
                bool flag25 = false;
                for (int num645 = 0; num645 < 200; num645++)
                {
                    NPC nPC2 = Main.npc[num645];
                    if (nPC2.CanBeChasedBy(projectile, false))
                    {
                        float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                        if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25))
                        {
                            num633 = num646;
                            vector46 = nPC2.Center;
                            flag25 = true;
                        }
                    }
                }
                Vector2 value19 = vector46 - projectile.Center;
                value19.Normalize();
                value19 *= 14;
                if (projectile.ai[1] == 0)
                {
                    projectile.velocity = value19;
                    projectile.ai[1] = 1;
                }
            }
        }

        public void SetRot()
        {
            float oldInit = rotInit;
            int[] projs = BaseAI.GetProjectiles(Main.player[projectile.owner].Center, projectile.type, projectile.owner, 200f);
            rotInit = projs.Length == 0 ? 0f : ((float)Math.PI * 2f / projs.Length);

            if (rotInit != oldInit)
            {
                int projSlot = 0;
                for (int m = 0; m < projs.Length; m++)
                {
                    if (projs[m] == projectile.identity) { projSlot = m; }
                }
                rot = rotInit * (projSlot + 1f);
            }
        }

        public override void Kill(int timeLeft)
		{
			int[] projs = BaseAI.GetProjectiles(projectile.Center, projectile.type, projectile.owner, 200f);
		}
	}
}