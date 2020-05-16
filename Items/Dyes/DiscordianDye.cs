using System.Collections.Generic;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Dyes
{
    public class DiscordianDye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Dye");
            BaseUtility.AddTooltips(item, new string[] { "Gives a discordian touch to whatever this dye is applied to" });
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
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
            recipe.AddIngredient(null, "BlazingDye", 1);
            recipe.AddIngredient(null, "AbyssalDye", 1);
            recipe.AddTile(Terraria.ID.TileID.DyeVat);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }
    }
}