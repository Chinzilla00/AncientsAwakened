using AAMod.Items.Melee;
using Microsoft.Xna.Framework;
using Terraria;
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
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileHammer[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Dragon Egg");
            drop1 = mod.ItemType<Pyrosphere>();
            drop2 = mod.ItemType<Items.Ranged.Firebuster>();
            drop3 = mod.ItemType<Items.Magic.Volley>();
            drop4 = mod.ItemType<Items.Pets.DragonsSoul>();
            drop5 = mod.ItemType<Items.Accessories.DragonsGuard>();
            AddMapEntry(new Color(102, 45, 42), name);
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
                Main.NewText("The sound of the egg breaking echoes through the volcano...", Color.DarkOrange);
            }
            else if (AAWorld.SmashDragonEgg == 1)
            {
                AAWorld.SmashDragonEgg--;
                Main.NewText("You hear a distant roar...", Color.DarkOrange);
            }
            else
            {
                Player player = Main.player[BaseMod.BaseAI.GetPlayer(new Vector2(i, j), -1)];
                AAWorld.SmashDragonEgg = 2;
                AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Broodmother"), false, 0, 0, "The Broodmother");
            }
        }


    }
}