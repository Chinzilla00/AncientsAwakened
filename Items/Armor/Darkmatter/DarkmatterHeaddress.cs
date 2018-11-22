using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHeaddress : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Darkmatter Headress");
			Tooltip.SetDefault(@"25% increased minion damage
Dark, yet still barely visible");

		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = 300000;
			item.rare = 11;
			item.defense = 20;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.25f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"Increases max number of minions by 6
Your minions electrocute enemies";
            player.maxMinions += 6;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetSu = true;
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