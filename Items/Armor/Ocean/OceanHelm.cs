using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Head)]
	public class OceanHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ocean Helmet");
            Tooltip.SetDefault(@"Increases maximum mana by 20
2% increased magic damage");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 3;
            item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.magicDamage += 0.02f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == mod.ItemType("OceanHelm") && legs.type == mod.ItemType("OceanBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"20% increased magic damage and 15% mana use reduction while submerged in water";
            if (player.wet && !player.lavaWet && !player.honeyWet)
            {
                player.magicDamage += 0.2f;
                player.manaCost *= 0.85f;
            }
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}