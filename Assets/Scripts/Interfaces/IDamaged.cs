﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaged
{
    void TakeDamage(int _physicalDamage, int _magicDamage);
}
