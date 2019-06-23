using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
    public class InfernoAltar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Dragon Altar");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("ChaosAltar");
            item.placeStyle = 1;
            item.width = 28;
            item.height = 22;
            item.rare = 3;
            item.value = 1000;
            item.accessory = false;
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 15);
            recipe.AddIngredient(null, "BroodScale", 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

