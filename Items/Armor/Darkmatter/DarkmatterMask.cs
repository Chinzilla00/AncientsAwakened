using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkmatter Mask");
			Tooltip.SetDefault(@"9% increased magic damage
Dark, yet still barely visible");

		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 20;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.09f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"200 increased maximum mana
20% reduced mana usage
Your Magic spells electrocute enemies";
            player.statManaMax2 += 200;
            player.manaCost *= 0.80f;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetMa = true;
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