using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Blocks
{
    public class AncientForge : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Forge");
            Tooltip.SetDefault(@"A forge imbued with ancient magic");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.expert = true;
            item.createTile = mod.TileType("AncientForge");
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            if (AAWorld.downedAllAncients)
            {
                spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Main.DiscoColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            }
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CrucibleScale", 2);
            recipe.AddIngredient(null, "DreadScale", 2);
            recipe.AddIngredient(null, "UnstableSingularity", 2);
            recipe.AddRecipeGroup("AAMod:ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}