using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class AnkhStone : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
            item.shieldSlot = -1;
            AutoDefaults();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.1f;
            player.meleeSpeed -= 0.07f;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Stone");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
10% reduced mana usage");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AnkhCharm);
            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
