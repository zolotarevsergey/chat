﻿using System;

namespace BLL.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message)
        {

        }
    }
}
