using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    [AutoloadEquip(EquipType.Neck)]
    public class Naitokurosu : ModItem
    {

        


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Naitokurosu");
            Tooltip.SetDefault(@"Grants you the abilities of a true master ninja
Allows you to do a speedy dash
At night, you move twice as fast and your attacks inflict venom on your targes
From 11:00 PM to 1:00 AM, you move three times as fast and your ranged & throwing attacks inflict Moonraze");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.expert = true;
            item.accessory = true;
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
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.blackBelt = true;
            player.dash = 1;
            player.spikedBoots = 2;
            player.GetModPlayer<AAPlayer>().Naitokurosu = true;
            player.buffImmune[mod.BuffType("HydraToxin")] = true;
            player.buffImmune[mod.BuffType("Clueless")] = true;
            if (Main.dayTime)
            {
                player.moveSpeed += 0f;
            }
            if (!Main.dayTime && Main.time < 14400 && Main.time > 21600)
            {
                player.moveSpeed += 1f;
            }
            if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
            {
                player.moveSpeed += 2f;
            }
        }
    }
}