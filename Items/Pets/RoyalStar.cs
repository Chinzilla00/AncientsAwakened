using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class RoyalStar : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Royal Star");

			Tooltip.SetDefault("Summons a very pretty kitty");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("RoyalKitten");
            item.buffType = mod.BuffType("RoyalKitten");
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