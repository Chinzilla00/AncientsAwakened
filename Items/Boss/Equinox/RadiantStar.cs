using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Equinox
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class RadiantStar : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.expert = true;
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

        public override void UpdateEquip(Player player)
        {
            if (Main.dayTime)
            {
                player.lifeRegen += 5;
                player.statDefense += 8;
                player.meleeSpeed += 0.3f;
                player.meleeDamage += 0.3f;
                player.meleeCrit += 4;
                player.rangedDamage += 0.3f;
                player.rangedCrit += 4;
                player.magicDamage += 0.3f;
                player.magicCrit += 4;
                player.pickSpeed -= 0.30f;
                player.minionDamage += 0.3f;
                player.minionKB += 0.7f;
                player.thrownDamage += 0.3f;
                player.thrownCrit += 4;
            }
        }

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Star");
            Tooltip.SetDefault(
@"Gives immensely increased stats during the day
'It's Shiny'");
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>(mod).RStar = true;
        }

    }
}