using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoL
{
    public static class StatsCalculations
    {
        public static int CalculateBaseDamage(WeaponStats w, CharacterStats st)
        {
            int physical = w.a_physical - st.physical;
            int strike = w.a_strike - st.vs_strike;
            int slash = w.a_slash - st.vs_slash;
            int thrust = w.a_thrust - st.vs_thrust;

            int sum = physical + strike + slash + thrust;

            int magic = w.a_magic - st.magic;
            int fire = w.a_fire - st.fire;
            int lighting = w.a_lighting - st.lighting;
            int dark = w.a_dark - st.dark;

            sum += magic + fire + lighting + dark;

            if (sum <= 0)
                sum = 1;

            return sum;
        }

    }
}
