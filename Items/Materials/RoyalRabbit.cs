using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;


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
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            int num = NPC.NewNPC((int)(player.position.X + Main.rand.Next(-20, 20)), (int)(player.position.Y - 0f), mod.NPCType("RoyalRabbit"));
            if (Main.netMode == NetmodeID.Server && num < 200)
            {
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
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
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.RoyalRabbitSummoned1"), 107, 137, 179);
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), player.Center);
                            Globals.AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAMod.Common.RajahRabbit"));

                        }
                        if (bunnyKills % 100 == 0 && bunnyKills >= 1000)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.RoyalRabbitSummoned2") + player.name.ToUpper() + "!", 107, 137, 179);
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Rajah"), player.Center);
                            Globals.AAModGlobalNPC.SpawnRajah(player, true, new Vector2(player.Center.X, player.Center.Y - 2000), Language.GetTextValue("Mods.AAMod.Common.RajahRabbit"));
                        };
                    }
                }
                item.active = false;
            }
        }
    }
}
