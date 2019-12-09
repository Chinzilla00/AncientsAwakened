using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Potions
{
    public class RoninPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ronin Potion");
			Tooltip.SetDefault("When you don't get hurt, you will have 3s immune time");
		}
		
		public override void SetDefaults()
        {
            item.width = 20;
			item.height = 34;
			item.useTurn = true;
			item.maxStack = 30;
			item.healLife = 50;
            item.useAnimation = 17;
			item.useTime = 17;
			item.useStyle = 2;
			item.UseSound = SoundID.Item3;
			item.consumable = true;
			item.potion = true;
			item.value = 50000;
            item.rare = 4;
		}

        public override bool UseItem(Player player)
        {
            if(player.statLife == player.statLifeMax2) player.AddBuff(mod.BuffType("Ronin"), 180);
            return base.UseItem(player);
        }
	}
}