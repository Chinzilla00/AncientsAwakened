using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Plantera
{
    [AutoloadEquip(EquipType.Head)]
	public class BeastHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+6% thrown damage");
			DisplayName.SetDefault("Beast Helmet");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.buyPrice(0, 1, 50, 0);
            item.rare = 7;
			item.defense = 16;

        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("BeastBody") && legs.type == mod.ItemType("BeastLegs");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.lifeRegen = 1;
			player.moveSpeed += 0.1f;
			player.setBonus = "+10% movement speed"
				+ "\nIncreased life regeneration";
		}
		
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.06f; //6% throwing damage
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "PlanteraPetal", 12); //24        10, 8, 6
            recipe.AddIngredient(ItemID.ChlorophyteBar, 6);//30      14, 10, 6
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}