using AAMod.Items.Boss;
using AAMod.Tiles.Crafters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace AAMod.Items.Dev
{
    public class TemporalAnomalyEX : BaseAAItem
    {
        public override string Texture => "AAMod/BlankTex";

        private const float intensity = 0.01f;
        private const float frequency = 16;
        private const float rotationIncrement = 4.5f;

        private const int attractRange = 2048;
        private const int numBalls = 5;
        private const int timeBetweenSuccNoises = 20;
        private const int dustDistance = 96;

        private int soundTimer;

        private float rotationOffset;

        private static MiscShaderData TooltipShader
        {
            get => GameShaders.Misc["AAMod:TemporalAnomalyTooltip"];
            set => GameShaders.Misc["AAMod:TemporalAnomalyTooltip"] = value;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jaw of Oblivion");
            Tooltip.SetDefault("Fires an otherwordly asteroid that collapses into a singularity\nHold right click to attract nearby items towards you\n\"The Supremest of SUCC -Oli\"");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.Size = new Vector2(40);
            item.magic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 25;
            item.useAnimation = 25;
            item.mana = 150;
            item.damage = 2060;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.knockBack = 8.5f;
            item.UseSound = SoundID.Item73;
            item.shoot = ProjectileType<Projectiles.Dev.TemporalAnomalyEX>();
            item.shootSpeed = 12;
            item.expert = true;
            item.expertOnly = true;
            item.rare = ItemRarityID.Purple;
            item.noMelee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemType<TemporalAnomaly>());
            recipe.AddIngredient(ItemType<EXSoul>());
            recipe.AddTile(TileType<QuantumFusionAccelerator>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && Main.mouseRight && !Main.mouseLeft)
            {
                for (int i = 0; i < Main.maxItems; i++)
                {
                    Item item = Main.item[i];
                    if (item != null && item.active && item.DistanceSQ(player.Center) < attractRange * attractRange && item.velocity.Length() < 32f)
                    {
                        item.velocity += item.DirectionTo(player.Center) * 4;
                    }
                }

                soundTimer++;
                if (soundTimer >= timeBetweenSuccNoises)
                {
                    Main.PlaySound(SoundID.Item103);
                    soundTimer = 0;
                }

                rotationOffset += rotationIncrement;
                float rotationPerBall = 360f / numBalls;

                for (int i = 0; i < numBalls; i++)
                {
                    Vector2 spawnPosition = player.Center + new Vector2(dustDistance, 0).RotatedBy(MathHelper.ToRadians((rotationPerBall * i) + rotationOffset));
                    Color color = Color.Lerp(Color.Black, Color.Purple, Main.rand.NextFloat());
                    for (int j = 0; j < 10; j++)
                    {
                        Dust.NewDust(spawnPosition, 4, 4, DustID.Smoke, 0, 0, 0, color);
                    }
                }
            }
            else
            {
                soundTimer = 0;
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ProjectileType<Projectiles.Dev.TemporalAnomalyEX>()] == 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 80;
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override bool PreDrawTooltip(ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            TooltipShader.UseSaturation(intensity).UseOpacity(frequency).Apply();
            return true;
        }

        public override void PostDrawTooltip(ReadOnlyCollection<DrawableTooltipLine> lines)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
        }

        public static void LoadTooltipShader()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> tShader = new Ref<Effect>(AAMod.instance.GetEffect("Effects/TemporalAnomalyTooltip"));
                TooltipShader = new MiscShaderData(tShader, "ShadeTooltip");
            }
        }
    }
}
