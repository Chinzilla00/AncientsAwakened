using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Head)]
    public class PerfectChaosVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slayer Visor");
            Tooltip.SetDefault(@"45% increased ranged damage
38% increased ranged critical strike chance
12% increased damage resistance
25% reduced ammo consumption
The power of discordian rage radiates from this hood");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 39;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("PerfectChaosPlate") && legs.type == mod.ItemType("PerfectChaosGreaves");
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'Chaos empowers you'
As your health decreases, your ranged damage and critical chance increases
Your ranged attacks raze your oponents with the flames of discordian hell";
            player.GetModPlayer<AAPlayer>(mod).perfectChaosRa = true;
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                player.rangedDamage += .4f;
                player.rangedCrit += 7;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                player.rangedDamage += .3f;
                player.rangedCrit += 14;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                player.rangedDamage += .2f;
                player.rangedCrit += 21;
            }
            else if (player.statLife <= player.statLifeMax2 * .8f)
            {
                player.rangedDamage += .1f;
                player.rangedCrit += 28;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.45f;
            player.rangedCrit += 38;
            player.endurance *= 1.1f;
            player.ammoCost75 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DreadHelm", 1);
            recipe.AddIngredient(null, "Discordium", 6);
            recipe.AddIngredient(null, "ChaosScale", 6);
            recipe.AddTile(null, "AncientForge");
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