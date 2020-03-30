using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Bogwood
{
    [AutoloadEquip(EquipType.Head)]
    public class BogwoodHelmet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bogwood Helmet");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.value = 1000;
            item.rare = 0;
            item.defense = 1;
        }
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("BogwoodChestplate") && legs.type == mod.ItemType("BogwoodBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+1 Defense";
            player.statDefense += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Bogwood", 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}