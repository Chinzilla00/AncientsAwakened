using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueCopperShortswordEX : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultima Shortsword");
			Tooltip.SetDefault("Copper Shortsword EX");
        }
		public override void SetDefaults()
		{
            
			item.damage = 1000;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 3;
			item.knockBack =20;
			item.value = 10000000;
			item.rare = 9;
			item.expert = true;
			item.UseSound = SoundID.Item37;
			item.autoReuse = true;
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
			recipe.AddIngredient(null, "TrueCopperShortsword", 1);
			recipe.AddIngredient(null, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
