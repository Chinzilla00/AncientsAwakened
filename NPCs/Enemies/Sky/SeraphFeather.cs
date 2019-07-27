using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Sky
{
	public class SeraphFeather : ModProjectile
	{	
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.HarpyFeather);
        }

        public override void Kill(int timeLeft)
        {
            for (int num610 = 0; num610 < 10; num610++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 42, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default, 1f);
            };
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, 1f, 1f, 5, false, 0f, 0f, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, lightColor, false);
            return false;
        }
    }
}