using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Darkpuppey
{
    public class F : ModProjectile
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
            projectile.width = 34;
            projectile.height = 32;
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
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIVilethorn(projectile, 70, 4, 10);
            spineBeginning = projectile.ai[1] == 0;
            spineEnd = projectile.ai[1] == 10;
            if (projectile.ai[1] == 0)
            {
                projectile.frame = 2;
            }
            else if (projectile.ai[1] == 10)
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
            Color newLightColor = new Color(Math.Max(0, Color.White.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.White.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, Color.White.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);
            Texture2D mainTex = Main.projectileTexture[projectile.type];
            BaseMod.BaseDrawing.DrawTexture(sb, mainTex, 0, projectile);
            return false;
        }
    }
}