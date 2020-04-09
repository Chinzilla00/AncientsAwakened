using Terraria;

namespace AAMod.Items.Boss.AH
{
    public class Masamune : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Masamune");
            Tooltip.SetDefault(@"Left click to quickly slash at your foes with the blade
Ignores invicibility frames
Right click to shoot a blade wave");
		}

		public override void SetDefaults()
		{
            item.damage = 350;
            item.width = 70; 
            item.height = 80;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.useAnimation = 25;
            item.useTime = 15;
            item.useStyle = 5;
            item.useTime = 5;
            item.knockBack = 4f;
            item.autoReuse = false;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.shoot = mod.ProjectileType("Surasshu");
            item.shootSpeed = 15f;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.melee = true;
                item.noMelee = false;
                item.noUseGraphic = false;
                item.damage = 250;
                item.channel = false;
                item.useAnimation = 15;
                item.useTime = 15;
                item.useStyle = 1;
                item.autoReuse = true;
                item.shoot = mod.ProjectileType("MasamuneSlash");
                item.shootSpeed = 12f;
            }
            else
            {
                item.damage = 350;
                item.melee = true;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.channel = true;
                item.useAnimation = 25;
                item.useTime = 5;
                item.useStyle = 5;
                item.autoReuse = false;
                item.shoot = mod.ProjectileType("Surasshu");
                item.shootSpeed = 15f;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("Moonraze"), 600);
        }
    }
}