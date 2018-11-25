using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TrueFleshrendClaymore : ModItem
	{
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Fleshrend Claymore");
			Tooltip.SetDefault(@"Inflics Ichor on your target
Despite the name, it's not actually made of flesh");
        }
		public override void SetDefaults()
		{
            
			item.damage = 115;
			item.melee = true;
			item.width = 75;
			item.height = 71;
			item.useTime = 29;
			item.useAnimation = 29;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = 100000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("TrueFleshClaymoreShot");
            item.shootSpeed = 12f;
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
			recipe.AddIngredient(mod, "FleshrendClaymore", 1);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
	       player.HealEffect(damage / 20);
        }
    }
}
