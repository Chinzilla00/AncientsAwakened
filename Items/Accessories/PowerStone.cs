using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class PowerStone : ModItem
    {
        public static short customGlowMask = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Stone");
            Tooltip.SetDefault(
@"Multiplies your attack power by 2.25
'Fun isn’t something one considers when balancing the universe'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Items/Accessories/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.accessory = true;
            item.glowMask = customGlowMask;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Purple;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 2.25f;
            player.rangedDamage *= 2.25f;
            player.magicDamage *= 2.25f;
            player.thrownDamage *= 2.25f;
            player.minionDamage *= 2.25f;
            int num = 0;
            num += player.bodyFrame.Y / 56;
            if (num >= Main.OffsetsPlayerHeadgear.Length)
            {
                num = 0;
            }
            Vector2 vector = new Vector2((float)(3 * player.direction - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + Vector2.UnitY * player.gfxOffY + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector2 = new Vector2((float)(3 * player.shadowDirection[1] - ((player.direction == 1) ? 1 : 0)), -11.5f * player.gravDir) + player.Size / 2f + Main.OffsetsPlayerHeadgear[num];
            Vector2 vector3 = Vector2.Zero;
            if (player.mount.Active && player.mount.Cart)
            {
                int num2 = Math.Sign(player.velocity.X);
                if (num2 == 0)
                {
                    num2 = player.direction;
                }
                vector3 = new Vector2(MathHelper.Lerp(0f, -8f, player.fullRotation / 0.7853982f), MathHelper.Lerp(0f, 2f, Math.Abs(player.fullRotation / 0.7853982f))).RotatedBy((double)player.fullRotation, default(Vector2));
                if (num2 == Math.Sign(player.fullRotation))
                {
                    vector3 *= MathHelper.Lerp(1f, 0.6f, Math.Abs(player.fullRotation / 0.7853982f));
                }
            }
            if (player.fullRotation != 0f)
            {
                vector = vector.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
                vector2 = vector2.RotatedBy((double)player.fullRotation, player.fullRotationOrigin);
            }
            float num3 = 0f;
            if (player.mount.Active)
            {
                num3 = (float)player.mount.PlayerOffset;
            }
            Vector2 vector4 = player.position + vector + vector3;
            Vector2 vector5 = player.oldPosition + vector2 + vector3;
            vector5.Y -= num3 / 2f;
            vector4.Y -= num3 / 2f;
            float num4 = 1f;
            switch (player.yoraiz0rEye % 10)
            {
                case 1:
                    return;
            }
            if (player.yoraiz0rEye < 7)
            {
                DelegateMethods.v3_1 = Main.hslToRgb(Main.rgbToHsl(player.eyeColor).X, 1f, 0.5f).ToVector3() * 0.5f * num4;
                if (player.velocity != Vector2.Zero)
                {
                    Utils.PlotTileLine(player.Center, player.Center + player.velocity * 2f, 4f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));
                }
                else
                {
                    Utils.PlotTileLine(player.Left, player.Right, 4f, new Utils.PerLinePoint(DelegateMethods.CastLightOpen));
                }
            }
            int num5 = (int)Vector2.Distance(vector4, vector5) / 3 + 1;
            if (Vector2.Distance(vector4, vector5) % 3f != 0f)
            {
                num5++;
            }
            for (float num6 = 1f; num6 <= (float)num5; num6 += 1f)
            {
                Dust dust = Main.dust[Dust.NewDust(player.Center, 0, 0, 182, 0f, 0f, 0, Color.MediumPurple, 1f)];
                dust.position = Vector2.Lerp(vector5, vector4, num6 / (float)num5);
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
                dust.customData = this;
                dust.scale = num4;
                dust.shader = GameShaders.Armor.GetSecondaryShader(player.cYorai, player);
            }
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().Power = true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Amethyst, 10);
                recipe.AddIngredient(ItemID.LargeAmethyst, 1);
                recipe.AddIngredient(ItemID.AvengerEmblem, 1);
                recipe.AddIngredient(ItemID.FragmentNebula, 15);
                recipe.AddIngredient(ItemID.FragmentSolar, 15);
                recipe.AddIngredient(ItemID.FragmentVortex, 15);
                recipe.AddIngredient(ItemID.FragmentStardust, 15);
                recipe.AddIngredient(ItemID.SoulofMight, 30);
                recipe.AddIngredient(null, "DarkMatter", 10);
                recipe.AddIngredient(null, "RadiumBar", 10);
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
        public bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.type == mod.ItemType("PowerStone"))
            {
                if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
                {
                    int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                    for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                    {
                        // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                        if (slot != i && player.armor[i].type == mod.ItemType<InfinityGauntlet>())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
    
}