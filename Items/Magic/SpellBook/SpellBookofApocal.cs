using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Magic.SpellBook
{
    public class SpellBookofApocal : EffectSpellBook
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellBook of Apocal");
			Tooltip.SetDefault(@"Fires spinning stars that bounce on walls
If this book is copied and consumed by the SpellBook of Sefer, you get the following effects:
In this shooting turn, your magic projectiles will get homing effects");
		}

		public override void SetDefaults()
		{
			item.damage = 415;
			item.mana = 12;
			item.useStyle = 5;
			item.shootSpeed = 4f;
			item.shoot = mod.ProjectileType("SagStar");
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item117;
			item.useTime = 20;
            item.useAnimation = 20;
			item.autoReuse = true;
			item.noMelee = true;
			item.knockBack = 2f;
			item.rare = 10;
			item.value = Item.sellPrice(0, 25, 26, 0);
			item.magic = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddIngredient(null, "VoidEnergy", 10);
			recipe.AddIngredient(null, "ApocalyptitePlate", 10);
            recipe.AddTile(null, "BinaryReassembler");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void Sustainedeffect(Projectile projectile)
		{
			if(projectile.type != 250 && projectile.type != 251)
			{
				const int homingDelay = 20;
				const float desiredFlySpeedInPixelsPerFrame = 60;
				const float amountOfFramesToLerpBy = 20;

				projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().ExtraAI0++;
				if (projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().ExtraAI0 > homingDelay)
				{
					projectile.GetGlobalProjectile<Items.Magic.SpellBook.spellbookproj>().ExtraAI0 = 15;

					int foundTarget = HomeOnTarget(projectile);
					if (foundTarget != -1)
					{
						NPC n = Main.npc[foundTarget];
						Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
						projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
					}
				}
			}
		}

		private int HomeOnTarget(Projectile projectile)
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance)
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }
	}
}
