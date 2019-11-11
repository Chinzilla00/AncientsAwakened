using Terraria;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class GlowmossBall : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Moss Ball");
			Tooltip.SetDefault(@"Summons a glowmoss ball
Don�t ask what makes it glow, Trust me");
		}

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = 1;
			item.shoot = mod.ProjectileType("Glowmoss");
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = 8;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 5, 50, 0);
			item.buffType = mod.BuffType("Glowmoss");
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