
using AAMod.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.BossSummons
{
    public class WormIdol : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Idol");
            Tooltip.SetDefault(@"An ancient statue depicting some form of worm god.
It looks like it hasn't been touched in years");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.Purple;
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<IdolPointer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("WormPointer")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("WormPointer"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MechanicalWorm, 1);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            recipe.AddIngredient(null, "SoulFragment", 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }

    public class IdolPointer : ModPlayer
    {
        public bool effect;

        public override void ResetEffects()
        {
            effect = false;
        }
    }

    public class WormPointer : ModProjectile
    {
        public override string Texture => "AAMod/Textures/WormPointer";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pointer");
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0;
        }

        public override void AI()
        {
            Vector2 AltarSpawn = new Vector2(Main.maxTilesX * 0.15f * 16, 100 * 16);
            Player player = Main.player[projectile.owner];
            IdolPointer modPlayer = player.GetModPlayer<IdolPointer>();

            if (!modPlayer.effect)
            {
                projectile.Kill();
                return;
            }

            Vector2 PlayerPoint = Vector2.Zero;

            PlayerPoint.X = player.Center.X - projectile.width / 2;
            PlayerPoint.Y = player.Center.Y - projectile.height / 2 + player.gfxOffY - 60f;

            projectile.Center = PlayerPoint;

            AltarSpawn += new Vector2(37.5f * 16, 42 * 16);

            BaseAI.LookAt(AltarSpawn, projectile, 2, 0, 0, true);

            projectile.direction = 1;
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Rectangle frame = BaseDrawing.GetFrame(0, 30, 30, 0, 0);

            BaseDrawing.DrawAura(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, auraPercent, 1.2f, projectile.scale, projectile.rotation, -1, 1, frame, 0, 0, Color.White);
            BaseDrawing.DrawTexture(sb, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, -1, 1, frame, projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE));

            return false;
        }
    }
}