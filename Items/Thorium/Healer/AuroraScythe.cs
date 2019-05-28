using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Thorium.Healer
{
    public class AuroraScythe : CrossoverItem
	{
		public override void SetStaticDefaults()
		{
			crossoverModName = "Thorium";
            DisplayName.SetDefault("Aurora Scythe");
            Tooltip.SetDefault(@"Spins a frostburning scythe around you that shreds through enemies
Scythes inflict frostburn on contact
Grants 1 soul essence on direct hit");			
		}

		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.rare = 3;
            item.value = BaseUtility.CalcValue(0, 5, 50, 50);

            item.useStyle = 1;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;
            item.damage = 24;
            item.knockBack = 6;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("AuroraScythe");
            item.shootSpeed = 0.1f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("AuroraScytheEffect"), damage, knockBack, player.whoAmI, k, 0f);
			}
			return true;
		}

		public override void GetWeaponDamage(Player player, ref int damage)
		{
			damage = (int)(damage * ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantBoost);
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (Main.rand.Next(100) <= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantCrit)
			{
				crit = true;
			}
		}

        public override void UpdateInventory(Player player)
        {
            if (!AAMod.thoriumLoaded)
            {
                item.TurnToAir();
            }
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int index = -1, index2 = -1;
            for (int m = 0; m < list.Count; m++)
            {
                if (list[m].Name.Equals("Damage")) { index = m; continue; }
                if (list[m].Name.Equals("Tooltip0")) { index2 = m; continue; }		
				if(index > -1 && index2 > -1) break;
            }
            string oldTooltip = list[index].text;
            string[] split = oldTooltip.Split(' '); 
            list.RemoveAt(index);
            list.Insert(index, new TooltipLine(mod, "Damage", split[0] + " radiant damage"));
			TooltipLine colorLine = new TooltipLine(mod, "Healer", "-Healer Class-");
			colorLine.overrideColor = new Color(255, 255, 91);
            list.Insert(index2, colorLine);
			base.ModifyTooltips(list);
        }

        public override void AddRecipes()
        {
            if (!AAMod.thoriumLoaded) return;
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(1306);
            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(1306);
            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}