using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Materials
{
    public class RadiantIncinerite : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 24;
            item.maxStack = 99;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Green;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RadiantIncinerite");
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Incinerite Bar");
            Tooltip.SetDefault("You can barely look at it, it's so bright");
        }

		public override void AddRecipes()
        {                                                   
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(null, "IncineriteBar", 1);              //example of how to craft with a modded item
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
    }
}
