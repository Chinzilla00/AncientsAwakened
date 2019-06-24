using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class RiftShredder : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rift Shredder");
			Tooltip.SetDefault("Shoots void stars that shred through reality itself");
        }

		public override void SetDefaults()
		{
            
			item.damage = 370;
			item.melee = true;
			item.width = 94;
			item.height = 70;
			item.useTime = 15;
            item.shoot = mod.ProjectileType("Rift");
            item.shootSpeed = 14f;
            item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = new LegacySoundStyle(2, 15, Terraria.Audio.SoundType.Sound);
			item.autoReuse = true;
            item.rare = 9; AARarity = 13;
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
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "BreakingDawn");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Terraria.Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType<Dusts.VoidDust>(), 0f, 0f, 46, default(Color), 1.25f);
			dust.noGravity = true;
        }
	}
}
