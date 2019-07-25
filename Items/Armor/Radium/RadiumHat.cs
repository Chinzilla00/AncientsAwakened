using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Hat");
			Tooltip.SetDefault(@"35% increased minion damage
Shines with the light of a starry night sky");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = 300000;
			item.rare = 11;
			item.defense = 18;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.35f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Lang.ArmorBonus("RadiumHatBonus");
            if (Main.dayTime)
            {
                player.moveSpeed += .3f;
            }
            player.maxMinions += 7;
            player.panic = true;
            player.starCloak = true;
            player.GetModPlayer<AAPlayer>(mod).Radium = true;
            player.GetModPlayer<AAPlayer>(mod).radiumSu = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}