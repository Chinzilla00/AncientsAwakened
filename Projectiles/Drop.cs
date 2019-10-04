using System;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{

    public class Drop : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 12;   //projectile width
            projectile.height = 12;  //projectile height
            projectile.friendly = false;      //Wont damage enemies
            projectile.hostile = false;       //Wont damage you
            projectile.magic = true;          //It's magic!
            projectile.tileCollide = true;   //Less cheese if you wish
            projectile.penetrate = 10;      //Lots of piercing
            projectile.timeLeft = 600;   //Lasts long enough to hit the ground
            projectile.light = 0.25f;    //Shiny
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true; //Water can't slow down water
            projectile.damage = 10; //You decide
            projectile.scale = 1f; //Sprite scale
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f; //Face the speed!
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Microsoft.Xna.Framework.Color), 0.7f);
            } //Fancy Shmancy particles

            projectile.velocity.Y = projectile.velocity.Y + 0.08f; //Gravity
            if (projectile.velocity.Y >= 0) //Is it going down?
            {
                //projectile.tileCollide = true; //Just in case
                projectile.friendly = true; //Yes, collide with enemies
            }
            else
            {
                //projectile.tileCollide = false; //Just in case
                projectile.friendly = false;  //No, don't collide with enemies!
            }

        }
        public override void Kill(int timeLeft)    //When it breaks
        {
            Main.PlaySound(SoundID.NPCHit3, projectile.position); //*Poof!*
        }
    }
}
