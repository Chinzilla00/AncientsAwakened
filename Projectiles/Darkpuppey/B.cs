using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppey
{
    public class B : ModProjectile
    {
        public bool spineEnd = false;
        public bool spineBeginning = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Decieving Truth");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
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
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Confused, 300);
            }
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIVilethorn(projectile, 70, 4, 15);
            spineBeginning = projectile.ai[1] == 0;
            spineEnd = projectile.ai[1] == 15;
            if (projectile.ai[1] == 0)
            {
                projectile.frame = 2;
            }
            else if (projectile.ai[1] == 15)
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
            Color newLightColor = new Color(Math.Max(0, Color.DarkRed.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.DarkRed.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.DarkRed.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 3, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4, frame, projectile.GetAlpha(Color.White), true);
            return false;
        }
    }
}