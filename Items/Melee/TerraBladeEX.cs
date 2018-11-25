using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TerraBladeEX : ModItem
	{
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Terra Blade");//<--- Item name here
			Tooltip.SetDefault(@"Terra Blade EX");
        }
        public override void SetDefaults()
		{
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.useStyle = 1;
			item.damage = 400;
			item.useAnimation = 12;
			item.useTime = 12;
			item.width = 62;
			item.height = 74;
			item.shoot = mod.ProjectileType("TerraShotEX");
			item.shootSpeed = 25f;
			item.knockBack = 7f;
			item.melee = true;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.autoReuse = true;
			item.crit = 8;
			
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

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Terrablaze"), 600);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

