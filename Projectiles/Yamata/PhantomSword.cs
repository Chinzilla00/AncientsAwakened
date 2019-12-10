using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Yamata
{

    public class PhantomSword : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;       //projectile width
            projectile.height = 30;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.hostile = false;
            projectile.melee = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 10;      //how many npc will penetrate
            projectile.timeLeft = 300;   //how many time this projectile has before disepire
            projectile.light = 0.25f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.damage = 1;
            projectile.scale = 0.75f;
            projectile.usesIDStaticNPCImmunity = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }

        private const int AI_Timer_Slot = 1;

        public float AI_Timer
        {
            get => projectile.ai[AI_Timer_Slot];
            set => projectile.ai[AI_Timer_Slot] = value;
        }

        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f + ((float)Math.PI * 0.25f);
            if (projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref projectile.velocity);
                projectile.localAI[0] = 1f;
            }

            Vector2 move = Vector2.Zero;
            bool target = false;
            AI_Timer++;
            if (AI_Timer >= 20)
            {
                projectile.tileCollide = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, .5f, 1f, 12, false, 0f, 0f);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, lightColor, false);
            return false;
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 15f)
            {
                vector *= 15f / magnitude;
            }
        }
    }
}
