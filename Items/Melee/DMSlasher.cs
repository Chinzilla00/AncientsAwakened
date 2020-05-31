﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using AAMod.Globals;

namespace AAMod.Items.Melee
{
    public class DMSlasher : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Slasher");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 56;
            item.melee = true;
            item.damage = 350;
            item.knockBack = 3;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.useTime = 30 / 2; //dont change the 30 unless you want to soft lock your computer. instead use it as a value and use 15 as the number 2, doing this will divide the 30 with 15 to get a use time of 2
            item.useAnimation = 13;
            item.shoot = mod.ProjectileType("MS");
            item.shootSpeed = 25f;
            item.value = 25000;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkEnergy", 35);
            recipe.AddIngredient(null, "DarkMatter", 25);
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void HoldItem(Player player)
        {
            Saber.HoldItemManager(player, item, ModContent.ProjectileType<DMSlash>(),
                Color.Blue, 0.9f, player.itemTime == 0 ? 0f : 1f);
        }

        // Doesn't get called unless item.shoot is defined.
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        { return Saber.IsChargedShot(player); }

        public override bool UseItemFrame(Player player)
        {
            Saber.UseItemFrame(player, 0.9f, item.isBeingGrabbed);
            return true;
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            int height = 80;
            int length = 132;
            Saber.UseItemHitboxCalculate(player, item, ref hitbox, ref noHitbox, 0.9f, height, length);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Color colour = new Color(0.1f, 255f, 181f);
            Saber.OnHitFX(player, target, crit, colour, true);
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (Saber.SabreIsChargedStriking(player, item))
            { damage = -500; }
        }
    }
    public class DMSlash : ModProjectile
    {
        public static Texture2D specialSlash;
        public static int specialProjFrames = 7;
        readonly int chargeSlashDirection = 1;


        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 7;
            if (Main.netMode == NetmodeID.Server) return;
        Player projOwner = Main.player[projectile.owner];
          projectile.position.X = projOwner.Center.X - (projectile.width / 2);
          projectile.position.Y = projOwner.Center.Y - (projectile.height / 2);
            specialSlash = mod.GetTexture("Items/Melee/" + GetType().Name + "2");
        projectile.direction = projOwner.direction;
        projectile.spriteDirection = projOwner.direction;
        projOwner.heldProj = projectile.whoAmI;
        projOwner.itemTime = projOwner.itemAnimation;
        }
        public override void SetDefaults()
        {
            projectile.width = 136;
            projectile.height = 66;
            projectile.aiStyle = -1;
            projectile.timeLeft = 60;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override bool? CanCutTiles() { return false; }
        public int FrameCheck
        {
            get { return (int)projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }
        public int SlashLogic
        {
            get { return (int)projectile.ai[1]; }
            set { projectile.ai[1] = value; }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Saber.AINormalSlash(projectile, SlashLogic)) { }
            else
            {
                // Charged attack
                Saber.AISetChargeSlashVariables(player, chargeSlashDirection);
                Saber.NormalSlash(projectile, player);
            }
            projectile.damage = 0;
            projectile.ai[0] += 1f; // Framerate
            projectile.position += player.velocity;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Player player = Main.player[projectile.owner];
            int weaponItemID = ModContent.ItemType<DMSlasher>();
            Color lighting = Lighting.GetColor((int)(player.MountedCenter.X / 16), (int)(player.MountedCenter.Y / 16));
            return Saber.PreDrawSlashAndWeapon(spriteBatch, projectile, weaponItemID, lighting,
                SlashLogic == 0f ? specialSlash : null,
                SlashLogic == 0f ? new Color(1f, 255f, 181f, 1f) : lighting,
                specialProjFrames,
                SlashLogic == 0f ? chargeSlashDirection : SlashLogic);
        }

    }
}
