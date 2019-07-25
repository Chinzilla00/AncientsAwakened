using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosSkull : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Dragonskull");
            Tooltip.SetDefault(@"24% increased Ranged damage");
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
            player.rangedDamage += .24f;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Lang.ArmorBonus("ChaosSkullBonus");

            player.rangedCrit += 20;
            player.ammoCost75 = true;
            player.ammoCost80 = true;
            player.GetModPlayer<AAPlayer>(mod).ChaosRa2 = true;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TrueDynaskull", 1);
            recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}