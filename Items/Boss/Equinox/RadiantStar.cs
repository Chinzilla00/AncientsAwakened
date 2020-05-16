using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Equinox
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class RadiantStar : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
            item.expert = true; item.expertOnly = true;
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
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Star");
            Tooltip.SetDefault(
@"Gives immensely increased stats during the day
'It's Shiny'");
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.dayTime)
            {
                player.lifeRegen += 5;
                player.statDefense += 8;
                player.meleeSpeed += 0.10f;
                player.meleeCrit += 4;
                player.rangedCrit += 4;
                player.magicCrit += 4;
                player.pickSpeed -= 0.30f;
                player.minionKB += 0.7f;
                player.allDamage += 0.17f;
                player.thrownCrit += 4;
            }
            player.GetModPlayer<AAPlayer>().RStar = true;
        }

    }
}