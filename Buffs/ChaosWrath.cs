using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class ChaosWrath : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Chaotic Wrath");
            Description.SetDefault("Pain only makes you stronger");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (modPlayer.perfectChaosRa)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.rangedDamage += .4f;
                    player.rangedCrit += 7;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.rangedDamage += .3f;
                    player.rangedCrit += 14;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.rangedDamage += .2f;
                    player.rangedCrit += 21;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.rangedDamage += .1f;
                    player.rangedCrit += 28;
                }
            }
            else if (modPlayer.perfectChaosSu)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.minionDamage *= 1.60f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.minionDamage *= 1.45f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.minionDamage *= 1.3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.minionDamage *= 1.15f;
                }
            }
            else if (modPlayer.perfectChaosMe)
            {
                if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.endurance += .05f;
                    player.meleeDamage += .1f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.endurance += .1f;
                    player.meleeDamage += .2f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.endurance += .12f;
                    player.meleeDamage += .3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.endurance += .15f;
                    player.meleeDamage += .4f;
                }
            }
            else if (modPlayer.perfectChaosMa)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.manaCost *= 0;
                    player.magicDamage *= 1.4f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.manaCost *= .25f;
                    player.magicDamage *= 1.3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.manaCost *= .5f;
                    player.magicDamage *= 1.2f;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.manaCost *= .75f;
                    player.magicDamage *= 1.1f;
                }
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}