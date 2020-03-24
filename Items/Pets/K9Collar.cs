using Terraria;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class K9Collar : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("K9 Collar");
			Tooltip.SetDefault("Summons a robotic buddy");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("K9");
            item.buffType = mod.BuffType("K9");
            item.noUseGraphic = true;
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
    }
}