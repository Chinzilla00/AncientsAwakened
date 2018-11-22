using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkmatter Helmet");
			Tooltip.SetDefault(@"10% increased melee damage
Dark, yet still barely visible");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 20;
			item.value = 300000;
			item.rare = 11;
			item.defense = 34;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.10f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"15% increased melee critical chance and speed
Your melee weapons electrocute enemies";
            
            player.meleeSpeed += 0.15f;
            player.meleeCrit += 15;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetMe = true;
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}