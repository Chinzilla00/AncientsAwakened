using Microsoft.Xna.Framework;
using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning
{
    public class DoomiteSignalC : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Radio");
            Tooltip.SetDefault(@"Summons a doomite radio probe to fight with you");
        }

        public override void SetDefaults()
        {
            item.damage = 21;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.noMelee = true;
            item.knockBack = 3;
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("DoomiteProbeC");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("DoomiteProbeC");
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;
            }
            float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }
            num78 = 0f;
            num79 = 0f;
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, mod.ProjectileType("DoomiteProbeC"), num73, num74, i, 0f, 0f);
            return false;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "DoomiteSignal");
			recipe.AddIngredient(mod, "SandScepter");
			recipe.AddIngredient(mod, "SnakeStaff");
			recipe.AddIngredient(mod, "FireStaff");
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "DoomiteSignal");
			recipe.AddIngredient(mod, "SandScepter");
			recipe.AddIngredient(mod, "SnakeStaff");
			recipe.AddIngredient(mod, "HydraStaff");
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}