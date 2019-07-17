using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Rajah
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class RajahCape : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit's Cloak of Supremecy");
            Tooltip.SetDefault(@"Every 10% of health lost gives you:
1. 12% extra attack power to your highest damage type boost
2. 5% increased movement speed
3. 4% damage resistance
All effects of the Sash of Vengeance
'You have been deemed a worthy successor by the Champion of the Innocent'");
        }
        public override void SetDefaults()
        {
            item.width = 66;
            item.height = 78;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
            item.accessory = true;
            item.expert = true;
            item.defense = 10;
            item.expert = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            Color damageColor = Color.Firebrick;
            string DamageType = "";
            if (modPlayer.MeleeHighest(player))
            {
                DamageType = "Melee";
                damageColor = Color.Firebrick;
            }
            else if (modPlayer.RangedHighest(player))
            {
                DamageType = "Ranged";
                damageColor = Color.SeaGreen;
            }
            else if (modPlayer.MagicHighest(player))
            {
                DamageType = "Magic";
                damageColor = Color.Violet;
            }
            else if (modPlayer.SummonHighest(player))
            {
                DamageType = "Summoning";
                damageColor = Color.Cyan;
            }
            else if (modPlayer.ThrownHighest(player))
            {
                DamageType = "Throwing";
                damageColor = Color.DarkOrange;
            }

            string DamageAmmount = (10 * DamageBoost(player)) + "% ";

            string SpeedAmmount = (10 * Speed(player)) + "% ";

            string ResAmmount = (10 * DamageRes(player)) + "% ";

            TooltipLine DamageToltip = new TooltipLine(mod, "Damage Type", "Current Damage Boost: + " + DamageAmmount + DamageType + " Damage")
            {
                overrideColor = damageColor
            };
            TooltipLine SpeedTooltip = new TooltipLine(mod, "Damage Type", "Current Speed Boost: " + SpeedAmmount);

            TooltipLine ResTooltip = new TooltipLine(mod, "Damage Type", "Current Damage Resistance: " + ResAmmount);

            tooltips.Add(DamageToltip);
            tooltips.Add(SpeedTooltip);
            tooltips.Add(ResTooltip);
            base.ModifyTooltips(tooltips);
        }

        public override void UpdateEquip(Player player)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.autoJump = true;
            Player.jumpHeight = 40;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            if (modPlayer.MeleeHighest(player))
            {
                player.meleeDamage += DamageBoost(player);
            }
            else if (modPlayer.RangedHighest(player))
            {
                player.rangedDamage += DamageBoost(player);
            }
            else if (modPlayer.MagicHighest(player))
            {
                player.magicDamage += DamageBoost(player);
            }
            else if (modPlayer.SummonHighest(player))
            {
                player.minionDamage += DamageBoost(player);
            }
            else if (modPlayer.ThrownHighest(player))
            {
                player.thrownDamage += DamageBoost(player);
            }
            player.moveSpeed += Speed(player);
            player.endurance += DamageRes(player);
        }

        public float DamageBoost(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return 1.08f;
            }
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .96f;
            }
            if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .84f;
            }
            if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .72f;
            }
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .60f;
            }
            if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .48f;
            }
            if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .36f;
            }
            if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .12f;
            }
            return 0f;
        }

        public float DamageRes(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return .36f;
            }
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .32f;
            }
            if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .28f;
            }
            if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .24f;
            }
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .20f;
            }
            if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .16f;
            }
            if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .12f;
            }
            if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .08f;
            }
            if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .04f;
            }
            return 0f;
        }

        public float Speed(Player player)
        {
            if (player.statLife <= player.statLifeMax2 * .1f)
            {
                return .45f;
            }
            if (player.statLife <= player.statLifeMax2 * .2f)
            {
                return .4f;
            }
            if (player.statLife <= player.statLifeMax2 * .3f)
            {
                return .35f;
            }
            if (player.statLife <= player.statLifeMax2 * .4f)
            {
                return .3f;
            }
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                return .25f;
            }
            if (player.statLife <= player.statLifeMax2 * .6f)
            {
                return .2f;
            }
            if (player.statLife <= player.statLifeMax2 * .7f)
            {
                return .15f;
            }
            if (player.statLife <= player.statLifeMax2 * .8f)
            {
                return .1f;
            }
            if (player.statLife <= player.statLifeMax2 * .9f)
            {
                return .05f;
            }
            return 0f;
        }
    }
    
}