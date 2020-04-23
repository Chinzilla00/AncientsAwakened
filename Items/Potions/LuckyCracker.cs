using Terraria;
using Terraria.ID;

namespace AAMod.Items.Potions
{
    public class LuckyCracker : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lucky Cracker");
			Tooltip.SetDefault("She said it can make you lucky. Do you trust her?");
		}
		
		public override void SetDefaults()
		{
            item.UseSound = SoundID.Item2;
            item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.maxStack = 30;
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = 7;
			item.buffType = BuffID.WellFed;
			item.buffTime = 52000;
			item.buffTime = 18000;
		}

		public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("CrasyLucky"), 3600);
            return base.UseItem(player);
        }
	}
}
