using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn, EquipType.Wings)]
    public class InfinityGauntlet : ModItem
    {
            
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault(
@"Pressing the G key allows you to snap your fingers, wiping out half of the enemies on your screen
The snap has a 5 minute cooldown
All effects of the infinity stones
'Perfectly Balanced, as all things should be'");
        }

        public bool death;
        public int rodCD;
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 44;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.rare = 11;
            item.expert = true;
            item.accessory = true;
            item.defense = 12;
            
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

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage *= 3;
            player.meleeSpeed += 0.18f;
            player.aggro += 8;
            player.meleeDamage *= 1.5f;
            player.rangedDamage *= 1.5f;
            player.magicDamage *= 1.5f;
            player.thrownDamage *= 1.5f;
            player.minionDamage *= 1.5f;
            player.statManaMax2 += 1000;
            player.buffImmune[88] = true;
            player.maxMinions += 10;
            player.manaCost *= 0.0f;
            player.accRunSpeed = 10;
            player.moveSpeed += 1f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;
        }

        

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 1000;
            player.GetModPlayer<AAPlayer>().dwarvenGauntlet = true;
            player.GetModPlayer<AAPlayer>().Power = true;
            player.GetModPlayer<AAPlayer>().Time = true;
            player.GetModPlayer<AAPlayer>().InfinityGauntlet = true;
            if (player.controlHook && rodCD == 0 && Main.myPlayer == player.whoAmI)
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

                        rodCD = 30;
                    }
                }
            }
            if (rodCD != 0)
            {
                rodCD--;
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.4f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 5f;
            constantAscend = 0.3f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 25f;
            acceleration *= 5f;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DwarvenGauntlet", 1);
                recipe.AddIngredient(null, "RealityStone", 1);
                recipe.AddIngredient(null, "SoulStone", 1);
                recipe.AddIngredient(null, "MindStone", 1);
                recipe.AddIngredient(null, "TimeStone", 1);
                recipe.AddIngredient(null, "SpaceStone", 1);
                recipe.AddIngredient(null, "PowerStone", 1);
                recipe.SetResult(this);
                recipe.AddRecipe();
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
                    if (slot != i && player.armor[i].type == mod.ItemType<SpaceStone>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}