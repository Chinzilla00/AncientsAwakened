using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Yamata
{
    public class Flairdra : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flairdra");

            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.penetrate = -1; 
            projectile.melee = true; 
        }
		
		public override void AI()
        { 
            Vector2 vector54 = Main.player[projectile.owner].Center - projectile.Center;
            projectile.rotation = vector54.ToRotation() - 1.57f;
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            Main.player[projectile.owner].itemAnimation = 20;
            Main.player[projectile.owner].itemTime = 20;
            float arg_1C53D_0 = vector54.X;
            if (vector54.X < 0f)
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
            Main.player[projectile.owner].itemRotation = (vector54 * -1f * projectile.direction).ToRotation();
            projectile.spriteDirection = (vector54.X > 0f) ? -1 : 1;
            if (projectile.ai[0] == 0f && vector54.Length() > 600f)
            {
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 1f || projectile.ai[0] == 2f)
            {
                float num687 = vector54.Length();
                if (num687 > 1500f)
                {
                    projectile.Kill();
                    return;
                }
                if (num687 > 800f)
                {
                    projectile.ai[0] = 2f;
                }
                projectile.tileCollide = false;
                float num688 = 20f;
                if (projectile.ai[0] == 2f)
                {
                    num688 = 40f;
                }
                projectile.velocity = Vector2.Normalize(vector54) * num688;
                if (vector54.Length() < num688)
                {
                    projectile.Kill();
                    return;
                }
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 5f)
            {
                projectile.alpha = 0;
            }
            if ((int)projectile.ai[1] % 4 == 0 && projectile.owner == Main.myPlayer)
            {
                Vector2 vector55 = vector54 * -1f;
                vector55.Normalize();
                vector55 *= (float)Main.rand.Next(45, 65) * 0.1f;
                vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector55.X, vector55.Y, mod.ProjectileType("FlairdraCyclone"), projectile.damage, projectile.knockBack, projectile.owner, -10f, 0f);
                return;
            }
        }

        public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            if (Main.netMode != 1 && Main.rand.Next(2) == 0)
            {
            target.immune[projectile.owner] = 1;
                Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / 3;
                for (int i = 0; i < 3; i++)
                {
                    double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f) * 5, (float)(Math.Cos(offsetAngle) * 3f) * 5, mod.ProjectileType("YWSplit"), projectile.damage / 3, projectile.knockBack, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f) * 5, (float)(-Math.Cos(offsetAngle) * 3f) * 5, mod.ProjectileType("YWSplit"), projectile.damage / 3, projectile.knockBack, projectile.owner, 0f, 0f);
                }
                projectile.netUpdate = true;
            }
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			//projectile.tileCollide = false;
			//projectile.timeLeft = 20;
			projectile.ai[0] = 1f;
			return false;
		}
        
        // chain voodoo
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            projectile.frameCounter++;
            if (projectile.frameCounter >= 10)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 7)
                { 
                    projectile.frame = 0; 
                }
            }

            Texture2D texture = mod.GetTexture("Chains/Flairdra_Chain");
            
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}