using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class AncientArcanum : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Arcanum");
			Tooltip.SetDefault("Releases a homing miniature quazar that explodes upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			item.mana = 35;
			item.damage = 195;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 9f;
			item.shoot = mod.ProjectileType("AncientArcanum");
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useAnimation = 30;
			item.useTime = 30;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 8f;
			item.rare = ItemRarityID.Purple;
			item.value = Item.sellPrice(0, 30, 0, 0);
			item.magic = true;
			item.glowMask = 194;
			item.noUseGraphic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
