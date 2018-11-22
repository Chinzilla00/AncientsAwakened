using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Heartstone
{
    [AutoloadEquip(EquipType.Legs)]
    public class HeartstoneLeggings : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;


            item.value = 10;
            item.rare = 2;
            item.defense = 6;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Heartstone Leggings");
      Tooltip.SetDefault(@"+10 Health
Its forged with heart, no really");
    }


        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;  //player movement speed incresed 0.05f = 5%
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LifeCrystal, 2);
			recipe.AddIngredient(ItemID.FallenStar, 1);
			recipe.AddIngredient(ItemID.StoneBlock, 50);   //you need 10 Wood
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
