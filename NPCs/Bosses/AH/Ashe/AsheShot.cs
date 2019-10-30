using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    public class AsheShot : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dayfire");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if(projectile.ai[0] != 0 || projectile.ai[1] != 0)
            {
                projectile.scale = 0.8f;
                projectile.rotation = new Vector2(projectile.ai[0], projectile.ai[1]).ToRotation() + 1.57079637f;
                projectile.position += 0.05f * Vector2.Normalize(new Vector2(projectile.ai[0], projectile.ai[1]));
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Color.White.R, Color.White.G, Color.White.B, projectile.alpha);
        }

        public override void Kill(int timeLeft)
        {
            if(projectile.ai[0] == 0 && projectile.ai[1] == 0) Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - (spread / 2);
            double deltaAngle = spread / 8f;
            Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<AsheBoom>(), projectile.damage, 2);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 200);
        }
    }
}