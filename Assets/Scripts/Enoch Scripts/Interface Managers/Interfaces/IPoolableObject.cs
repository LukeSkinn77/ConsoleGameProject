﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableObject
{
    void SetObjectPool(ObjectPooler objectPooler);
}
