using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles.Projectiles
{
    class TorchsandBall : ModProjectile
    {
        protected bool falling = true;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.ForcePlateDetection[projectile.type] = true;
        }

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<Dusts.RazewoodDust>(), 0f, 0f, 0);
                Main.dust[dust].velocity.X *= 0.4f;
            }
            projectile.tileCollide = true;
            projectile.localAI[1] = 0f;
            if (projectile.ai[0] == 1f)
            {
                if (!falling)
                {
                    projectile.ai[1] += 1f;
                    if (projectile.ai[1] >= 60f)
                    {
                        projectile.ai[1] = 60f;
                        projectile.velocity.Y += 0.2f;
                    }
                }
                else
                {
                    projectile.velocity.Y += 0.41f;
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                projectile.velocity.Y += 0.2f;
                if (projectile.velocity.X < -0.04f)
                {
                    projectile.velocity.X += 0.04f;
                }
                else if (projectile.velocity.X > 0.04f)
                {
                    projectile.velocity.X -= 0.04f;
                }
                else
                {
                    projectile.velocity.X = 0f;
                }
            }
            projectile.rotation += 0.1f;
            if (projectile.velocity.Y > 10f)
            {
                projectile.velocity.Y = 10f;
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            if (falling)
            {
                projectile.velocity = Collision.AnyCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true);
            }
            else
            {
                projectile.velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, fallThrough, fallThrough, 1);
            }
            return false;
        }



        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer && !projectile.noDropItem)
            {
                int tileX = (int)(projectile.position.X + projectile.width / 2) / 16;
                int tileY = (int)(projectile.position.Y + projectile.width / 2) / 16;
                if (Main.tile[tileX, tileY].halfBrick() && projectile.velocity.Y > 0f && System.Math.Abs(projectile.velocity.Y) > System.Math.Abs(projectile.velocity.X))
                {
                    tileY--;
                }
                if (!Main.tile[tileX, tileY].active())
                {
                    bool onMinecartTrack = tileY < Main.maxTilesY - 2 && Main.tile[tileX, tileY + 1] != null && Main.tile[tileX, tileY + 1].active() && Main.tile[tileX, tileY + 1].type == TileID.MinecartTrack;
                    if (!onMinecartTrack)
                    {
                        WorldGen.PlaceTile(tileX, tileY, ModContent.TileType<Torchsand>(), false, true, -1, 0);
                    }
                    if (!onMinecartTrack && Main.tile[tileX, tileY].active() && Main.tile[tileX, tileY].type == ModContent.TileType<Torchsand>())
                    {
                        if (Main.tile[tileX, tileY + 1].halfBrick() || Main.tile[tileX, tileY + 1].slope() != 0)
                        {
                            WorldGen.SlopeTile(tileX, tileY + 1, 0);
                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 14, tileX, tileY + 1, 0f, 0, 0, 0);
                            }
                        }
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 1, tileX, tileY, ModContent.TileType<Torchsand>(), 0, 0, 0);
                        }
                    }
                }
            }
        }

        public override bool CanDamage()
        {
            return projectile.localAI[1] != -1f;
        }
    }
}