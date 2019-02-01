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
At night, you move twice as fast and your attacks inflict venom on your targets
From 11:00 PM to 1:00 AM, you move three times as fast and your ranged attacks & minions inflict Moonraze");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.expert = true;
            item.accessory = true;
        }


        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/Boss/Yamata/Naitokurosu");
            Texture2D textureGlow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = mod.GetTexture("Items/Boss/Yamata/Naitokurosu1");
            Texture2D texture3 = mod.GetTexture("Items/Boss/Yamata/NaitokurosuA");
            Texture2D texture3Glow = mod.GetTexture("Glowmasks/NaitokurosuA_Glow");
            if (Main.dayTime)
            {
                spriteBatch.Draw
                (
                    texture2,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            
            else if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
            {
                spriteBatch.Draw
                (
                    texture3,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    texture3Glow,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            else
            {
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    textureGlow,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Main.itemTexture[item.type];
            Texture2D textureGlow = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = mod.GetTexture("Items/Boss/Yamata/Naitokurosu1");
            Texture2D texture3 = mod.GetTexture("Items/Boss/Yamata/NaitokurosuA");
            Texture2D texture3Glow = mod.GetTexture("Glowmasks/NaitokurosuA_Glow");
            if (Main.dayTime)
            {
                spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            else if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
            {
                spriteBatch.Draw(texture3, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture3Glow, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(textureGlow, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
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
            else if (!Main.dayTime && Main.time >= 14400 && Main.time <= 21600)
            {
                player.moveSpeed += 2f;
            }
            else
            {
                player.moveSpeed += 1f;
            }
        }
    }
}