using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
	[AutoloadEquip(EquipType.Body)]
	public class PerfectChaosPlate : ModItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Chaos Slayer Plate");
            Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
15% increased damage resistance
15% increased melee speed
The power of discordian rage radiates from this armor");
        }


        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = Item.sellPrice(3, 0, 0, 0);
			item.rare = 10;
			item.defense = 60;
		}

		public override void UpdateEquip(Player player)
		{

            player.meleeDamage *= 1.3f;
            player.endurance *= 1.15f;
            player.meleeSpeed *= 1.15f;
        }
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DracoPlate", 1);
            recipe.AddIngredient(null, "DreadPlate", 1);
            recipe.AddIngredient(null, "Discordium", 10);
            recipe.AddIngredient(null, "ChaosScale", 10);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/PerfectChaosPlate_Glow");
            spriteBatch.Draw(Glow, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
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
                AAColor.Shen3,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}