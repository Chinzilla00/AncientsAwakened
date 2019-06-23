using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
	public class SGBox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sleeping Giant Music Box");

            Tooltip.SetDefault(@"Plays 'Sleeping Giant' by LordCakeSpy");
        }

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("SGBox");
            item.width = 72;
			item.height = 36;
            item.rare = 9;
            AARarity = 14;
            item.value = 10000;
			item.accessory = true;
            item.rare = 11;
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/IZBox_Glow1");
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
            recipe.AddIngredient(null, "VoidBox");
            recipe.AddIngredient(null, "UnstableSingularity", 10);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
