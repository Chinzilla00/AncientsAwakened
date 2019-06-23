using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.GlowingMushium
{
    [AutoloadEquip(EquipType.Head)]
	public class ShroomHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glowing Mushroom Hat");
            Tooltip.SetDefault("2% increased mana regeneration");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
			item.value = 90;
			item.rare = 1;
			item.defense = 2;
            item.value = Item.sellPrice(0, 1, 0, 0);
		}
		
		public override void UpdateEquip(Player player)
        {
            player.manaRegenBonus += 2;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("ShroomShirt") && legs.type == mod.ItemType("ShroomPants");
		}

		public override void UpdateArmorSet(Player player)
		{

            player.setBonus = @"You are immune to Mana Sickness";

            player.buffImmune[BuffID.ManaSickness] = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GlowingMushiumBar", 5);
            recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}