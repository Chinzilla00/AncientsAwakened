using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Body)]
	public class RadiumPlatemail : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Radium Platemail");
			Tooltip.SetDefault("25% increased damage \n" + "Shines with the light of a starry night sky");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 300000;
			item.defense = 28;
			item.rare = ItemRarityID.Cyan;
			AARarity = 12;
		}

		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = Globals.AAColor.Rarity12;
				}
			}
		}

		public override void UpdateEquip(Player player)
		{
			player.allDamage += .25f;
            Lighting.AddLight(player.Center, 1.0f, 1.0f, 1.0f);
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "RadiumBar", 30);
            recipe.AddIngredient(null, "Stardust", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}