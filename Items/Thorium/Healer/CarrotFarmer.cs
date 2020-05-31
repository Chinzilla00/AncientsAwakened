using System.Collections.Generic;
using AAMod.CrossMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Thorium.Healer
{
	public class CarrotFarmer : CrossoverItem
	{
		public override void SetStaticDefaults()
		{
			crossoverModName = "ThoriumMod";
            DisplayName.SetDefault("Carrot Farmer");
            Tooltip.SetDefault(@"Spins a Carrot Scythe around you that shreds through enemies
Scythes fire off carrots while spun
Grants 1 soul essence on direct hit");			
		}

		public override void SetDefaults()
		{
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.value = BaseUtility.CalcValue(0, 10, 0, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 25;
            item.useTime = 25;
            item.UseSound = SoundID.Item1;
            item.damage = 80;
            item.knockBack = 9;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("CarrotFarmer");
            item.shootSpeed = 0.1f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int k = 0; k < 2; k++)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("CarrotFarmerEffect"), damage, knockBack, player.whoAmI, k, 0f);
			}
			return true;
		}

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= ((CrossMod.ModSupportPlayer)player.GetModPlayer(mod, "CrossMod.ModSupportPlayer")).Thorium_radiantBoost;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (Main.rand.Next(100) <= ((CrossMod.ModSupportPlayer)player.GetModPlayer(mod, "CrossMod.ModSupportPlayer")).Thorium_radiantCrit)
			{
				crit = true;
			}
		}

        public override void UpdateInventory(Player player)
        {
            if (ModLoader.GetMod("ThoriumMod") == null)
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
            TooltipLine colorLine = new TooltipLine(mod, "Healer", "-Healer Class-")
            {
                overrideColor = new Color(255, 255, 91)
            };
            list.Insert(index2, colorLine);
			base.ModifyTooltips(list);
        }
    }
}