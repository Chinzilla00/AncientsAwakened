using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;

namespace AAMod.Projectiles.AH
{
	public class FireOrbiter : AAProjectile
	{
		float rot = 0f;
		float rotInit = -1f;
		
		public override void SetStaticDefaults()
		{
            DisplayName = "Oribiters";
		}

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
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

        public override void AI()
		{
			Player owner = Main.player[projectile.owner];
			if (owner == null || !owner.active || owner.dead) { projectile.Kill(); return; }
			if (Main.myPlayer == owner.whoAmI) 
			{
				int id = owner.FindBuffIndex(mod.BuffType("Orbiters"));
				if(id == -1){ projectile.Kill(); return; }
				owner.AddBuff(mod.BuffType("Orbiters"), 100, false); 
			}
            
            if (projectile.active) { SetRot(); }
			if (projectile.timeLeft <= 50 && projectile.timeLeft >= 20) { projectile.timeLeft = 100; }
			BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, owner.Center, true, 40f, 20f, 0.07f, true);
		}

		public override void Kill(int timeLeft)
		{
			if (Main.myPlayer == projectile.owner)
			{
				int[] projs = BaseAI.GetProjectiles(projectile.Center, projectile.type, projectile.owner, 200f);
				Player p = Main.player[projectile.owner];
				if (projs.Length <= 1)
				{
					if (p.FindBuffIndex(mod.BuffType("Orbiters")) != -1) { p.ClearBuff(mod.BuffType("Orbiters")); }
					p.AddBuff(mod.BuffType("Orbiters"), 300, false);
				}
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			byte a = (byte)(255 - projectile.alpha);
			Color lightColor = new Color(a, a, a, a);			
			BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile, lightColor);
			return false;
		}
	}
}