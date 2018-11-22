using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class BinaryReassembler : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Fragmentation Reassembler");
            Tooltip.SetDefault("Reality has never been so easy to manipulate");
            if (Main.netMode != 2)
            {
                Microsoft.Xna.Framework.Graphics.Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Blocks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 11;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 1000000;
            item.createTile = mod.TileType("BinaryReassembler");
            item.glowMask = customGlowMask;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.mod == "Terraria" && line2.Name == "ItemName")
            {

                line2.overrideColor = new Color(100, 0, 10);

                line2.overrideColor = new Color(120, 0, 30);
//
            }
        }
    }

    public override void AddRecipes()
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "RadiumBar", 20);
                recipe.AddIngredient(null, "DarkMatter", 20);
                recipe.AddIngredient(null, "Apocalyptite", 10);
                recipe.AddTile(null, "TerraCore");
                recipe.SetResult(this);
                recipe.AddRecipe();
            ///recipe.AddRecipeGroup("AAMod:DarkmatterHelmets");
        }
    }
}
