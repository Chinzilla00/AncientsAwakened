using AAMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class DragonEgg : ModTile
    {
        public int drop1;
        public int drop2;
        public int drop3;
        public int drop4;
        public int drop5;

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileHammer[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Dragon Egg");
            drop1 = mod.ItemType<DragonClaw>(); //change me
            drop2 = mod.ItemType<DragonClaw>(); //change me
            drop3 = mod.ItemType<DragonClaw>(); //change me
            drop4 = mod.ItemType<DragonClaw>(); //change me
            drop5 = mod.ItemType<DragonClaw>(); //change me
            AddMapEntry(new Color(200, 200, 200), name);
            disableSmartCursor = true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.9f;
            g = 0.0f;
            b = 0.3f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int thinger = Main.rand.Next(5);

            if (thinger == 0)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop1);
            }
            else if (thinger == 1)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop2);
            }
            else if (thinger == 2)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop3);
            }
            else if (thinger == 3)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop4);
            }
            else
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop5);
            }

            if (AAWorld.SmashDragonEgg == 2)
            {
                AAWorld.SmashDragonEgg--;
                //message
            }
            else if (AAWorld.SmashDragonEgg == 1)
            {
                AAWorld.SmashDragonEgg--;
                //message
            }
            else
            {
                AAWorld.SmashDragonEgg = 2;
                //boss
            }
        }
    }
}