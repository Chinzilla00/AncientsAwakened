using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod
{
    public class AAModReforgeSouls : GlobalItem
	{
        public override bool CanRightClick(Item item)
		{
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == mod.ItemType("Godly") && reforgable) ||
                (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee) || 
                (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown) && item.ammo == AmmoID.None) ||
                (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic)))
			{
				return true;
			}
            return base.CanRightClick(item);
		}


		public override void RightClick(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;

            if ((Main.mouseItem.type == mod.ItemType("Godly") && reforgable) ||
                (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee) ||
                (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown) && item.ammo == AmmoID.None) ||
                (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic)))
            { 
                Main.mouseItem.stack = 0;
			}
        }
		
		public override bool ConsumeItem(Item item, Player player)
        {
            bool reforgable = item.damage > 3 && !item.consumable && item.knockBack > 0 && item.maxStack == 1;
            if (Main.mouseItem.type == mod.ItemType("Godly") && reforgable)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 59);
			}
			if (Main.mouseItem.type == mod.ItemType("Legendary") && reforgable && item.melee)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 81);
			}
			if (Main.mouseItem.type == mod.ItemType("Unreal") && reforgable && (item.ranged || item.thrown) && item.ammo == AmmoID.None)
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 82);
			}
			if (Main.mouseItem.type == mod.ItemType("Mythical") && reforgable && (item.summon || item.magic))
			{
				Main.mouseItem.stack--;
				Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, 1, false, 83);
			}
            return base.ConsumeItem(item, player);
		}
    }
}
