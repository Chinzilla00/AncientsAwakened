using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DracorangP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(106);
			projectile.melee = false;
			projectile.thrown = true;
            projectile.penetrate = -1;  
            projectile.width = 22;
            projectile.height = 32;
			projectile.aiStyle = 3;
			aiType = 106;
        }

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Dracorang");
		}
		
		public override void AI()
		{
			int type = Main.rand.Next(326,328);
			int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, type, projectile.damage/3, 0, Main.myPlayer);
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].penetrate = 1;
			Main.projectile[proj].timeLeft = 15;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;
		}
    }
}
