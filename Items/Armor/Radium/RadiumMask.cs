using AAMod.Items.Armor.Darkmatter;
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
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.GetModPlayer<DarkmatterMaskEffects>().sunSiphon = true;
            player.setBonus = "Damage nearby enemies \n 50% of the damage dealt will restore mana\n If mana is full you'll get mana overload buff instead\n" + (int)(100 * player.magicDamage) + " Magic Damage\n" + (player.magicCrit) + "% critical strike chance";

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