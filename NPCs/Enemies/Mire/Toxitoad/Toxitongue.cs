using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire.Toxitoad
{
    public class Toxitongue : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toxitoad");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = false;
            projectile.penetrate = -1; 
        }
		
		public override void AI()
        { 
            Vector2 vector54 = Main.npc[projectile.owner].Center - projectile.Center;
            projectile.rotation = vector54.ToRotation() - 1.57f;
            if (!Main.npc[projectile.owner].active)
            {
                projectile.Kill();
                return;
            }
            float arg_1C53D_0 = vector54.X;
            if (vector54.X < 0f)
            {
                Main.npc[projectile.owner].spriteDirection = 1;
                projectile.direction = 1;
            }
            else
            {
                Main.npc[projectile.owner].spriteDirection = -1;
                projectile.direction = -1;
            }
            Main.npc[projectile.owner].rotation = (vector54 * -1f * projectile.direction).ToRotation();
            projectile.spriteDirection = (vector54.X > 0f) ? -1 : 1;
            if (projectile.ai[0] == 0f && vector54.Length() > 400f)
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
                if (num687 > 600f)
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
            if ((int)projectile.ai[1] % 4 == 0)
            {
                Vector2 vector55 = vector54 * -1f;
                vector55.Normalize();
                vector55 *= Main.rand.Next(45, 65) * 0.1f;
                vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector55.X, vector55.Y, mod.ProjectileType("FlairdraCyclone"), projectile.damage, projectile.knockBack, projectile.owner, -10f, 0f);
                return;
            }
        }
		
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            target.AddBuff(BuffID.Venom, 600);
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			projectile.ai[0] = 1f;
			return false;
		}
        
        // chain voodoo
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {

            Texture2D texture = ModContent.GetTexture("AAMod/NPCs/Enemies/Mire/Toxitoad/Toxitongue_Chain");
            
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.npc[projectile.owner].Center;
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