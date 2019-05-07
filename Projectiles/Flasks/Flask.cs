using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Dusts;

namespace AAMod.Projectiles.Flasks
{
    public class Flask : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flask");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.damage = 0;
            projectile.penetrate = -1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            projectile.tileCollide = true;
            aiType = 48;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = null;
            if (projectile.ai[0] == 0)
            {
                tex = mod.GetTexture("Projectiles/Flasks/PurityFlask");
            }
            else if (projectile.ai[0] == 1)
            {
                tex = mod.GetTexture("Projectiles/Flasks/CorruptionFlask");
            }
            else if (projectile.ai[0] == 2)
            {
                tex = mod.GetTexture("Projectiles/Flasks/HallowFlask");
            }
            else if (projectile.ai[0] == 3)
            {
                tex = mod.GetTexture("Projectiles/Flasks/GlowingSporeSac");
            }
            else if (projectile.ai[0] == 4)
            {
                tex = mod.GetTexture("Projectiles/Flasks/CrimsonFlask");
            }
            else if (projectile.ai[0] == 5)
            {
                tex = mod.GetTexture("Projectiles/Flasks/AshJar");
            }
            else if (projectile.ai[0] == 6)
            {
                tex = mod.GetTexture("Projectiles/Flasks/DarkwaterFlask");
            }
            else if (projectile.ai[0] == 7)
            {
                tex = mod.GetTexture("Projectiles/Flasks/VoidFlask");
            }
            else if (projectile.ai[0] == 9)
            {
                tex = mod.GetTexture("Projectiles/Flasks/SporeSac");
            }
            else if (projectile.ai[0] == 10)
            {
                tex = mod.GetTexture("Projectiles/Flasks/Fungicide");
            }
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, tex.Width, tex.Height, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, tex, 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 7, frame, lightColor, true);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Shatter, (int)position.X, (int)position.Y);

            int radius = 6;
            int FlaskDust = 0;

            for (int i = 0; i < 8; i++)
            {
                if (projectile.ai[0] == 0)
                {
                    FlaskDust = 110;
                }
                else if (projectile.ai[0] == 1)
                {
                    FlaskDust = 111;
                }
                else if (projectile.ai[0] == 2)
                {
                    FlaskDust = 112;
                }
                else if (projectile.ai[0] == 3)
                {
                    FlaskDust = 113;
                }
                else if (projectile.ai[0] == 4)
                {
                    FlaskDust = 114;
                }
                else if (projectile.ai[0] == 5)
                {
                    FlaskDust = mod.DustType<Dusts.OrangeSolution>();
                }
                else if (projectile.ai[0] == 6)
                {
                    FlaskDust = mod.DustType<Dusts.IndigoSolution>();
                }
                else if (projectile.ai[0] == 7)
                {
                    FlaskDust = mod.DustType<VoidDust>();
                }
                else if (projectile.ai[0] == 9)
                {
                    FlaskDust = mod.DustType<MushDust>();
                }
                else if (projectile.ai[0] == 10)
                {
                    FlaskDust = mod.DustType<SwarmDust>();
                }

                for (int m = 0; m < 20; m++)
                {
                    int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 1.6f);
                    Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (m / 20) * 6.28f);
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
                for (int m = 0; m < 20; m++)
                {
                    int dustID = Dust.NewDust(new Vector2(projectile.Center.X - 1, projectile.Center.Y - 1), 2, 2, FlaskDust, 0f, 0f, 100, Color.White, 2f);
                    Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (m / 20) * 6.28f);
                    Main.dust[dustID].noLight = false;
                    Main.dust[dustID].noGravity = true;
                }
            }

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //circle
                    {
                        if (projectile.ai[0] == 0)
                        {
                            WorldGen.Convert(xPosition, yPosition, 0, 1); //Purity
                        }
                        else if (projectile.ai[0] == 1)
                        {
                            WorldGen.Convert(xPosition, yPosition, 1, 1); //Corruption
                        }
                        else if (projectile.ai[0] == 2)
                        {
                            WorldGen.Convert(xPosition, yPosition, 2, 1); //Hallow
                        }
                        else if (projectile.ai[0] == 3)
                        {
                            WorldGen.Convert(xPosition, yPosition, 3, 1); //Mushroom
                        }
                        else if (projectile.ai[0] == 4)
                        {
                            WorldGen.Convert(xPosition, yPosition, 4, 1); //Crimson
                        }
                        else if (projectile.ai[0] == 5)
                        {
                            AAWorld.AAConvert(xPosition, yPosition, 1, 1); //Inferno
                        }
                        else if (projectile.ai[0] == 6)
                        {
                            AAWorld.AAConvert(xPosition, yPosition, 2, 1); //Mire
                        }
                        else if (projectile.ai[0] == 7)
                        {
                            AAWorld.AAConvert(xPosition, yPosition, 3, 1); //Void
                        }
                        else if (projectile.ai[0] == 9)
                        {
                            AAWorld.AAConvert(xPosition, yPosition, 4, 1); //Surface Mushroom
                        }
                        else if (projectile.ai[0] == 10)
                        {
                            AAWorld.AAConvert(xPosition, yPosition, 5, 1); //Fungicide
                        }
                    }
                }
            }
        }
    }
}