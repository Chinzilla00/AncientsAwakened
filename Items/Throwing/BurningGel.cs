using Terraria;
using Terraria.ID;

namespace AAMod.Items.Throwing
{
    public class BurningGel : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 32;
			item.ranged = true;
			item.width = 20;
			item.height = 18;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 30;
			item.useAnimation = 30;
			item.shoot = mod.ProjectileType("BurningGelP");
			item.shootSpeed = 9f;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 2;
			item.value = Item.sellPrice(0, 0, 0, 25);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burning Gel");
			Tooltip.SetDefault("Ignites target on hit");
		}
	}
}
