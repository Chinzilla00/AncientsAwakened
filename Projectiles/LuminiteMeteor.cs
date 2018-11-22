using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class LuminiteMeteor : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
        }

		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("");
		}

        public override void AI()
        {
            
        }

        public void Kill()
        {
            OreComet();
        }

        public void OreComet()
        {
            int x = Main.rand.Next(0, Main.maxTilesX);
            int y = Main.maxTilesY;
            int[] tileIDs = { mod.TileType("LuminiteOre") };
            if (Main.tile[x, y].type <= -1)
            {
                y++;
            }
            else
            {
                WorldGen.TileRunner(x, y, 2, 4, tileIDs[Main.rand.Next(tileIDs.Length)], false, 0f, 0f, true, true);
                return;
            }
        }
    }
}
