using BaseMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroTesseractMK22 : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomsday Tesseract MK2");
            Tooltip.SetDefault(@"DESCRIPTI0NHERE
UNSTABLE. C0NTAINS C0DE T0 ACTIVATE THE BRINGER 0F DEATH
N0N-C0NSUMABLE");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
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
            if (player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
            {
                if (NPC.AnyNPCs(mod.NPCType("Zero2")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("ERR0R. ZER0 UNIT ALREADY ACTIVE. PLEASE TRY AGAIN LATER.", new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("ZeroAwakened2")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("ERR0R. ZER0 UNIT ALREADY ACTIVE. PLEASE TRY AGAIN LATER.", new Color(255, 0, 0), false);
                    return false;
                }
                return true;
            }
            if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("ERR0R. PLAYER.GETM0DPLAYER<AAPLAYER>(M0D).Z0NEV0ID == FALSE. PLEASE TRY AGAIN LATER.", new Color(255, 0, 0), false);
            return false;
        }

        public override bool UseItem(Player player)
        {
            if (!AAWorld.downedZero && !Main.expertMode)
            {
                Main.NewText("ZER0 UNIT ACTIVATED. ENGAGE D00MBRINGER PR0T0C0L.", Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (!AAWorld.downedZero && Main.expertMode)
            {
                Main.NewText("ZER0 UNIT ACTIVATED. ENGAGE D00MBRINGER PR0T0C0L.", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (!Main.expertMode && AAWorld.downedZero)
            {
                Main.NewText("TARGET L0CKED. FAILURE T0 TERMINATE Y0U IS N0T A P0SSIBILITY THIS TIME, TERRARIAN.", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (Main.expertMode && AAWorld.downedZero)
            {
                Main.NewText("TARGET L0CKED. FAILURE T0 TERMINATE Y0U IS N0T A P0SSIBILITY THIS TIME, TERRARIAN.", Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (Main.netMode != 1)
            {
				AAWorld.zeroUS = true;
				if(!NPC.AnyNPCs(mod.NPCType("ZeroDeactivated")))
					NPC.NewNPC((int)player.position.X + Main.rand.Next(-2200, 2200), (int)player.position.Y + 1200, mod.NPCType("Zero2"));
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