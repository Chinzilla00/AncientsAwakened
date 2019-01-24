using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Akuma
{
    public class Daystorm : ModProjectile
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
            DisplayName.SetDefault("Daystorm");

            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = 75;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
            
            
        }
        

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            int num2 = 0;
            if (projectile.ai[0] >= 40f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 80f)
            {
                num2++;
            }
            if (projectile.ai[0] >= 120f)
            {
                num2++;
            }
            int num3 = 24;
            int num4 = 6;
            projectile.ai[1] += 1f;
            bool flag = false;
            if (projectile.ai[1] >= (float)(num3 - num4 * num2))
            {
                projectile.ai[1] = 0f;
                flag = true;
            }
            projectile.frameCounter += 1 + num2;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num3 - num4 * num2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(SoundID.Item91, projectile.position);
                }
            }
            if (projectile.ai[1] == 1f && projectile.ai[0] != 1f)
            {
                Vector2 vector2 = Vector2.UnitX * 24f;
                vector2 = vector2.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                Vector2 value = projectile.Center + vector2;
                for (int i = 0; i < 2; i++)
                {
                    int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, mod.DustType("AkumaADust"), projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 100, default(Color), 1f);
                    Main.dust[num5].velocity *= 0.66f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].scale = 1.4f;
                }
            }
            if (flag && Main.myPlayer == projectile.owner)
            {
                Player player = Main.player[projectile.owner];
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                if (player.direction == 1)
                {
                    projectile.direction = 1;
                }
                else
                {
                    projectile.direction = -1;
                }
                bool flag2 = player.channel && player.CheckMana(player.inventory[player.selectedItem].mana, true, false) && !player.noItems && !player.CCed;
                if (flag2)
                {
                    float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value2 = vector;
                    Vector2 value3 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - value2;
                    if (player.gravDir == -1f)
                    {
                        value3.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value2.Y;
                    }
                    Vector2 vector3 = Vector2.Normalize(value3);
                    if (float.IsNaN(vector3.X) || float.IsNaN(vector3.Y))
                    {
                        vector3 = -Vector2.UnitY;
                    }
                    vector3 *= scaleFactor;
                    if (vector3.X != projectile.velocity.X || vector3.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector3;
                    int num6 = mod.ProjectileType("Dayser");
                    float scaleFactor2 = 14f;
                    int num7 = 7;
                    for (int j = 0; j < 2; j++)
                    {
                        value2 = projectile.Center + new Vector2((float)Main.rand.Next(-num7, num7 + 1), (float)Main.rand.Next(-num7, num7 + 1));
                        Vector2 spinningpoint = Vector2.Normalize(projectile.velocity) * scaleFactor2;
                        spinningpoint = spinningpoint.RotatedBy(Main.rand.NextDouble() * 0.19634954631328583 - 0.098174773156642914, default(Vector2));
                        if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
                        {
                            spinningpoint = -Vector2.UnitY;
                        }
                        Projectile.NewProjectile(value2.X, value2.Y, spinningpoint.X, spinningpoint.Y, num6, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D14 = Main.projectileTexture[projectile.type];
            int num215 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = num215 * projectile.frame;
            Vector2 vector27 = (projectile.position + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            float scale5 = 1f;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
            if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
            {
                color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            if (Main.player[projectile.owner].shroomiteStealth && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
            {
                float num216 = Main.player[projectile.owner].stealth;
                if ((double)num216 < 0.03)
                {
                    num216 = 0.03f;
                }
                float arg_97B3_0 = (1f + num216 * 10f) / 11f;
                color25 *= num216;
                scale5 = num216;
            }
            if (Main.player[projectile.owner].setVortex && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
            {
                float num217 = Main.player[projectile.owner].stealth;
                if ((double)num217 < 0.03)
                {
                    num217 = 0.03f;
                }
                float arg_9854_0 = (1f + num217 * 10f) / 11f;
                color25 = color25.MultiplyRGBA(new Microsoft.Xna.Framework.Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num217)));
                scale5 = num217;
            }
            Main.spriteBatch.Draw(texture2D14, vector27, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, texture2D14.Width, num215)), projectile.GetAlpha(color25), projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Main.glowMaskTexture[35], vector27, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, texture2D14.Width, num215)), new Microsoft.Xna.Framework.Color(255, 255, 255, 0) * scale5, projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
