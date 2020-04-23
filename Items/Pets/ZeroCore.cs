using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class ZeroCore : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Zero Core");
			Tooltip.SetDefault("Summons a creature-detecting ZER0 lite");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("ZeroBab");
            item.buffType = mod.BuffType("ZeroBab");
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