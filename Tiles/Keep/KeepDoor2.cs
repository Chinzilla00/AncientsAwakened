using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Keep
{
    public class KeepDoor2 : ModTile
    {
        private bool _activated = false;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.NotReallySolid[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Sealed Door");
            AddMapEntry(new Color(80, 100, 80), name);
            minPick = 500;
            mineResist = 10f;
            dustType = 107;
            animationFrameHeight = 54;
            disableSmartCursor = true;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (_activated)
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (!AAWorld.Terra2)
            {
                Main.tileSolid[Type] = false;
                _activated = false;
            }
            else
            {
                Main.tileSolid[Type] = true;
                _activated = true;
            }
        }
    }
}