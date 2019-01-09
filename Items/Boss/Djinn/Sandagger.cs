using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class DarkmatterKunai : ModItem
	{
		public override void SetDefaults()
		{

            item.damage = 15;            
            item.thrown = true;
            item.width = 14;
            item.height = 14;
			item.useTime = 8;
            item.maxStack = 999;
			item.useAnimation = 8;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.value = 8;
			item.rare = 3;
			item.shootSpeed = 9f;
			item.shoot = mod.ProjectileType ("Sandagger");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.consumable = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandaggeri");
            Tooltip.SetDefault("");
        }
    }
}
