using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortsword : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Copper Shortsword");
			Tooltip.SetDefault("Literally just did it for the memes");
        }
		public override void SetDefaults()
		{
            
			item.damage = 500;
			item.melee = true;
			item.width = 36;
			item.height = 36;
			item.useTime = 28;
			item.useAnimation = 40;
			item.useStyle = 3;
			item.knockBack =20;
			item.value = 10000000;
			item.rare = 9;
			item.expert = true;
			item.UseSound = SoundID.Item37;
			item.autoReuse = false;
            item.shoot = mod.ProjectileType("TrueCopperShot");
            item.shootSpeed = 20f;
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
			recipe.AddIngredient(ItemID.CopperShortsword, 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
