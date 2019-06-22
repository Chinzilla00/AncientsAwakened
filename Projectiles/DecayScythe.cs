using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles     //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class DecayScythe : ModProjectile
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            projectile.glowMask = customGlowMask;


        }

        public override void SetDefaults()
        {
            projectile.width = 104;
            projectile.height = 96;
            projectile.friendly = true;
            projectile.penetrate = -1; 
            projectile.tileCollide = false;
            projectile.ignoreWater = true;  
            projectile.melee = true;
            
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }
            Lighting.AddLight(projectile.Center, 0.52f, .40f, .80f);     //this is the projectile light color R, G, B (Red, Green, Blue)
            projectile.Center = player.MountedCenter;
            projectile.position.X += player.width / 2 * player.direction;  //this is the projectile width sptrite direction from the playr
            projectile.spriteDirection = player.direction;
            projectile.rotation += .5f * player.direction; //this is the projectile rotation/spinning speed
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;
            if (Main.netMode != 1)
            {
                projectile.ai[1]++;
                if (projectile.ai[1] > 30)
                {
                    projectile.ai[1] = 0;
                    Vector2 vector = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
                    float num22 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
                    float num23 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
                    if (player.gravDir == -1f)
                    {
                        num23 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
                    }
                    float num24 = (float)Math.Sqrt((double)(num22 * num22 + num23 * num23));
                    if ((float.IsNaN(num22) && float.IsNaN(num23)) || (num22 == 0f && num23 == 0f))
                    {
                        num22 = (float)player.direction;
                        num23 = 0f;
                        num24 = 10;
                    }
                    else
                    {
                        num24 = 10 / num24;
                    }
                    num22 *= num24;
                    num23 *= num24;
                    Main.PlaySound(SoundID.Item71, (int)projectile.Center.X, (int)projectile.Center.Y);
                    int a = Projectile.NewProjectile(vector.X, vector.Y, num22, num23, mod.ProjectileType<DecayScytheProj>(), projectile.damage, projectile.knockBack, player.whoAmI, 0f, 0f);
                    Main.projectile[a].netUpdate = true;

                }
            }
            
 
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 1000);
            target.AddBuff(BuffID.CursedInferno, 1000);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

    }
}
