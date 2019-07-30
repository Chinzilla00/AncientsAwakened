using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Materials
{
    public class RoyalRabbit : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Rabbit");
            Tooltip.SetDefault("Under direct protection by the Pouncing Punisher");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 30;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 8;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            int num = NPC.NewNPC((int)(player.position.X + Main.rand.Next(-20, 20)), (int)(player.position.Y - 0f), mod.NPCType("RoyalRabbit"));
            if (Main.netMode == 2 && num < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
            return true;
        }

        public override void PostUpdate()
        {
            if (item.lavaWet)
            {
                Player player = Main.player[Player.FindClosest(item.Center, item.width, item.height)];
                for (int i = 0; i < Main.maxPlayers; ++i)
                {
                    if (player.active && !player.dead)
                    {
                        int bunnyKills = NPC.killCount[Item.NPCtoBanner(NPCID.Bunny)];
                        if (bunnyKills % 100 == 0 && bunnyKills < 1000)
                        {
                            if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossSummonsInfo("RoyalRabbit1"), 107, 137, 179);
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), "Rajah Rabbit");

                        }
                        if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                        {
                            if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Lang.BossSummonsInfo("RoyalRabbit2") + player.name.ToUpper() + "!", 107, 137, 179);
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), player.Center);
                            AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), "Rajah Rabbit");
                        };
                    }
                }
            }
        }
    }
}
