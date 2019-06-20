using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class TechneciumRepeater : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Repeater");
			Tooltip.SetDefault("33% chance not to consume arrows");
        }

		public override void SetDefaults()
		{
			item.damage = 39;
			item.ranged = true;
			item.width = 52;
			item.height = 20;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 3;
            item.value = 108000;
            item.rare = 4;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 13f;
			item.useAmmo = AmmoID.Arrow;
		}
		
		public override bool ConsumeAmmo(Player player)
		{
		return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 0);
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "TechneciumBar", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
