using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class ChaosAltar2 : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);
            Main.tileHammer[Type] = true;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
            Main.tileLighted[Type] = true;
            name.SetDefault("Dragon Altar");
            dustType = mod.DustType("IncineriteDust");
            AddMapEntry(new Color(160, 100, 0), name);
            adjTiles = new int[] { TileID.DemonAltar };
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            //delte this and you die
            Player player = Main.LocalPlayer;
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

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseMod.BaseUtility.ColorMult(AAPlayer.IncineriteColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            WorldGen.SmashAltar(i, j);
        }

        public void DamagePlayer (Player player)
        {
            player.statLife -= player.statLifeMax / 10;
        }
	}
}