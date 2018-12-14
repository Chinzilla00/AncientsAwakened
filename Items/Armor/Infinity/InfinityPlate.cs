using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;


namespace AAMod.Items.Armor.Infinity
{
    [AutoloadEquip(EquipType.Body)]
	public class InfinityPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Infinity Slayer Armor");
			Tooltip.SetDefault(@"30% increased damage
15% increased damage resistance
30% increased melee speed
Infinite power and malice flows through this armor");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 3000000;
			item.defense = 52;
		}

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.3f;
            player.thrownDamage *= 1.3f;
            player.minionDamage *= 1.3f;
            player.meleeDamage *= 1.3f;
            player.magicDamage *= 1.3f;
            player.endurance *= 1.15f;
            player.meleeSpeed *= 1.15f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.IZ;
                }
            }
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayChestplate", 1);
            recipe.AddIngredient(null, "Infinitium", 15);
            recipe.AddTile(null, "BinaryReassembler");
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
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}