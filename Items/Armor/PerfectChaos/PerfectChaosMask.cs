using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Head)]
    public class PerfectChaosMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slayer Mask");
            Tooltip.SetDefault(@"70% increased minion damage
5% increased damage resistance
+6 maximum Minions
+2 maximum sentries 
The power of discordian rage radiates from this hood");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 27;
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
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.PerfectChaosMaskBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosSu = true;
            player.AddBuff(mod.BuffType("ChaosWrath"), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += .7f;
            player.endurance += .05f;
            player.maxMinions += 6;
            player.maxTurrets += 2;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayMask", 1);
            recipe.AddIngredient(null, "Discordium", 6);
            recipe.AddIngredient(null, "ChaosScale", 6);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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