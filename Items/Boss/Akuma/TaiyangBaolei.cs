using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    [AutoloadEquip(EquipType.Shield)]
    public class TaiyangBaolei : ModItem
    {
        private int saveTime;
        private int Defense;

        


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Taiyang Baolei");
            Tooltip.SetDefault(@"Makes you immune to almost all debuffs
allows you to do a strong dash
During the day, you gain 10% damage resistance and your melee & magic attacks set enemies ablaze
From 11:00 AM to 1:00 PM, you gain 20% damage resistance and your melee & magic attacks inflict daybroken");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 32;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.expert = true;
            item.accessory = true;
            item.defense = 8;
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
            player.GetModPlayer<AAPlayer>().Baolei = true;
            player.buffImmune[20] = true;
            player.buffImmune[22] = true;
            player.buffImmune[23] = true;
            player.buffImmune[30] = true;
            player.buffImmune[31] = true;
            player.buffImmune[32] = true;
            player.buffImmune[33] = true;
            player.buffImmune[35] = true;
            player.buffImmune[36] = true;
            player.buffImmune[38] = true;
            player.buffImmune[44] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.buffImmune[67] = true;
            player.buffImmune[69] = true;
            player.buffImmune[70] = true;
            player.buffImmune[120] = true;
            player.buffImmune[144] = true;
            player.buffImmune[153] = true;
            player.buffImmune[156] = true;
            player.buffImmune[195] = true;
            player.buffImmune[196] = true;
            player.buffImmune[197] = true;
            player.buffImmune[203] = true;
            player.buffImmune[mod.BuffType("DragonFire")] = true;
            player.buffImmune[mod.BuffType("BurningAsh")] = true;
            player.noKnockback = true;
            player.dash = 3;
            if (!Main.dayTime)
            {
                player.endurance += 0f;
            }
            if (Main.dayTime && Main.time < 23400 && Main.time > 30600)
            {
                player.endurance += 0.1f;
            }
            if (Main.dayTime && Main.time >= 23400 && Main.time <= 30600)
            {
                player.endurance += 0.2f;
            }
        }
    }
}