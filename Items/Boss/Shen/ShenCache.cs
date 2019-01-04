using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Shen
{
    public class ShenCache : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Treasure Cache");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.expert = true;
			bossBagNPC = mod.NPCType("ShenA");
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

        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.NextFloat() < 0.01f)
            {
                int choice = Main.rand.Next(17);
                {
                    if (choice == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("HalHat"));
                        player.QuickSpawnItem(mod.ItemType("HalTux"));
                        player.QuickSpawnItem(mod.ItemType("HalTrousers"));
                        player.QuickSpawnItem(mod.ItemType("Prismeow"));
                    }
                    else if (choice == 1)
                    {
                        player.QuickSpawnItem(mod.ItemType("FishDiverMask"));
                        player.QuickSpawnItem(mod.ItemType("FishDiverJacket"));
                        player.QuickSpawnItem(mod.ItemType("FishDiverBoots"));
                        player.QuickSpawnItem(mod.ItemType("KipronWings"));
                        player.QuickSpawnItem(mod.ItemType("AmphibianLongsword"));
                    }
                    else if (choice == 2)
                    {
                        player.QuickSpawnItem(mod.ItemType("N1"));
                        player.QuickSpawnItem(mod.ItemType("Sax"));
                    }
                    if (choice == 3)
                    {
                        player.QuickSpawnItem(mod.ItemType("GlitchesHat"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesBreastplate"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesGreaves"));
                        player.QuickSpawnItem(mod.ItemType("UmbreonSP"));
                    }
                    if (choice == 4)
                    {
                        player.QuickSpawnItem(mod.ItemType("GavransGoggles"));
                        player.QuickSpawnItem(mod.ItemType("GavransChest"));
                        player.QuickSpawnItem(mod.ItemType("GavransChest"));
                    }
                    if (choice == 5)
                    {
                        player.QuickSpawnItem(mod.ItemType("ChinMask"));
                        player.QuickSpawnItem(mod.ItemType("ChinSuit"));
                        player.QuickSpawnItem(mod.ItemType("ChinPants"));
                        player.QuickSpawnItem(mod.ItemType("ChinsMagicCoin"));
                        player.QuickSpawnItem(mod.ItemType("ChinStaff"));
                    }
                    if (choice == 6)
                    {
                        player.QuickSpawnItem(mod.ItemType("SkrallStaff"));
                    }
                    if (choice == 7)
                    {
                        player.QuickSpawnItem(mod.ItemType("CharlieShell"));
                    }
                    if (choice == 8)
                    {
                        player.QuickSpawnItem(mod.ItemType("TimeTeller"));
                    }
                    if (choice == 9)
                    {
                        player.QuickSpawnItem(mod.ItemType("TitanAxe"));
                    }
                    if (choice == 10)
                    {
                        player.QuickSpawnItem(mod.ItemType("EnderStaff"));
                    }
                    if (choice == 11)
                    {
                        player.QuickSpawnItem(mod.ItemType("CatsEyeRifle"));
                    }
                    if (choice == 12)
                    {
                        player.QuickSpawnItem(mod.ItemType("DuckstepGun"));
                    }
                    if (choice == 13)
                    {
                        player.QuickSpawnItem(mod.ItemType("TiedHat"));
                        player.QuickSpawnItem(mod.ItemType("TiedHalTux"));
                        player.QuickSpawnItem(mod.ItemType("TiedTrousers"));
                        player.QuickSpawnItem(mod.ItemType("GentlemansRapier"));
                    }
                    if (choice == 14)
                    {
                        player.QuickSpawnItem(mod.ItemType("Etheral"));
                        player.QuickSpawnItem(mod.ItemType("MoonHood"));
                        player.QuickSpawnItem(mod.ItemType("MoonRobe"));
                        player.QuickSpawnItem(mod.ItemType("MoonBoots"));
                    }
                }
            }
            player.QuickSpawnItem(mod.ItemType("ChaosScale"), Main.rand.Next(30, 40));
            player.QuickSpawnItem(mod.ItemType("ChaosSoul"));
            player.QuickSpawnItem(mod.ItemType("EXSoul"));
            string[] lootTable = 
            {
                "ChaosSlayer", "MeteorStrike", "Skyfall"
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
	}
}