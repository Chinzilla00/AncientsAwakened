﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class BrokenCode : BaseAAItem
    {
        
        public int CodeCD = 0;
        public bool on = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Code");
            Tooltip.SetDefault(@"Allows you to glitch with a 5 second cooldown
Grapple to Glitch
While cooldown is occurring, your speed is increased, you gain invincibility frames
While cooldown is occurring, your magic/summon weapons require no mana and have 20% increased damage
Teleportation has 15 second cooldown
'You don't look so good'
WARNING: May permanently displace appendages until game restart. This is a feature.");
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 60;
            item.height = 52;
            item.maxStack = 1;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.expert = true; item.expertOnly = true;
            item.accessory = true;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity13;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Globals.AAColor.COLOR_WHITEFADE1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlHook && CodeCD == 0 && Main.myPlayer == player.whoAmI)
            {
                Vector2 vector32;
                vector32.X = Main.mouseX + Main.screenPosition.X;
                if (player.gravDir == 1f)
                {
                    vector32.Y = Main.mouseY + Main.screenPosition.Y - player.height;
                }
                else
                {
                    vector32.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                }
                vector32.X -= player.width / 2;
                if (vector32.X > 50f && vector32.X < (Main.maxTilesX * 16) - 50 && vector32.Y > 50f && vector32.Y < (Main.maxTilesY * 16) - 50)
                {
                    int num246 = (int)(vector32.X / 16f);
                    int num247 = (int)(vector32.Y / 16f);
                    if ((Main.tile[num246, num247].wall != 87 || num247 <= Main.worldSurface || NPC.downedPlantBoss) && !Collision.SolidCollision(vector32, player.width, player.height))
                    {
                        player.Teleport(vector32, 1, 0);
                        NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Glitch"));
                        CodeCD = 600;
                        player.AddBuff(ModContent.BuffType<Buffs.Glitched>(), 300);
                    }
                }
            }
            if (CodeCD > 300)
            {
                if (CodeCD > 450)
                {
                    player.immuneNoBlink = true;
                }
                else
                {
                    player.immuneNoBlink = false;
                }
                if (on)
                {
                    on = false;
                    player.moveSpeed += 5f;
                    player.headPosition.Y -= 20f;
                    player.headPosition.X += 15f;
                    player.bodyPosition.Y += 37f;
                    player.bodyPosition.X -= 23f;
                    player.legPosition.Y += 20f;
                    player.legPosition.X -= 12f;
                }
            }
            else
            {
                if (!on)
                {
                    on = true;
                    player.moveSpeed -=5f;
                    player.headPosition.Y += 20f;
                    player.headPosition.X -= 15f;
                    player.bodyPosition.Y -= 37f;
                    player.bodyPosition.X += 23f;
                    player.legPosition.Y -= 20f;
                    player.legPosition.X += 12f;
                }
            }
            if (CodeCD > 0)
            {
                CodeCD --;
            }
            if (item.accessory)
            {
                player.GetModPlayer<AAPlayer>().BrokenCode = true;
            }
            else
            {
                player.GetModPlayer<AAPlayer>().BrokenCode = false;
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }
    }
}