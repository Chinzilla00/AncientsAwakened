using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class SpaceStone : ModItem
    {
        
        public int StoneCD;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Stone");
            Tooltip.SetDefault(
@"Allows you to teleport with the hook funtion like with the rod of discord
You are immune to the Chaos State Debuff
'But this...Does put a smile on my face'");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Cyan;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void UpdateEquip(Player player)
        {
            player.buffImmune[88] = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlHook && StoneCD == 0 && Main.myPlayer == player.whoAmI)
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
                        NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                        
                        StoneCD = 120;
                    }
                }
            }
            if (StoneCD != 0)
            {
                StoneCD--;
            }
            
        }


        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType<PowerStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<MindStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<SoulStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<RealityStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<TimeStone>())
                    {
                        return false;
                    }
                    if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}