using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Heartstone
{
    [AutoloadEquip(EquipType.Body)]
    public class HeartstoneChestguard : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;


            item.value = 10;
            item.rare = 4;
            item.defense = 11;
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
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddIngredient(ItemID.StoneBlock, 75);   //you need 10 Wood
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
