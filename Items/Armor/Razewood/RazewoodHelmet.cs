using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Razewood
{
    [AutoloadEquip(EquipType.Head)]
    public class RazewoodHelmet : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razewood Helmet");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.value = 1000;
            item.rare = ItemRarityID.White;
            item.defense = 1;
        }
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RazewoodChestplate") && legs.type == mod.ItemType("RazewoodBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.RazewoodHelmet");
            player.statDefense += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Razewood", 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}