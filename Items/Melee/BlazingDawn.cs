using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BlazingDawn : ModItem
    {
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blazing Dawn");
            Tooltip.SetDefault("The Radiant Dawn calls");
        }
		public override void SetDefaults()
		{
			item.damage = 47;
			item.melee = true;
			item.width = 62;
			item.height = 62;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = 80000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
            
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if(Main.rand.NextFloat() < 1f);
            {
                Dust dust;
                dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType<Dusts.AshRain>(), 0f, 0f, 46, default(Color), 1.381579f)];
                dust.noGravity = true;
            }
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
			recipe.AddIngredient(mod, "FlamingFury", 1);
			recipe.AddIngredient(mod, "OceanRazor", 1);
			recipe.AddIngredient(mod, "DesertScimitar", 1);
			recipe.AddIngredient(mod, "IceLongsword", 1);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
