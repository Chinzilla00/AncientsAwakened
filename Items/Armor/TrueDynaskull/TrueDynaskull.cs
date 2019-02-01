using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueDynaskull
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueDynaskull : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Dynaskull");
			Tooltip.SetDefault(@"100% increased throwing velocity");

		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 100000;
			item.rare = 7;
			item.defense = 16;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.thrownVelocity *= 2f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TrueDynaskullRibguard") && legs.type == mod.ItemType("TrueDynaskullGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"Your thrown projectiles have so much power behind them, they confuse the target due to concussive force
83% chance to not consume thrown weapons
Pressing the Ability hotkey fires off a dynaskull shot towards your cursor";

            player.thrownCost50 = true;
            player.thrownCost33 = true;
            player.GetModPlayer<AAPlayer>(mod).DynaskullSet = true;
            player.GetModPlayer<AAPlayer>(mod).trueDynaskull = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Dynaskull", 1);
            recipe.AddIngredient(null, "DesertCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}