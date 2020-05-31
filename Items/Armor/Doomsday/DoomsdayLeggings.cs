using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Legs)]
	public class DoomsdayLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doomsday Assault Greaves");
			Tooltip.SetDefault(@"18% increased movement speed
120 increased mana
The power to destroy entire planets rests in this armor");

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

        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 18;
			item.value = 3000000;
			item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.18f;
            player.statManaMax2 += 120;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .18f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 18);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}