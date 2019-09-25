using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Abyssal
{
    [AutoloadEquip(EquipType.Head)]
	public class AbyssalFukumen : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Abyssal Fukumen");
            Tooltip.SetDefault(@"35% increased movement speed
12% increased ranged damage
Weightless as shadow itself");
        }

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.value = 10000;
			item.rare = 3;
			item.defense = 5;
		}

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += .12f;
            player.moveSpeed += .35f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("AbyssalGi") && legs.type == mod.ItemType("AbyssalHakama");
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = @"Your ranged attacks are imbued with the poisonous properties of hydra venom
20% decreased ammo consumption
Enemies are less likely to target you";
            player.GetModPlayer<AAPlayer>(mod).depthSet = true;
            player.aggro -= 3;
            player.ammoCost80 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DepthFukumen", 1);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddIngredient(null, "RelicBar", 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}