using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    [AutoloadEquip(EquipType.Back)]
    public class AnkhTablet : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
            item.shieldSlot = -1;
            AutoDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.meleeSpeed -= 0.07f;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Tablet");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
+2 max minions");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AnkhCharm);
            recipe.AddIngredient(ItemID.SolarTablet);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
