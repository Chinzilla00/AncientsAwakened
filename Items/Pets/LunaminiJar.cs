using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class LunaminiJar : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Moon Bee in a Jar");
			Tooltip.SetDefault("Summons a Lunamini");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(2, 2));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("Lunamini");
            item.buffType = mod.BuffType("Lunamini");
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