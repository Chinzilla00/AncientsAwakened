using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Dyes
{
    public class BlazingFuryDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Fury Dye");
            BaseUtility.AddTooltips(item, new string[] { "Gives a blazing touch to whatever this dye is applied to" });
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.AkumaA;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 15;
            item.maxStack = 99;
            item.rare = ItemRarityID.Yellow;
            item.dye = (byte)GameShaders.Armor.GetShaderIdFromItemId(item.type);
            item.value = BaseUtility.CalcValue(0, 10, 0, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "BlazingDye", 2);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}