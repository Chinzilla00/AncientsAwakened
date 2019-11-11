using Terraria;
using Terraria.ID;
using Terraria.DataStructures;

namespace AAMod.Items.Pets
{
    public class DragonsSoul : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Soul");
			Tooltip.SetDefault(@"Summons a Dragon Soul
It feels hot, but comforting...");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
        }

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = 1;
			item.shoot = mod.ProjectileType("DragonSoul");
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = 8;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.buffType = mod.BuffType("DragonSoul");
            item.noUseGraphic = true;
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}