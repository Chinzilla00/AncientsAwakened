using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Dyes
{
    public class BlazingDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Dye");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "Gives a blazing touch to whatever this dye is applied to" });
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 15;
            item.maxStack = 99;
            item.rare = 8;
            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
            item.value = BaseUtility.CalcValue(0, 10, 0, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CrucibleScale", 3);
            recipe.AddIngredient(Terraria.ID.ItemID.BottledWater);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}