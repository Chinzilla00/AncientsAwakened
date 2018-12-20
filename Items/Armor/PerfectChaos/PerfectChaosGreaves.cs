using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
	[AutoloadEquip(EquipType.Legs)]
	public class PerfectChaosGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Slayer Greaves");
            Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
15% increased damage resistance
15% increased melee speed
45% increased movement speed
The power of discordian rage radiates from this armor");
        }

		public override void SetDefaults()
		{
            item.width = 22;
            item.height = 16;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 35;
            item.rare = 11;
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
            recipe.AddIngredient(null, "DracoLeggings", 1);
            recipe.AddIngredient(null, "DreadBoots", 1);
            recipe.AddIngredient(null, "Discordium", 4);
            recipe.AddIngredient(null, "ChaosScale", 4);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
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