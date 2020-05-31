using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class RadiumPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 90;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 4;
            item.useAnimation = 12;
            item.pick = 235;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 1;
            item.value = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starminer");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RadiumBar", 20);
            recipe.AddIngredient(mod, "Stardust", 5);
            recipe.AddTile(mod, "QuantumFusionAccelerator");
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
