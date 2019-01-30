using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppy
{
    public class Nightmare3 : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Horrid Nightmare");
            Main.projFrames[projectile.type] = 3;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            AIVilethorn(projectile, 50, 4, 15);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public static void AIVilethorn(Projectile p, int alphaInterval = 50, int alphaReduction = 4, int length = 8)
        {
            if (p.ai[0] == 0f)
            {
                p.frame = 0;
                p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
                p.alpha -= alphaInterval;
                if (p.alpha <= 0)
                {
                    p.frame = 1;
                    p.alpha = 0;
                    p.ai[0] = 1f;
                    if (p.ai[1] == 0f) { p.ai[1] += 1f; p.position += p.velocity; }
                    if (p.ai[1] < length && Main.myPlayer == p.owner)
                    {
                        Vector2 rotVec = p.velocity;
                        int id = Projectile.NewProjectile(p.Center.X + p.velocity.X, p.Center.Y + p.velocity.Y, rotVec.X, rotVec.Y, p.type, p.damage, p.knockBack, p.owner);
                        Main.projectile[id].damage = p.damage;
                        Main.projectile[id].ai[1] = p.ai[1] + 1f;
                        NetMessage.SendData(27, -1, -1, NetworkText.FromLiteral(""), id, 0f, 0f, 0f, 0);
                        p.position -= p.velocity;
                        return;
                    }
                }
            }
            else
            {
                p.frame = 2;
                p.alpha += alphaReduction;
                if (p.alpha >= 255) { p.Kill(); return; }
            }
            p.position -= p.velocity;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 300);
        }
    }
}