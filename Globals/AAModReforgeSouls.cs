using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
{
    /*public class AAModReforgeSouls : GlobalItem
	{
		public override bool CanRightClick(Item item)
		{
            if (Main.mouseItem.type == mod.ItemType("Godly") && item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
            {
				return true;
			}
			if (Main.mouseItem.type == mod.ItemType("Legendary") && item.damage > 3 && item.melee && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				return true;
			}
			if (Main.mouseItem.type == mod.ItemType("Unreal") && item.damage > 3 && (item.ranged || item.thrown) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				return true;
			}
			if (Main.mouseItem.type == mod.ItemType("Mythical") && item.damage > 3 && (item.summon || item.magic) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				return true;
			}
			return base.CanRightClick(item);
		}

		public override void RightClick(Item item, Player player)
		{
            if (Main.mouseItem.type == mod.ItemType("Godly") && item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
            {
				Main.mouseItem.stack = 0;
			}
			if (Main.mouseItem.type == mod.ItemType("Legendary") && item.damage > 3 && item.melee && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack = 0;
			}
			if (Main.mouseItem.type == mod.ItemType("Unreal") && item.damage > 3 && (item.ranged || item.thrown) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack = 0;
			}
			if (Main.mouseItem.type == mod.ItemType("Mythical") && item.damage > 3 && (item.summon || item.magic) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack = 0;
			}
		}
		
		public override bool ConsumeItem(Item item, Player player)	
		{
            if (Main.mouseItem.type == mod.ItemType("Godly") && item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
            {
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 59);
			}
			if (Main.mouseItem.type == mod.ItemType("Legendary") && item.damage > 3 && item.melee && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 81);
			}
			if (Main.mouseItem.type == mod.ItemType("Unreal") && item.damage > 3 && (item.ranged || item.thrown) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 82);
			}
			if (Main.mouseItem.type == mod.ItemType("Mythical") && item.damage > 3 && (item.summon || item.magic) && !item.consumable && item.knockBack > 0 && item.maxStack == 1)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height, item.type, 1, false, 83);
			}
			return base.ConsumeItem(item, player);
		}
    }*/
}
