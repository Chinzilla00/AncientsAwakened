using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcloud
{
    [AutoloadEquip(EquipType.Legs)]
    public class StarcloudLeggings : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;

            item.value = 10;
            item.rare = 3;
            item.defense = 4;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcloud Leggings");
      Tooltip.SetDefault("6% increased magic damage +20 max mana");
    }


        public override void UpdateEquip(Player player)
        { 
			player.magicDamage += 0.06f;
            player.statManaMax2 += 20;
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StarcloudBar", 15);   //you need 10 Wood
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
