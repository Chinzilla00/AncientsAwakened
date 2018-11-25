using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Blocks
{
    public class TruePaladinsSmeltery : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("True Paladin's Smeltery");
            Tooltip.SetDefault(
@"A superforge meant for only the worthiest of smiths
Functions as most nececary crafting stations");
        }

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = mod.TileType("TruePaladinsSmeltery");
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

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "PaladinsSmeltery", 1);
                recipe.AddIngredient(null, "HaphestusForge", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
