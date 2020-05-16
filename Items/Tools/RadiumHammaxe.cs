using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class RadiumHammaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 70;
            item.melee = true;
            item.width = 44;
            item.height = 40;
            item.useTime = 10;
            item.useAnimation = 20;
            item.axe = 50;
            item.hammer = 45;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
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
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Hammaxe");
        }

        public override void AddRecipes()  
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.AddIngredient(null, "RadiumBar", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");   
            recipe.SetResult(this);  
            recipe.AddRecipe();
        }
    }
}
