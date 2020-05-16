using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class OreStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Staff");
        }

        public override void SetDefaults()
        {
            item.damage = 160;
            item.magic = true;
            item.mana = 10;
            item.width = 38;
            item.height = 44;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 5;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("OreBomb");
            item.useTime = 25;
            item.useAnimation = 25;
            item.shootSpeed = 12;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldDigger", 1);
            recipe.AddIngredient(null, "CovetiteBar", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
