using BaseMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class ZeroRune : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ERR0R_NULL");
            Tooltip.SetDefault(@"ACTIVATES THE GR0UND ZER0 C0DE F0R THE NEAREST ZER0 UNIT
N0N-C0NSUMABLE");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Oblivion;
                }
            }
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
            {
                if (NPC.AnyNPCs(mod.NPCType("Zero")))
                {
                    if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("ERR0R. ZER0 UNIT ALREADY ACTIVE. PLEASE TRY AGAIN LATER.", new Color(255, 0, 0), false);
                    return false;
                }
                if (NPC.AnyNPCs(mod.NPCType("ZeroAwakened")))
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
            Main.NewText("D00MSDAY PR0T0CALL ACTIVATED MANUALLY. TERMINATI0N SYSTEMS AT FULL P0WER", Color.Red.R, Color.Red.G, Color.Red.B);

            if (Main.netMode != 1)
            {
                AAWorld.zeroUS = true;
                if (!NPC.AnyNPCs(mod.NPCType("ZeroDeactivated")))
                    NPC.NewNPC((int)player.position.X + Main.rand.Next(-2200, 2200), (int)player.position.Y + 1200, mod.NPCType("ZeroAwakened"));
            }
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ZeroDeath"));
            return true;
        }
    }
}