using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Equinox;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.BossSummons
{
    public class EquinoxWorm : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Equinox Worm");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"A worm created using celestial materials
Summons the Equinox Worms
Non-Consumable");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.rare = 11;
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>()) && !NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>());
        }

        public override bool UseItem(Player player)
        {
            if (AAWorld.WormActive || AAWorld.downedEquinox)
            {
                if (Main.netMode == 0) { if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken"), 175, 75, 255, false); }
                else if (Main.netMode == 2)
                    if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseMod.BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken"), 175, 75, 255, false); }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.AAMod.Common.EquinoxWormawoken")), new Color(175, 75, 255), -1);
                    }
                AAModGlobalNPC.SpawnBoss(player, mod.NPCType("DaybringerHead"), false, 0, 0);
                AAModGlobalNPC.SpawnBoss(player, mod.NPCType("NightcrawlerHead"), false, 0, 0);
                Main.PlaySound(SoundID.Roar, player.position, 0);
            }
            else
            {
                if (Main.netMode == 0) { if (Main.netMode != 1) BaseMod.BaseUtility.Chat("The Worm's eye flashes briefly, but does nothing.", 75, 175, 255, false); }
                else if (Main.netMode == 2)
                    if (Main.netMode == NetmodeID.SinglePlayer) { if (Main.netMode != 1) BaseMod.BaseUtility.Chat("The Worm's eye flashes briefly, but does nothing.", 75, 175, 255, false); }
                    else if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("The Worm's eye flashes briefly, but does nothing."), new Color(75, 175, 255), -1);
                    }
            }
            return true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        { 
            Texture2D texture = (AAWorld.WormActive || AAWorld.downedEquinox) ? mod.GetTexture("Items/BossSummons/EquinoxWormA") : Main.itemTexture[item.type];
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                AAColor.Shen3,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = (AAWorld.WormActive || AAWorld.downedEquinox) ? mod.GetTexture("Items/BossSummons/EquinoxWormA") : Main.itemTexture[item.type];
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MechanicalWorm, 2);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(null, "StarChart", 1);
            recipe.AddIngredient(null, "WormIdol", 1);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}