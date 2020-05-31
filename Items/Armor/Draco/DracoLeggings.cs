using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Draco
{
    [AutoloadEquip(EquipType.Legs)]
	public class DracoLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconian Sun Greaves");
			Tooltip.SetDefault(@"16% increased movement speed
15% increased melee speed
3% increased damage resistance
+25 Max Life
The blazing fury of the Inferno rests in this armor");

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
			item.defense = 32;
            item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.16f;
			player.meleeSpeed += 0.15f;
			player.endurance += .03f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .16f;
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 18);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "KindledSuneate", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}