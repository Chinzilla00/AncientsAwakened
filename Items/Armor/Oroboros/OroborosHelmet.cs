using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Oroboros
{
    [AutoloadEquip(EquipType.Head)]
    public class OroborosHelmet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oroboros Wood Helmet");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.value = 1000;
            item.rare = 3;
            item.defense = 4;
        }
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("OroborosChestplate") && legs.type == mod.ItemType("OroborosBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.OroborosHelmetBonus");
            player.statDefense += 3;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "OroborosWood", 20);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}