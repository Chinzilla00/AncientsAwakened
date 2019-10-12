using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Magic
{
    public class ElectricitySpell : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 90;                        
            item.magic = true;                     
            item.width = 32;
            item.height = 32;

            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;        
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 6;
            item.mana = 4;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("ElectricitySpellP");  
            item.shootSpeed = 11f;     
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Electricity Shard");
          Tooltip.SetDefault("It shoots sparks in an even spread.");
        }

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		float spread = 45f * 0.0174f;
		float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
		double startAngle = Math.Atan2(speedX, speedY)- (spread/2);
		double deltaAngle = spread/5f;
		double offsetAngle;
		int i;
		for (i = 0; i < 5;i++ )
		{
			offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
		}
		return false;
		}
	}

    public class SpellDrop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.AngryNimbus && Main.rand.Next(6) == 0)
            {
                npc.DropLoot(ModContent.ItemType<ElectricitySpell>());
            }
        }
    }

}
