using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class ChaosYariEXShot : ModProjectile
    {
        public bool spineEnd = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Perfect Chaos Yari");
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
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
            projectile.melee = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIVilethorn(projectile, 70, 4, 30);
            spineEnd = projectile.ai[1] == 25;
        }

        public override void PostAI()
        {
            if (Main.netMode != 2 && projectile.alpha < 170 && projectile.alpha + 5 >= 170)
            {
                for (int j = 0; j < 4; j++)
                {
                    int DustType = mod.DustType<Dusts.Discord>();
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, DustType, projectile.velocity.X * 0.025f, projectile.velocity.Y * 0.025f, 40, Color.White, j == 0 ? 1.1f : 1.2f);
                }
            }
        }

        private Texture2D mainTex;
        private Texture2D endTex;

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Color lightColor = new Color(0, 0, 200);
            Color newLightColor = new Color(Math.Max(0, lightColor.R + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, lightColor.G + Math.Min(0, -projectile.alpha + 20)), Math.Max(0, lightColor.B + Math.Min(0, -projectile.alpha + 20)));
            BaseMod.BaseDrawing.AddLight(projectile.Center, newLightColor);
            if (mainTex == null) { mainTex = Main.projectileTexture[projectile.type]; endTex = mod.GetTexture("Projectiles/ChaosYariEXShot_Tip"); }
            BaseMod.BaseDrawing.DrawTexture(sb, spineEnd ? endTex : mainTex, 0, projectile);
            return false;
        }
    }
}