using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.Items.Boss.Broodmother
{
    public class BroodBag : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.expert = true;
        }
        public override int BossBagNPC => mod.NPCType("Broodmother");

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

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.Next(7) == 0)
            {
                //player.QuickSpawnItem(mod.ItemType("ZeroMask"));
            }
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("BroodEgg"));
            }
            if (Main.rand.Next(20) == 0)
            {

                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PHMDevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("Incinerite"), Main.rand.Next(75, 125));
            player.QuickSpawnItem(mod.ItemType("BroodScale"), Main.rand.Next(50, 100));
            player.QuickSpawnItem(mod.ItemType("DragonCape"));
        }
	}
}