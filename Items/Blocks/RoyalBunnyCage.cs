using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace AAMod.Items.Blocks
{
    class RoyalBunnyCage : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.width = 24;
            item.height = 22;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("RoyalBunnyCage"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Bunny Cage");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RoyalRabbit", 1);
            recipe.AddIngredient(ItemID.Terrarium, 1);
            recipe.AddRecipeGroup("AAMod:Gold", 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
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
