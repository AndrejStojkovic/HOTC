using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileData : MonoBehaviour
{
    public string Name;
    public int Experience;
    public int Level;
    public List<Trap> Loadout;
    public List<Trap> UnlockedTraps;

    public ProfileData(ProfileData profile) {
        Name = profile.Name;
        Experience = profile.Experience;
        Level = profile.Experience;
        Loadout = profile.Loadout;
        UnlockedTraps = profile.UnlockedTraps;
    }
}