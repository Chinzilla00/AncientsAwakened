using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomsdayMask : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomsday Tactical Visor");
            Tooltip.SetDefault(@"50% increased minion damage
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
            item.width = 18;
            item.height = 18;
            item.value = 3000000;
            item.defense = 28;
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
            player.minionDamage += .5f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DoomsdayChestplate") && legs.type == mod.ItemType("DoomsdayLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DoomsdayMaskBonus");

            player.maxMinions += 5;
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.NightOwl, 2);
            player.GetModPlayer<AAPlayer>().zeroSet1 = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 15);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}