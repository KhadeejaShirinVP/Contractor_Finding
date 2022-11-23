﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEncrypt
    {
        string EncodePasswordToBase64(string password);
        string DecodeFrom64(string encodedData);
    }
}
