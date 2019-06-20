using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcrystal
{
    [AutoloadEquip(EquipType.Body)]
    public class StarcrystalPlate : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;
            item.value = 10;
            item.rare = 3;
            item.defense = 5;
        }
        
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcrystal Chestguard");
      Tooltip.SetDefault(@"+20 Mana");
    }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
        }
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddRecipeGroup("AAMod:Gold", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
