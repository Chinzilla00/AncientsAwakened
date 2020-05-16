using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class Voidsaber : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 48;
			item.useAnimation = 25;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.rare = ItemRarityID.Blue;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.damage = 9;
			item.knockBack = 4f;
			item.autoReuse = false;
			item.noMelee = true;
			item.melee = true;
			item.shoot = ModContent.ProjectileType<Projectiles.Voidslash>();
			item.shootSpeed = 15f;
			item.value = 5400;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voidsaber");
			Tooltip.SetDefault("");
		}
	}
}
