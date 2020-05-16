using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.DevTile
{
    public class DevTileCreat : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type)
		{
            if (Main.expertMode)
            {
                if(DevWorld.CCBoxSetOK)
                {
                    if(AAWorld.downedEquinox)
                    {
                        bool canplace = (type == mod.TileType("MireGrass") || type == mod.TileType("Depthstone")) && (Main.tile[i + 1, j - 1].type == mod.TileType("MireGrass") || type == mod.TileType("Depthstone")) && !Main.tile[i, j - 1].active() && !Main.tile[i + 1, j - 1].active() && j > Main.worldSurface + 200;
                        if(canplace)
                        {
                            WorldGen.PlaceTile(i, j - 1, mod.TileType("CCMireBox"), true, false);
                            DevWorld.CCBoxSetOK = false;
                            if (Main.netMode == NetmodeID.Server && Main.tile[i, j].active())
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1, 0);
                            }
                        }
                    }
                }
                if(DevWorld.InvokerBookSetOK)
                {
                    if(NPC.downedPlantBoss)
                    {
                        bool canplace = type == 19 && (Main.tile[i, j].frameY == 10 * 18 || Main.tile[i, j].frameY == 11 * 18) && !Main.tile[i, j - 1].active();
                        if(canplace)
                        {
                            WorldGen.PlaceTile(i, j - 1, mod.TileType("InvokerBookTile"), true, false);
                            DevWorld.InvokerBookSetOK = false;
                            if (Main.netMode == NetmodeID.Server && Main.tile[i, j].active())
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1, 0);
                            }
                        }
                    }
                }
            }
		}
    }
}