using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcrystal
{
    [AutoloadEquip(EquipType.Legs)]
    public class StarcrystalBoots : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;


            item.value = 10;
            item.rare = 2;
            item.defense = 4;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcrystal Leggings");
      Tooltip.SetDefault(@"+20 Mana");
    }


        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;  //player movement speed incresed 0.05f = 5%
        }


        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddRecipeGroup("AAMod:Gold", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
