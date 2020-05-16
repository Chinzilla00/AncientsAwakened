using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Body)]
	public class DoomsdayChestplate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Doomsday Assault Armor");
			Tooltip.SetDefault(@"22% decreased mana usage
+50 Max Life
The power to destroy entire planets rests in this armor");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 3000000;
			item.defense = 38;
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
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

        public override void UpdateEquip(Player player)
		{
			player.manaCost *= .78f;
            player.statLifeMax2 += 50;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "ApocalyptitePlate", 20);
			recipe.AddIngredient(null, "UnstableSingularity", 5);
			recipe.AddTile(null, "ACS");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}