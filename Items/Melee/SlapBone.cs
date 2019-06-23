using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class SlapBone : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.SlapHand);
			item.damage = 56;
			item.useTime = 15;
			item.useAnimation = 15;     
			item.knockBack = 50;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = 5;            
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slap Bone");
			Tooltip.SetDefault("The easiest hit will send enemy into space");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(ItemID.SlapHand);
			recipe.AddIngredient(mod.ItemType("EXSoul"));
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
