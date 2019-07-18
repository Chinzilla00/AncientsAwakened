using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumMask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radium Mask");
			Tooltip.SetDefault(@"12% increased magic damage
Shines with the light of a starry night sky");

		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 18;
			item.value = 300000;
			item.rare = 11;
			item.defense = 18;
		}
		
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.12f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Lang.ArmorBonus("RadiumMaskBonus");
            if (Main.dayTime)
            {
                player.moveSpeed += .3f;
            }
            player.GetModPlayer<AAPlayer>(mod).Radium = true;
            player.GetModPlayer<AAPlayer>(mod).radiumMa = true;
            player.statManaMax2 += 200;
            player.manaCost *= 0.80f;
            player.panic = true;
            player.starCloak = true;
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