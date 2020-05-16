using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAMod.Items.Boss.Equinox
{
    public class EquinoxBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault(@"{$CommonItemTooltip.RightClickToOpen}
Contained loot depends on the time of day");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 32;
			item.height = 36;
			item.rare = ItemRarityID.Purple;
			item.expert = true; item.expertOnly = true;
        }
        public override int BossBagNPC => mod.NPCType("DaybringerHead");

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Items/Boss/Equinox/DBBag");
            Texture2D texture2 = mod.GetTexture("Items/Boss/Equinox/NCBag");
            if (Main.dayTime)
            {
                spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Items/Boss/Equinox/DBBag");
            Texture2D textureGlow = mod.GetTexture("Glowmasks/DBBag_Glow");
            Texture2D texture2 = mod.GetTexture("Items/Boss/Equinox/NCBag");
            Texture2D texture2Glow = mod.GetTexture("Glowmasks/NCBag_Glow");
            if (Main.dayTime)
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

                return false;
            }
            else
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
                spriteBatch.Draw
                (
                    texture2Glow,
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

                return false;
            }
        }

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (!Main.dayTime)
            {
                if (Main.rand.Next(7) == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("NCMask"));
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(mod.ItemType("DarkEnergy"), Main.rand.Next(40, 90));
                player.QuickSpawnItem(mod.ItemType("DarkVoid"));
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    player.QuickSpawnItem(mod.ItemType("DBMask"));
                }
                if (Main.rand.Next(20) == 0)
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
                    modPlayer.PMLDevArmor();
                }
                player.QuickSpawnItem(mod.ItemType("Stardust"), Main.rand.Next(40, 90));
                player.QuickSpawnItem(mod.ItemType("RadiantStar"));
            }
            if (AAWorld.RadiumOre)
            {
                player.QuickSpawnItem(mod.ItemType("StarIdol"));
            }
        }
	}
}