using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			item.shoot = ModContent.ProjectileType<Cerberus>();
            item.buffType = ModContent.BuffType<Buffs.Cerberus>();
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