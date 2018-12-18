using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.Infinity
{
    [AutoloadEquip(EquipType.Head)]
	public class InfinityVisor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infinity Slayer Visor");
			Tooltip.SetDefault(@"35% increased ranged damage and critical strike chance
12% increased damage resistance
25% decreased ammo consumption
Infinite power and malice flows through this armor");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 40;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.rangedDamage *= 1.35f;
            player.endurance *= 1.12f;
            player.ammoCost75 = true;
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("InfinityPlate") && legs.type == mod.ItemType("InfinityGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"'Infinite power enrages you'
You can see all life around you
You can see all potential threats around you
Your attacks scorch your enemies with the fires of infinity";
            
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.Dangersense, 2);
            player.GetModPlayer<AAPlayer>(mod).infinitySet = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayHelmet", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
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