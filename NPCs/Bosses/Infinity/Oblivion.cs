using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class Oblivion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion");
            Main.projFrames[projectile.type] = 24;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public int OblivionSpeech = 0;


        public override void AI()
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 0.5f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(Color.Red, Color.Black, Pie);

            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 23)
                {
                    projectile.frame = 0;
                }
            }

            projectile.velocity.X = 0;

            projectile.velocity.Y = 0;

            Player player = Main.player[Main.myPlayer];
            OblivionSpeech++;
            if (OblivionSpeech == 120)
            {
                Main.NewText("...impressive.", color1);
            }
            if (OblivionSpeech == 300)
            {
                Main.NewText("Defeating my mechanical body...", color1);
            }
            if (OblivionSpeech == 360)
            {
                Main.NewText("...Is not a small feat...", color1);
            }
            if (OblivionSpeech == 540)
            {
                Main.NewText("I applaud you, terrarian.", color1);
            }
            if (OblivionSpeech == 660)
            {
                Main.NewText("Although...next time we meet...when you're stronger...", color1);
            }
            if (OblivionSpeech == 720)
            {
                Main.NewText("..." + player.name + "...", color1);
            }
            if (OblivionSpeech == 820)
            {
                Main.NewText("I won't be so forgiving.", color1);
            }
            if (OblivionSpeech >= 820)
            {
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
        }
    }
}