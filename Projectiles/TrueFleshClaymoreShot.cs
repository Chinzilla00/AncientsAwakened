using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class TrueFleshClaymoreShot : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 1;  
            projectile.width = 32;
            projectile.height = 32;
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.timeLeft = 900;
        }
		
		public override void AI()
		{
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0.3f) / 255f, ((255 - projectile.alpha) * 0f) / 255f);
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (Main.rand.NextFloat() < 1f)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            {
                projectile.rotation += projectile.direction * 0.4f;
                projectile.spriteDirection = projectile.direction;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Ichor;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 246, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            DisplayName.SetDefault("Flesh Beam");
        }
        
	    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
           target.AddBuff(BuffID.Ichor, 300);
        }

        public override bool? CanCutTiles()
        {
            return true;
        }
    }
}
