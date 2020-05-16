
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroTesseract : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomsday Tesseract");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
            Tooltip.SetDefault(@"DESCRIPTI0NHERE
UNSTABLE. C0NTAINS C0DE T0 ACTIVATE THE BRINGER 0F DEATH
N0N-C0NSUMABLE");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.rare = ItemRarityID.Purple;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
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

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<AAPlayer>().ZoneVoid)
            {
                if (NPC.AnyNPCs(mod.NPCType("Zero")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("ZeroProtocol")))
                {
                    if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitFalse"), new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem && player.releaseUseItem) if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroUnitVoidZoneFalse"), new Color(255, 0, 0), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (!AAWorld.downedZero && !Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroTesseractTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (!AAWorld.downedZero && Main.expertMode)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroTesseractTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (!Main.expertMode && AAWorld.downedZero)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroTesseractDownedTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (Main.expertMode && AAWorld.downedZero)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Language.GetTextValue("Mods.AAMod.Common.ZeroTesseractDownedTrue"), Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(mod.NPCType("ZeroDeactivated")))
                    NPC.NewNPC((int)player.position.X, (int)player.position.Y - 300, mod.NPCType("Zero"));
            }

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"));
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 15);
            recipe.AddIngredient(null, "DarkMatter", 20);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}