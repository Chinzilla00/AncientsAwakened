using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class GRocket2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("R0CKET");
            Main.projFrames[projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            cooldownSlot = 1;
            projectile.damage = 6;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, (255 - projectile.alpha) * 0.9f / 255f, (255 - projectile.alpha) * 0f / 255f, (255 - projectile.alpha) * 0.4f / 255f);

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.localAI[1]++;
            if (projectile.localAI[1] >= 60)
            {

                if (projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref projectile.velocity);
                    projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 800f;
                bool target = false;
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    if (Main.npc[k].active)
                    {
                        Vector2 newMove = Main.npc[k].Center - projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
                if (target)
                {
                    AdjustMagnitude(ref move);
                    projectile.velocity = (10 * projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref projectile.velocity);
                }
            }
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 13f)
            {
                vector *= 12f / magnitude;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"), (int)projectile.Center.X, (int)projectile.Center.Y);
            float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y)- spread/2;
	    	double Angle = spread/4f;
	    	double offsetAngle;
	    	int i;
	    	if (projectile.owner == Main.myPlayer)
	    	{
		    	for (i = 0; i < 4; i++ )
		    	{
		   			offsetAngle = (startAngle + Angle * ( i + i * i ) / 2f ) + 32f * i;
		        	Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 2f ), (float)( Math.Cos(offsetAngle) * 6f ), mod.ProjectileType("GBlast"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
		        	Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 2f ), (float)( -Math.Cos(offsetAngle) * 6f ), mod.ProjectileType("GBlast"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
		    	}
	    	}
        }
    }
}