using MetaDslx.Modeling;
using MofBootstrapLib.Generator;
using MofBootstrapLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MofBootstrap
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmiSerializer = new MofXmiSerializer();

            MofGenerator mofGenerator = new MofGenerator(xmiSerializer.ReadModelFromFile("../../../MOF.xmi"));

            mofGenerator.GenerateCompleteMof(false);
        }
    }
}
