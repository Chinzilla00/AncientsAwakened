using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;


namespace AAMod.Projectiles
{
    public class ArchwitchStar : AAProjectile
	{
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.damage = 100;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.ignoreWater = true;		
        }

        float rot = 0f;
        float rotInit = -1f;

        public void SetRot()
		{
			float oldInit = rotInit;
			int[] projs = BaseAI.GetProjectiles(Main.player[projectile.owner].Center, projectile.type, projectile.owner, 300f);
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
			Player player = Main.player[projectile.owner];

            if (player.dead || player.ownedProjectileCounts[mod.ProjectileType("ArchwitchStaff")] < 1) projectile.Kill();

            if (player.ownedProjectileCounts[mod.ProjectileType("ArchwitchStaff")] < 1)
            {
				projectile.timeLeft = 2;
            }

            projectile.damage = (int)(100 * player.magicDamage);
			
            if (projectile.active) { SetRot(); }

			BaseAI.AIRotate(projectile, ref projectile.rotation, ref rot, player.Center, true, 80f, 20f, 0.07f, true);
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 0);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, dColor, true);
            return false;
        }
	}
}