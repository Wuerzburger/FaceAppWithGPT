﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAppWithGPT
{
    public interface ICommandHandler
    {
        void HandleCommand(CliOptions options);
    }
}
