
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles.Chests
{
    public class DoomsdayChest : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileSpelunker[Type] = true;
            Main.tileContainer[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1200;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileValue[Type] = 500;
            TileID.Sets.HasOutlines[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Doomsday Chest");
            AddMapEntry(new Color(150, 75, 0), name, MapChestName);
            name = CreateMapEntryName(Name + "_Locked"); // With multiple map entries, you need unique translation keys.
            name.SetDefault("{$Mods.AAMod.Common.DoomsdayChest_Locked}");
            AddMapEntry(new Color(0, 141, 63), name, MapChestName);
            dustType = mod.DustType("DoomDust");
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Containers };
            chest = "Doomsday Chest";
            chestDrop = mod.ItemType("DoomsdayChest");
        }

        public override ushort GetMapOption(int i, int j) => (ushort)(Main.tile[i, j].frameX / 36);

        public override bool HasSmartInteract() => true;

        public override bool IsLockedChest(int i, int j) => Main.tile[i, j].frameX / 36 == 1;

        public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
        {
            dustType = this.dustType;
            return true;
        }

        public string MapChestName(string name, int i, int j)
        {
            int left = i;
            int top = j;
            Tile tile = Main.tile[i, j];
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            if (Main.chest[chest].name == "")
            {
                return name;
            }
            else
            {
                return name + ": " + Main.chest[chest].name;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            return Chest.CanDestroyChest(left, top);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("DoomsdayChest"));
            Chest.DestroyChest(i, j);
        }

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            Main.mouseRightRelease = false;
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            if (player.sign >= 0)
            {
                Main.PlaySound(SoundID.MenuClose);
                player.sign = -1;
                Main.editSign = false;
                Main.npcChatText = "";
            }
            if (Main.editChest)
            {
                Main.PlaySound(SoundID.MenuTick);
                Main.editChest = false;
                Main.npcChatText = "";
            }
            if (player.editedChestName)
            {
                NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
                player.editedChestName = false;
            }
            bool isLocked = IsLockedChest(left, top);
            if (Main.netMode == NetmodeID.MultiplayerClient && !isLocked)
            {
                if (left == player.chestX && top == player.chestY && player.chest >= 0)
                {
                    player.chest = -1;
                    Recipe.FindRecipes();
                    Main.PlaySound(SoundID.MenuClose);
                }
                else
                {
                    NetMessage.SendData(MessageID.RequestChestOpen, -1, -1, null, left, top, 0f, 0f, 0, 0, 0);
                    Main.stackSplit = 600;
                }
            }
            else
            {
                if (isLocked)
                {
                    int key = ModContent.ItemType<Items.Usable.DoomstopperKey>();
                    if (player.ConsumeItem(key) && Chest.Unlock(left, top))
                    {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.Unlock, -1, -1, null, player.whoAmI, 1f, left, top);
                        }
                    }
                }
                else
                {
                    int chest = Chest.FindChest(left, top);
                    if (chest >= 0)
                    {
                        Main.stackSplit = 600;
                        if (chest == player.chest)
                        {
                            player.chest = -1;
                            Main.PlaySound(SoundID.MenuClose);
                        }
                        else
                        {
                            player.chest = chest;
                            Main.playerInventory = true;
                            Main.recBigList = false;
                            player.chestX = left;
                            player.chestY = top;
                            Main.PlaySound(player.chest < 0 ? SoundID.MenuOpen : SoundID.MenuTick);
                        }
                        Recipe.FindRecipes();
                    }
                }
            }
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.frameX % 36 != 0)
            {
                left--;
            }
            if (tile.frameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            player.showItemIcon2 = -1;
            if (chest < 0)
            {
                player.showItemIconText = Language.GetTextValue("LegacyChestType.0");
            }
            else
            {
                player.showItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Doomsday Chest";
                if (player.showItemIconText == "Doomsday Chest")
                {
                    player.showItemIcon2 = mod.ItemType("DoomsdayChest");
                    if (Main.tile[left, top].frameX / 36 == 1)
                        player.showItemIcon2 = ModContent.ItemType<Items.Usable.DoomstopperKey>();
                    player.showItemIconText = "";
                }
            }
            player.noThrow = 2;
            player.showItemIcon = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.showItemIconText == "")
            {
                player.showItemIcon = false;
                player.showItemIcon2 = 0;
            }
        }

        private int LockFrame = 0;

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 6)
            {
                frameCounter = 0;
                if (++LockFrame >= 8)
                {
                    LockFrame = 0;
                }
            }
        }

        public Color GetColor(Color color)
        {
            Color glowColor = AAColor.ZeroShield;
            return glowColor;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D LockTex = mod.GetTexture("Tiles/Chests/DoomsdayChestLockedFrame");
            Texture2D glowTex = mod.GetTexture("Glowmasks/DoomsdayChest_Glow");

            int frameX = tile != null && tile.active() ? tile.frameX + (Main.tileFrame[Type] * 36) : 0;
            int frameY = tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 38) : 0;

            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, frameX, frameY, false, false, false, null, GetColor);

            int LockframeY = tile != null && tile.active() ? tile.frameY + (LockFrame * 38) : 0;

            if (IsLockedChest(x, y))
            {
                BaseDrawing.DrawTileTexture(sb, LockTex, x, y, 16, 16, 36, LockframeY, false, false, false, null, GetColor);
            }
        }
    }
}