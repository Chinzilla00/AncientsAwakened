using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Head)]
    public class PerfectChaosKabuto : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Slayer Kabuto");
            Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
15% increased damage resistance
15% increased melee speed
The power of discordian rage radiates from this armor");
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(3, 0, 0, 0);
            item.rare = 9;
            AARarity = 14;
            item.defense = 44;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("PerfectChaosPlate") && legs.type == mod.ItemType("PerfectChaosGreaves");
		}

        private bool I1 = false;
        private bool I2 = false;
        private bool I3 = false;
        private bool I4 = false;

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"'Chaos empowers you'
As your health decreases, your melee damage and defense increase
Your attacks raze your oponents with the flames of Chaos";
            player.GetModPlayer<AAPlayer>(mod).perfectChaosMe = true;
            if (player.statLife <= player.statLifeMax2 * .8f)
            {
                I1 = false;
                player.endurance *= 1.05f;
                player.meleeDamage *= 1.1f;
            }
            else if (player.statLife <= player.statLifeMax2 * .6f)
            {
                I1 = false;
                player.endurance *= 1.1f;
                player.meleeDamage *= 1.2f;
            }
            else if (player.statLife <= player.statLifeMax2 * .4f)
            {
                I3 = false;
                player.endurance *= 1.12f;
                player.meleeDamage *= 1.3f;
            }
            else if (player.statLife <= player.statLifeMax2 * .2f)
            {
                I4 = false;
                player.endurance *= 1.15f;
                player.meleeDamage *= 1.4f;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 1.3f;
            player.meleeCrit += 30;
            player.endurance *= 1.15f;
            player.meleeSpeed *= 1.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DracoHelm", 1);
            recipe.AddIngredient(null, "Discordium", 6);
            recipe.AddIngredient(null, "ChaosScale", 6);
            recipe.AddTile(null, "AncientForge");
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