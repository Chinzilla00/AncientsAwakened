using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace GRealm.Items.Weapons.Melee
{
	public class DoomiteMiningLaser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doomite Mining Laser");	
            BaseUtility.AddTooltips(item, new string[] { "100% pickaxe power", "Mines using the power of a dark laser" });			
		}		

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 18;
            item.shootSpeed = 36f;
            item.knockBack = 1f;
            item.width = 20;
            item.height = 12;
            item.damage = 110;
            item.pick = 100;
            item.axe = 30;
            item.UseSound = SoundID.Item23;
            item.shoot = mod.ProjectileType("MiningLaser");
            item.rare = 4;
            item.value = Item.sellPrice(0, 0, 54, 0);
            item.tileBoost = 2;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.channel = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddItem(null, "Doomite", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            for (int m = 0; m < Main.projectile.Length; m++)
            {
                Projectile p = Main.projectile[m];
                if (p != null && p.active && p.owner == player.whoAmI && p.type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
	}
}