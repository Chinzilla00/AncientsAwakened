using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ValkyrieSlash : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Valkyrie Slash");
			Main.projFrames[projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 75;
			projectile.netUpdate = true;
            aiType = 595;
        }
        public override void AI()
        {
			if (Main.myPlayer == projectile.owner)
            {
                //Do net updatey thing. Syncs this projectile.
				if (Main.rand.Next(3) == 0)
                {
                 int num30 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y, 100, default, 2f);
                    Main.dust[num30].noGravity = true;
                    Main.dust[num30].position -= projectile.velocity;
                }
                projectile.netUpdate = true;
                Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
                if (Main.player[projectile.owner].Center.Y < mouse.Y)
                {
                    float Xdis = Main.player[Main.myPlayer].Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.player[Main.myPlayer].Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    projectile.position.X = Main.player[projectile.owner].Center.X + DistXT - 30;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y + DistYT - 30;
                }
                if (Main.player[projectile.owner].Center.Y >= mouse.Y)
                {
                    float Xdis = Main.player[Main.myPlayer].Center.X - mouse.X;  // change myplayer to nearest player in full version
                    float Ydis = Main.player[Main.myPlayer].Center.Y - mouse.Y; // change myplayer to nearest player in full version
                    float Angle = (float)Math.Atan(Xdis / Ydis);
                    float DistXT = (float)(Math.Sin(Angle) * 29);
                    float DistYT = (float)(Math.Cos(Angle) * 29);
                    projectile.position.X = Main.player[projectile.owner].Center.X + (0 - DistXT) - 30;
                    projectile.position.Y = Main.player[projectile.owner].Center.Y + (0 - DistYT) - 30;
                }
            }
			
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 12;
		}

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return projHitbox.Intersects(targetHitbox);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Snow;
        }
    }
}