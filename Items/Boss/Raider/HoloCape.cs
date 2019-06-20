using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Raider
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class HoloCape : BaseAAItem
    {

        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holographic Cloak");
            Tooltip.SetDefault(
@"15% Increased Damage Resistance");
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 2;
            item.accessory = true;
            item.expert = true;
            item.defense = 5;
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
            player.endurance *= 1.15f;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<DragonSerpentNecklace>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<StormCharm>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<Broodmother.DragonCape>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    
}