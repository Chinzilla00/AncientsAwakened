using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class ZeroTerratool : ModItem
    {
        
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }
        public override void SetDefaults()
        {

            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 20;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 200;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        private int toolType = 0;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 2;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                toolType++;
                if (toolType > 2) toolType = 0;
                switch (toolType)
                {
                    default: break;
                    case 0: item.pick = 250; break;
                    case 1: item.axe = 250; break;
                    case 2: item.hammer = 250; break;
                }
            }
            else
            {
                item.useStyle = 1;
                item.noMelee = false;
                item.noUseGraphic = false;
            }
            return base.CanUseItem(player);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D Pick = mod.GetTexture("Items/Boss/Zero/ZeroTerratool");
            Texture2D Axe = mod.GetTexture("Items/Boss/Zero/ZeroTerratool_Axe");
            Texture2D Ham = mod.GetTexture("Items/Boss/Zero/ZeroTerratool_Hammer");
            if (toolType == 0)
            {
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - Pick.Height * 0.5f + 2f);
                spriteBatch.Draw(Pick, position, null, lightColor, rotation, Pick.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            if (toolType == 1)
            {
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - Axe.Height * 0.5f + 2f);
                spriteBatch.Draw(Axe, position, null, lightColor, rotation, Axe.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            if (toolType == 2)
            {
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - Ham.Height * 0.5f + 2f);
                spriteBatch.Draw(Ham, position, null, lightColor, rotation, Ham.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            // Return true so the original sprite is drawn right after
            return false;
        }

        // Same as above but for drawing inside the player's inventory
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {

            Texture2D Pick = mod.GetTexture("Items/Boss/Zero/ZeroTerratool");
            Texture2D Axe = mod.GetTexture("Items/Boss/Zero/ZeroTerratool_Axe");
            Texture2D Ham = mod.GetTexture("Items/Boss/Zero/ZeroTerratool_Hammer");
            if (toolType == 0)
            {
                spriteBatch.Draw(Pick, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            if (toolType == 1)
            {
                spriteBatch.Draw(Axe, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            if (toolType == 2)
            {
                spriteBatch.Draw(Ham, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}