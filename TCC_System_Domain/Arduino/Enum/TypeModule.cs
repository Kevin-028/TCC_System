﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_System_Domain.Arduino
{
    public enum TypeModule : int
    {
        FacialReader,// leitor facial
        FingerprintReader,// leitor de digital
        RFID, // modulo RFID
        System
    }
}