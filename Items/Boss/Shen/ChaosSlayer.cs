using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosSlayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Slayer");
            Tooltip.SetDefault(@"Unleashes blades of chaos to smite your foes
blades go through tiles
'Shatter all sanity'");
        }

        public override void SetDefaults()
        {
            item.width = 85;
            item.height = 85;
            item.value = Item.sellPrice(1, 50, 0, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item103;
            item.damage = 400;
            item.knockBack = 12;
            item.melee = true;
            item.autoReuse = true;
			item.shoot = mod.ProjectileType("ChaosSlayerSword");
			item.shootSpeed = 5;
            item.useTurn = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
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

        public override bool Shoot(Player player, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
        {
			Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX, speedY, projType, damage, knockback, player.whoAmI);
			for (int m = 0; m < 2; m++)
			{
				Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX * 1f, speedY * 1f, m == 0 ? mod.ProjectileType("ChaosSlayerSwordRed") : mod.ProjectileType("ChaosSlayerSwordBlue"), damage, knockback, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ReignOfFire", 1);
            recipe.AddIngredient(null, "Hydraslayer", 1);
            recipe.AddIngredient(null, "ChaosScale", 5);
            recipe.AddIngredient(null, "Discordium", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}