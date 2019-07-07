using Terraria;
using Terraria.ID;

namespace AAMod.Items.Potions
{
    public class Carrot : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carrot");
			Tooltip.SetDefault("Good for your eyesight");
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
		}

        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.NightOwl, 52000);
            return base.UseItem(player);
        }
	}
}
