using System;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{

    public class BogOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 32;    //projectile width
            projectile.height = 32;   //projectile height
            projectile.friendly = true;      //Wont damage enemies
            projectile.hostile = false;      //Wont damage you
            projectile.magic = true;         //It's magic!
            projectile.tileCollide = true;   //Will collide with tilesd
            projectile.penetrate = 1;      //It will burst on contact
            projectile.timeLeft = 600;   //Lasts a while
            projectile.light = 0.25f;    //Shiny
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true; //Water wont slow it down
            projectile.damage = 10;
            projectile.scale = 1f;
        }
        public override void AI()           //Behaviour
        {
            Lighting.AddLight(projectile.Center, 1f, 0.1f, 1f); //Pink light
                if (Main.rand.Next(2) == 0)  //Wont spam too much dust
                {
                    Dust.NewDust(projectile.Center, projectile.width/2, projectile.height/2, 72, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Microsoft.Xna.Framework.Color), 0.7f);
                }
                float magnitude = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);  //Damn engine doesn't have a magnitude function, use pythagoras theorem!
            if (magnitude > 0.5f)  //Bubble will slow down over time
            {
                    projectile.velocity.X /= 1.005f;
                    projectile.velocity.Y /= 1.005f;
                }
        }

        public override void Kill(int timeLeft)    //When it bursts
        {
            Player owner = Main.player[projectile.owner];  //Make sure the droplets are still owned by the player
            Main.PlaySound(SoundID.Item54, projectile.position); //*Pop!*
            if (Main.netMode != 1) //This is where things start to get random, so make sure the randomness is not being processed by the client!
            {
                for (int k = 0; k < Main.rand.Next(21) + 10; k++)  //20 to 30 droplets at once!
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Main.rand.Next(401) - 200) / 100, (float)(Main.rand.Next(400) - 1600) / 100, mod.ProjectileType("Drop"), projectile.damage, 2f, projectile.owner,0f,0f);
                }
            }
        }
    }
}
