using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ObjectData;
using Terraria.ModLoader.IO;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class InvokerBook : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Aleister Book");
            Tooltip.SetDefault(@"A Legendary Book of Aleister 'Mega Therion'.
Increase 10% minion damage
Increase 2 minion slots
Maybe you need other items to help you get more power from this book.
Aleister's note: 
I need more powerful souls, *****,*********,**********");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = 11;
            item.accessory = true;
            item.useStyle = 4;
            item.useTime = 1;
            item.expertOnly = true;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
		{
            return false;
        }
        
        public override void UpdateEquip(Player player)
        {
            player.minionDamage += .1f;
            player.maxMinions += 2;

            InvokerPlayer InvokerPlayer = InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
        }
    }

    public class InvokerBookTile : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.addTile(Type);
			drop = mod.ItemType("InvokerBook");
			ModTranslation modTranslation = CreateMapEntryName(null);
			modTranslation.SetDefault("Aleister Book");
			AddMapEntry(Color.Gold, modTranslation);
			animationFrameHeight = 16;
		}

        public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = mod.ItemType("InvokerBook");
		}

        public override void RightClick(int i, int j)
		{
            Item.NewItem(i * 16, j * 16, 16, 16, mod.ItemType("InvokerBook"), 1, false, 0, false, false);
            WorldGen.KillTile(i, j, false, false, true);
		}
    }

    public class InvokerBookDrop : GlobalTile
    {
        public override void RandomUpdate(int i, int j, int type)
		{
            if (Main.expertMode && InvokerBookSet.InvokerBookSetOK && NPC.downedPlantBoss && type == 19 && (Main.tile[i, j].frameY == 10 * 18 || Main.tile[i, j].frameY == 11 * 18) && Main.tile[i, j - 1] != null && !Main.tile[i, j - 1].active())
            {
                WorldGen.PlaceTile(i, j - 1, mod.TileType("InvokerBookTile"), true, false);
                InvokerBookSet.InvokerBookSetOK = false;
                if (Main.netMode == 2 && Main.tile[i, j].active())
                {
                    NetMessage.SendTileSquare(-1, i, j, 1, 0);
                    return;
                }
            }
		}
    }
    public class InvokerBookSet : ModWorld
	{
        public override void Initialize()
		{
            InvokerBookSetOK = true;
        }
        public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (InvokerBookSetOK)
			{
				list.Add("InvokerBookSetOK");
			}
            TagCompound tagCompound = new TagCompound();
			tagCompound.Add("InvokerBookSet", list);
			return tagCompound;
        }

        public override void Load(TagCompound tag)
		{
            IList<string> list = tag.GetList<string>("InvokerBookSet");
            InvokerBookSetOK = list.Contains("InvokerBookSetOK");
        }
        public static bool InvokerBookSetOK;
    }
}