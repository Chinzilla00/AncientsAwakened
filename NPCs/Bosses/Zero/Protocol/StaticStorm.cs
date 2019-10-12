using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero.Protocol
{
    public class StaticStorm : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Storm");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 180;
            projectile.alpha = 100;
            projectile.penetrate = -1;
        }
        public float[] internalAI = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }
        public override void AI()
        {
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
                projectile.ai[1] = projectile.velocity.Length();
                projectile.netUpdate = true;
            }

            if (projectile.timeLeft < 40)
            {
                projectile.alpha += 5;
            }
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[1] / (2 * Math.PI * projectile.ai[0] * ++projectile.localAI[0]));

            internalAI[0] += 1f;
            if (internalAI[0] % 30f == 0f && internalAI[0] < 180f && Main.netMode != 1)
            {
                int[] array4 = new int[5];
                Vector2[] array5 = new Vector2[5];
                int num838 = 0;
                float num839 = 2000f;
                for (int num840 = 0; num840 < 255; num840++)
                {
                    if (Main.player[num840].active && !Main.player[num840].dead)
                    {
                        Vector2 center9 = Main.player[num840].Center;
                        float num841 = Vector2.Distance(center9, projectile.Center);
                        if (num841 < num839 && Collision.CanHit(projectile.Center, 1, 1, center9, 1, 1))
                        {
                            array4[num838] = num840;
                            array5[num838] = center9;
                            if (++num838 >= array5.Length)
                            {
                                break;
                            }
                        }
                    }
                }
                for (int num842 = 0; num842 < num838; num842++)
                {
                    Vector2 vector82 = array5[num842] - projectile.Center;
                    float ai = Main.rand.Next(100);
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 10f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector83.X, vector83.Y, ModContent.ProjectileType<ZeroShock>(), projectile.damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }

            if (projectile.frameCounter++ > 4)
            {
                projectile.frameCounter = 0;
                if (projectile.frame++ > 3)
                    projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            target.AddBuff(ModContent.BuffType<Buffs.Glitched>(), 120);
        }

        public override void Kill(int timeLeft)
        {
            int num1 = 36;
            for (int index1 = 0; index1 < num1; ++index1)
            {
                Vector2 vector2_1 = (Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width / 2f, projectile.height) * 0.75f).RotatedBy((index1 - (num1 / 2 - 1)) * 6.28318548202515 / num1, new Vector2()) + projectile.Center;
                Vector2 vector2_2 = vector2_1 - projectile.Center;
                int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, 226, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity = vector2_2;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = projectile.GetAlpha(color26);

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color27 = color26;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 250, 200);
        }
    }
}