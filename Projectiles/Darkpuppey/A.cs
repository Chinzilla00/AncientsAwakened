using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppey
{
    public class A : ModProjectile
    {
        public int Length = 15;
        public Color GlowColor = Color.Orange;
        public int AlphaInterval = 70;
        public int Debuff = BuffID.Daybreak;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Decieving Truth");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.timeLeft = 320;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.magic = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(Debuff, 300);
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIVilethorn(projectile, AlphaInterval, 4, Length);
            bool spineBeginning = projectile.ai[1] == 0;
            bool spineEnd = projectile.ai[1] == 15;
            if (spineBeginning == true)
            {
                projectile.frame = 2;
            }
            else if (spineEnd == true)
            {
                projectile.frame = 0;
            }
            else
            {
                projectile.frame = 1;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Color newLightColor = new Color(Math.Max(0, GlowColor.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, GlowColor.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, GlowColor.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);

            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 3, frame, projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}