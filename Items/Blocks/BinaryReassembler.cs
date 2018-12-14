using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class BinaryReassembler : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binary Fragmentation Reassembler");
            Tooltip.SetDefault("Reality has never been so easy to manipulate");
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
            
        }
        
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
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
