using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Tiles;

namespace AAMod.Projectiles
{
    public class VoidBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VoidBomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 170;
        }

        public override string Texture
        {
            get
            {
                return "Fargowiltas/Items/Misc/MireRenewal";
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Shatter, (int)position.X, (int)position.Y);

            int radius = 100;
            float[] speedX = { 0, 0, 5, 5, 5, -5, -5, -5 };
            float[] speedY = { 5, -5, 0, 5, -5, 0, 5, -5 };

            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, speedX[i], speedY[i], mod.ProjectileType("IndigoSolution"), 0, 0, Main.myPlayer);
            }

            int Size = 4;

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int xPosition = (int)(i + position.X / 16.0f);
                    int yPosition = (int)(j + position.Y / 16.0f);

                    if (Math.Sqrt(i * i + j * j) <= radius + 0.5)   //circle
                    {
                        for (int k = i - Size; k <= i + Size; k++)
                        {
                            for (int l = j - Size; l <= j + Size; l++)
                            {
                                if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(Size * Size + Size * Size))
                                {
                                    int type = Main.tile[k, l].type;
                                    if (TileID.Sets.Conversion.Stone[type])
                                    {
                                        Main.tile[k, l].type = (ushort)mod.TileType<DoomstoneB>();
                                        WorldGen.SquareTileFrame(k, l, true);
                                        NetMessage.SendTileSquare(-1, k, l, 1);
                                    }
                                    else if (TileID.Sets.Conversion.Grass[type])
                                    {
                                        Main.tile[k, l].type = (ushort)mod.TileType<Doomgrass>();
                                        WorldGen.SquareTileFrame(k, l, true);
                                        NetMessage.SendTileSquare(-1, k, l, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}