using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SnowflakeShuriken : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake Shuriken");
        }
        public override void SetDefaults()
		{
            item.damage = 10;
            item.maxStack = 999;
            item.ranged = true;
            item.width = 10;
            item.height = 10;
			item.useTime = 20;
			item.useAnimation = 20;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 0;
			item.value = 100;
			item.rare = 3;
			item.shootSpeed = 12f;
			item.shoot = mod.ProjectileType ("SS");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
        }
    }
}
