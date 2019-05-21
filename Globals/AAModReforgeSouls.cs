using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Items.Usable;

namespace AAMod
{
    public class AAModReforgeSouls : GlobalItem
	{
        /*public ReforgeHammer Hammer = null;
        public ReforgeHammerH HammerH = null;
        public ReforgeHammerF HammerF = null;
        public ReforgeHammerM HammerM = null;*/

        public override bool CanRightClick(Item item)
		{
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == mod.ItemType("Godly") && reforgable) ||
                (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee) || 
                (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown)) ||
                (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic)))
			{
				return true;
			}

            /*if ((Main.mouseItem.type == mod.ItemType("ReforgeHammer") || Main.mouseItem.type == mod.ItemType("ReforgeHammerF") || Main.mouseItem.type == mod.ItemType("ReforgeHammerH") || Main.mouseItem.type == mod.ItemType("ReforgeHammerM")) && item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
            {
                return true;
            }*/
            return base.CanRightClick(item);
		}


		public override void RightClick(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == mod.ItemType("Godly") && reforgable) ||
                (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee) ||
                (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown)) ||
                (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic)))
            { 
                Main.mouseItem.stack = 0;
			}

            /*if (reforgable)
            {
                if (Main.mouseItem.type == mod.ItemType("ReforgeHammer"))
                {
                    Item HammerType = Main.item[mod.ItemType<ReforgeHammer>()];
                    Hammer = (ReforgeHammer)HammerType.modItem;

                    Item item2 = player.GetItem(player.whoAmI, item, false, true);
                    item2.Prefix(-2);

                    int ReforgedItem = Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 81);
                    Main.item[ReforgedItem].newAndShiny = false;

                    Hammer.Durability -= 1;
                    if (Hammer.Durability <= 0)
                    {
                        Hammer.item.TurnToAir();
                    }
                }
                if (Main.mouseItem.type == mod.ItemType("ReforgeHammerH"))
                {
                    Item HammerType = Main.item[mod.ItemType<ReforgeHammerH>()];
                    HammerH = (ReforgeHammerH)HammerType.modItem;
                    HammerH.Durability -= 1;
                    if (HammerH.Durability <= 0)
                    {
                        HammerH.item.TurnToAir();
                    }
                }
                if (Main.mouseItem.type == mod.ItemType("ReforgeHammerF"))
                {
                    Item HammerType = Main.item[mod.ItemType<ReforgeHammerF>()];
                    HammerF = (ReforgeHammerF)HammerType.modItem;
                    HammerF.Durability -= 1;
                    if (HammerF.Durability <= 0)
                    {
                        HammerF.item.TurnToAir();
                    }
                }
                if (Main.mouseItem.type == mod.ItemType("ReforgeHammerM"))
                {
                    Item HammerType = Main.item[mod.ItemType<ReforgeHammerM>()];
                    HammerM = (ReforgeHammerM)HammerType.modItem;
                    HammerM.Durability -= 1;
                    if (HammerM.Durability <= 0)
                    {
                        HammerM.item.TurnToAir();
                    }
                }
            }*/
        }
		
		public override bool ConsumeItem(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;
            if (Main.mouseItem.type == mod.ItemType("Godly") && reforgable)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 59);
			}
			if (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 81);
			}
			if (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown))
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 82);
			}
			if (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic))
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 83);
			}
            return base.ConsumeItem(item, player);
		}
    }
}
