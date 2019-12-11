using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class CerberusWhistle : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hades' Whistle");
			Tooltip.SetDefault("Summons the guard dog of the king of the underworld himself");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("Cerberus");
            item.buffType = mod.BuffType("Cerberus");
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