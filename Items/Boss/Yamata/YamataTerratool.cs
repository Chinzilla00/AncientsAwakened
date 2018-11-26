using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class YamataTerratool : ModItem
    {
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(45, 46, 70);
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

        private int Tooltype = 0;
        private int PickPower = 0;
        private int AxePower = 0;
        private int HammerPower = 0;



        public override bool CanUseItem(Player player)
        {
            if (Tooltype == 0)
            { PickPower = 250; }
            else
            { PickPower = 0; }

            if (Tooltype == 1)
            { AxePower = 250; }
            else
            { AxePower = 0; }

            if (Tooltype == 2)
            { HammerPower = 250; }
            else
            { HammerPower = 0; }

            if (Tooltype > 2)
            { Tooltype = 0; }

            if (player.altFunctionUse == 2)
            {
                item.useStyle = 2;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                Tooltype += 1;
            }
            else
            {
                item.useStyle = 1;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.pick = PickPower;
                item.axe = AxePower;
                item.hammer = HammerPower;
            }
            return base.CanUseItem(player);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D Pick = mod.GetTexture("Items/Boss/Yamata/YamataTerratool");
            Texture2D Axe = mod.GetTexture("Items/Boss/Yamata/YamataTerratool_Axe");
            Texture2D Ham = mod.GetTexture("Items/Boss/Yamata/YamataTerratool_Hammer");
            if (Tooltype == 0)
            {
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - Pick.Height * 0.5f + 2f);
                spriteBatch.Draw(Pick, position, null, lightColor, rotation, Pick.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            if (Tooltype == 1)
            {
                Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - Axe.Height * 0.5f + 2f);
                spriteBatch.Draw(Axe, position, null, lightColor, rotation, Axe.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            if (Tooltype == 2)
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

            Texture2D Pick = mod.GetTexture("Items/Boss/Yamata/YamataTerratool");
            Texture2D Axe = mod.GetTexture("Items/Boss/Yamata/YamataTerratool_Axe");
            Texture2D Ham = mod.GetTexture("Items/Boss/Yamata/YamataTerratool_Hammer");
            if (Tooltype == 0)
            {
                spriteBatch.Draw(Pick, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            if (Tooltype == 1)
            {
                spriteBatch.Draw(Axe, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            if (Tooltype == 2)
            {
                spriteBatch.Draw(Ham, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }

            return false;
        }
    }
}
