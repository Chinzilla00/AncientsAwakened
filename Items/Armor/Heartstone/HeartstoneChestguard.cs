using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Heartstone
{
    [AutoloadEquip(EquipType.Body)]
    public class HeartstoneChestguard : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;


            item.value = 2000;
            item.rare = 3;
            item.defense = 7;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Heartstone Chestguard");
      Tooltip.SetDefault(@"+10 Health
Its forged with heart, no really");
    }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
        }
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 2);
            recipe.AddRecipeGroup("AAMod:Gold", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
