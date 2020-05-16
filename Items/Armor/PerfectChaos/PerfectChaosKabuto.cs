using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ID;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Head)]
    public class PerfectChaosKabuto : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slayer Kabuto");
            Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
5% increased damage resistance
15% increased melee speed
+25 Max Life
The power of discordian rage radiates from this armor");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            item.defense = 44;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("PerfectChaosPlate") && legs.type == mod.ItemType("PerfectChaosGreaves");
		}
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.PerfectChaosKabutoBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosMe = true;
            player.AddBuff(mod.BuffType("ChaosWrath"), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .3f;
            player.meleeCrit += 30;
            player.endurance += .05f;
            player.meleeSpeed += .15f;
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DracoHelm", 1);
            recipe.AddIngredient(null, "Discordium", 6);
            recipe.AddIngredient(null, "ChaosScale", 6);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/PerfectChaosKabuto_Glow");
            spriteBatch.Draw(Glow, position, null, AAColor.Shen3, 0, origin, scale, SpriteEffects.None, 0f);
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