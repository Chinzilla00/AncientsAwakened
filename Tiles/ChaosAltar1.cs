using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BaseMod;
using AAMod;

namespace AAMod.Tiles
{
    public class ChaosAltar1 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);
            Main.tileHammer[Type] = true;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Abyss Altar");
            dustType = mod.DustType("AbyssiumDust");
            AddMapEntry(new Color(120, 0, 160), name);
            adjTiles = new int[] { TileID.DemonAltar };
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            //delte this and you die
            Player player = Main.player[Main.myPlayer];
            if (!Main.hardMode)
            {
                if (blockDamaged == true)
                {
                    DamagePlayer(player);
                    blockDamaged = false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            AAWorld.SmashAltar(mod, i, j);
        }

        public void DamagePlayer (Player player)
        {
            player.statLife -= player.statLifeMax / 10;
        }
	}
}