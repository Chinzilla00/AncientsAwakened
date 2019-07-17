using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Starcloud
{
    [AutoloadEquip(EquipType.Body)]
    public class StarcloudChainmail : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 18;
            item.value = 10;
            item.rare = 3;
            item.defense = 6;
        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Starcloud Chainmail");
      Tooltip.SetDefault("+6% minion damage, +20 max mana");
    }


        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.06f;  //20 max mana
			player.statManaMax2 += 20;
        }
        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StarcloudBar", 20);   //you need 10 Wood
            recipe.AddTile(TileID.Anvils);   
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
